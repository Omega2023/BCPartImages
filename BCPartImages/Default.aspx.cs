using System;
using System.Collections.Generic;
using System.ComponentModel; // CancelEventArgs
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Deployment;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Xml;

namespace BCPartImages
{
    public partial class _Default : Page
    {
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet m_dsWork = new DataSet();
        DataTable m_dtWork = new DataTable();

        string xmlfn = "\\\\OmegaFS2\\Apps\\BCPartImages\\BCPartImages.xml";

        //Global items
        //string ConnectionString = "";
        string hostName = System.Net.Dns.GetHostName();
        string AppLogDir = "C:\\Log\\";
        string AppLog = "BCPartImages.txt";
        //string MyApplication = "Parts Images";

        //Omega_Cost.XML file initializations
        string AppVersion = "1.0.0.0";

        string BaseSQL = String.Empty; //SQL command that gets data to fill the m_dsWork DataSet
        string BaseSQLFile = String.Empty;
        string daConnNAV = String.Empty;
        string daConnSOLO = String.Empty;
        string daConnBC = String.Empty;
        string SQLTimeout = "";
        string CostDir = "";
        string[] colHeaders = new string[100];

        protected void Page_Load(object sender, EventArgs e)
        {
            bool status = false;
            bool loaded = false;

            if (!loaded)
            {
                try
                {
                    if (Directory.Exists(AppLogDir))
                    {
                        File.Delete(Path.Combine(AppLogDir, AppLog));
                    }
                    else
                    {
                        Directory.CreateDirectory(AppLogDir);
                    } //End If
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error Loading File: " + AppLogDir + "\r\n" + ex.Message);
                    LogEvent("Error Loading File: " + AppLogDir + "\r\n" + ex.Message);
                } //End Try
                loaded = true;
            }
            else
            {
                LogEvent("BCPartImages reloaded");
            } //End If

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                AppVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                //Put the assembly version here if the application is not a publish-ready ClickOnce app
                AppVersion = Application.ProductVersion;
            } //End If
            this.Text += " Version " + AppVersion;
            LogEvent("*** BCPartImages Version " + AppVersion + " ***");

            GetIPAddress(); //Get IP Address and PC Name

            status = ImportXmlInit(xmlfn, status); //Load XML file
            if (!status)
            {
                Quit_Cost_Calculation();
            } //End If

            LogEvent("Part Images BaseSQLFile " + BaseSQLFile);
            BaseSQL = ImportSQL(BaseSQLFile); //Load SQL file
            LogEvent("Part Images BaseSQL " + BaseSQL);

            if (!IsPostBack)
            {
                Label1.Text = "$0.00";
                Label2.Text = "$0.00";
                Label3.Text = "$0.00";
                Label4.Text = "$0.00";
                Label5.Text = "$0.00";
                Label6.Text = "$0.00";
                Label7.Text = "$0.00";
                Label8.Text = "$0.00";
                Label9.Text = "$0.00";
                Label10.Text = "$0.00";
            }
        }

        protected void LogEvent(string msgLog)
        {
            string fn = Path.Combine(AppLogDir, AppLog);
            try
            {
                StreamWriter s;
                if (Directory.Exists(AppLogDir))
                {
                    s = new StreamWriter(fn, true, Encoding.Default);
                    s.WriteLine(DateTime.Now + " - " + msgLog);
                    s.Flush();
                    s.Close();
                }
                else
                {
                    Directory.CreateDirectory(AppLogDir);
                    File.Create(fn);
                    s = new StreamWriter(fn, true, Encoding.Default);
                    s.WriteLine(DateTime.Now + " - " + msgLog);
                    s.Flush();
                    s.Close();
                } //End If
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Log Event\r\n" + ex.Message);
            } //End Try
            // Set a variable to the My Documents path.
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\WriteLines.txt", true, Encoding.Default))
            {
                outputFile.WriteLine(msgLog);
            }
        }

        protected void GetIPAddress()
        {
            string strHostName;
            string strIPAddress;

            strHostName = System.Net.Dns.GetHostName();
            strIPAddress = Convert.ToString(System.Net.Dns.GetHostEntry(strHostName).AddressList[1]);
            LogEvent("Host Name: " + strHostName + "  IP Address: " + strIPAddress);
        }

        public DataSet SelectRows(DataSet dataSet, string queryString, string connectionString, string SQLTimeout)
        {
            LogEvent("connectionString: " + connectionString);
            LogEvent("SQLTimeout: " + SQLTimeout);
            try
            {
                if (connectionString.Contains("OMEGASQL1"))
                {
                    //Omega database connection
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = new SqlCommand(queryString, connection);
                        dataAdapter.Fill(dataSet);
                        connection.Close();
                        return dataSet;
                    } //End using
                } //End if
                else if (connectionString.Contains("OMEGABC") || connectionString.Contains("OMEGA_BC"))
                {
                    //OmegaBC database connection
                    connectionString = Replace(connectionString, "OMEGABCDATA", "OETSQL01.corp.omega-holdings.com");
                    //connectionString = connectionString.Replace("OMEGABCDATA", "10.20.31.4,1433");
                    connectionString = connectionString.Replace("USERID", "SVC_LABEL_CONNECT");
                    connectionString = connectionString.Replace("PASSWORD", "^z@zvcHKUm8T^gh63r)3KmB7bnDuZ");
#if DEBUG
                    if (!connectionString.Contains("SVC_LABEL_CONNECT"))
                    {
                        connectionString = connectionString.Replace("Integrated Security=False", "Integrated Security=True");
                    } //End if
#endif
                    connectionString = connectionString.Replace("Omega_BC20_TEST", "Omega_BC18_PROD");
                    LogEvent("Select Rows connectionString\r\n" + connectionString);
                    //queryString = "SELECT TOP (1000) * FROM [OAC$Item Category$437dbf0e-84ff-417a-965d-ed2bb9650972]"
                    //queryString = "SELECT TOP (1000) * FROM [OAC$Item Category$437dbf0e-84ff-417a-965d-ed2bb9650972]"
                    //queryString = "SELECT TOP (1000) * FROM [OAC$Sales Header$437dbf0e-84ff-417a-965d-ed2bb9650972]"
                    LogEvent("Select Rows queryString\r\n" + queryString);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = new SqlCommand(queryString, connection);
                        dataAdapter.Fill(dataSet);
                        connection.Close();
                        return dataSet;
                    } //End using
                } //End else if
                else
                {
                    //Added for Ranshu
                    OdbcCommand command = new OdbcCommand();
                    OdbcDataAdapter dataAdapter = new OdbcDataAdapter();
                    OdbcConnection connection = new OdbcConnection(connectionString);
                    command.CommandText = queryString;
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.CommandTimeout = 10000;
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dataSet);
                    connection.Close();
                    return dataSet;
                } //End if
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Select Rows " + ex.Message);
                LogEvent("Select Rows " + ex.Message);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                //dataAdapter.Fill(dataSet);
                return dataSet;
            }
            finally
            {
                LogEvent("connectionString: " + connectionString);
                LogEvent("SQLTimeout: " + SQLTimeout);
                //LogEvent("SelectCommand:\r\n" + queryString);
            }
        }

        protected string ImportSQL(string sr)
        {
            string fn = sr; //Defined in C:\Omega_Labels.xml at <BaseSQL>C:\\Item Cost Calculation.sql</BaseSQL>
            try
            {
#if DEBUG
                fn = fn.Replace("\\\\OmegaFS2", "C:");
#endif
                LogEvent("ImportSQL fn " + fn);
                if (File.Exists(fn))
                {
                    try
                    {   // Open the text file using a stream reader.
                        LogEvent("Load File: " + BaseSQL);
                        using (StreamReader filereader = new StreamReader(fn))
                        {
                            // Read the stream to a string, and write the string to the console.
                            sr = filereader.ReadToEnd();
                            LogEvent(sr); //post sql to C:\Log\Omega_Cost.txt
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Import SQL - The file could not be read:\r\n" + fn + "\r\n" + ex.Message);
                        LogEvent("Import SQL - The file could not be read:\r\n" + fn + "\r\n" + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Import SQL - Unable to find file:\r\n" + fn + "\r\n" + ex.Message);
                    LogEvent("Import SQL - Unable to find file: " + fn + "\r\n" + ex.Message);
                } //End If
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Import SQL - Error Message\r\n" + ex.Message);
                LogEvent("Import SQL - Error Message\r\n " + ex.Message);
                Quit_Cost_Calculation();
            } //End Try
            return sr;
        }

        public bool ImportXmlInit(string fn, bool status)
        {
            XmlDocument m_xmld = new XmlDocument();
            string Parsing = "ImportXmlInit";

            try
            {
#if DEBUG
                fn = fn.Replace("\\\\OmegaFS2", "C:");
#endif
                LogEvent("ImportXmlInit fn " + fn);
                if (File.Exists(fn))
                {
                    LogEvent("Load File: " + fn);
                    m_xmld.Load(fn);
                    XmlNodeList elemList;

                    LogEvent("ImportXmlInit function");

                    Parsing = "AppVersion";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing);
                    string AppVer = elemList[0].InnerText;
                    if (!AppVer.Equals(AppVersion))
                    {
                        LogEvent("Warning - Application and Omega_Cost.XML Versions Do Not Match.");
                        LogEvent("Application Version: " + AppVersion + " XML Version: " + AppVer);
                    } //End If
                    LogEvent("AppVersion: " + AppVer);

                    Parsing = "SQLConnNAV";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing);
                    daConnNAV = elemList[0].InnerText;
                    LogEvent("SQLConnNAV: " + daConnNAV);

                    Parsing = "SQLConnSOLO";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing);
                    daConnSOLO = elemList[0].InnerText;
                    LogEvent("SQLConnSOLO: " + daConnSOLO);

                    Parsing = "SQLConnBC";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing);
                    daConnBC = elemList[0].InnerText;
                    LogEvent("SQLConnBC: " + daConnBC);

                    Parsing = "SQLTimeout";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing);
                    SQLTimeout = elemList[0].InnerText;
                    LogEvent("SQLTimeout: " + SQLTimeout);

                    Parsing = "BaseSQL";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing); //Location and filename
                    BaseSQL = "";
                    BaseSQL = elemList[0].InnerText;
                    BaseSQLFile = BaseSQL;
                    LogEvent("Import BaseSQL from: " + elemList[0].InnerText);

                    Parsing = "CostDir";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing);
                    CostDir = elemList[0].InnerText;
                    LogEvent("CostDir: " + CostDir);

                    status = true;
                    LogEvent("Return Status: " + status);
                }
                else
                {
                    MessageBox.Show(this, "Settings file not found: " + fn + "\r\n" + ex.Message);
                    LogEvent("File not found: " + fn + "\r\n");
                } //End If
            } //End Try
            catch (Exception ex)
            {
                status = false;
                if (ex.Message.Contains("Could not find file "))
                {
                    MessageBox.Show(this, "Settings file not found: " + fn + "\r\n" + ex.Message);
                    LogEvent("Settings File not found: " + fn + "\r\n" + ex.Message);
                }
                else
                {
                    MessageBox.Show(this, "A fatal error was caused when parsing " + Parsing + " in file " + fn + "\r\n" + ex.Message);
                    LogEvent("A fatal error was caused when parsing " + Parsing + " in file " + fn + "\r\n" + ex.Message);
                } //End If

                Quit_Cost_Calculation();
            } //End Catch
            return status;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Label1.Text = "$0.00";
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            Label2.Text = "$0.00";
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            Label3.Text = "$0.00";
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            Label4.Text = "$0.00";
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {
            Label5.Text = "$0.00";
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
            Label6.Text = "$0.00";
        }

        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {
            Label7.Text = "$0.00";
        }

        protected void TextBox8_TextChanged(object sender, EventArgs e)
        {
            Label8.Text = "$0.00";
        }

        protected void TextBox9_TextChanged(object sender, EventArgs e)
        {
            Label9.Text = "$0.00";
        }

        protected void TextBox10_TextChanged(object sender, EventArgs e)
        {
            Label10.Text = "$0.00";
        }

    protected bool RefreshDataSet(string sTmp)
    {
        //ReDim colHeaders(100); // Setup a maximum number of columns in the table, later set it to actual number defined

        //Replace Sales Order Number in Template SQL with the one the operator entered
        string sTemp = BaseSQL;
        Int32 n = sTemp.IndexOf("A.[No_] = '");
        if (n > 0)
        { //found string
            string strTmp = "A.[No_] = '";
            n += strTmp.Length;
            Int32 m = sTemp.IndexOf("'", n);
            sTemp = sTemp.Substring(n, m - n);
            LogEvent("Replace Part # " + sTemp + " with " + sTmp);
            BaseSQL = BaseSQL.Replace(sTemp, sTmp);
        }

        m_dsWork.Clear();

        // The omegasql1 Server's Omega-NAV-2009-A.dbo and solochain databases are needed to successfully run the base querries
        // The SQL querry is imported by the application using a file name defined in BCPartImages.xml see <ConnectionString> and select SalesOrder #
        m_dsWork.DataSetName = "BCPartImages";  //Set data set name
        LogEvent("Processing Parts # " + sTmp);

        //LogEvent("BaseSQL:\r\n" + BaseSQL);
        LogEvent("Refresh Data Set daConnBC: " + daConnBC);
        LogEvent("Refresh Data Set SQLTimeout: " + SQLTimeout);
        m_dsWork = SelectRows(m_dsWork, BaseSQL, daConnBC, SQLTimeout); //Import the data from the NAV database tables
        LogEvent("Refresh Data Set m_dsWork Tables Count: " + m_dsWork.Tables.Count);

        if (!m_dsWork.HasErrors)
        {
            try
            {
                LogEvent("Dataset Column Count: " + m_dsWork.Tables["Table"].Columns.Count);
                LogEvent("Dataset Row Count: " + m_dsWork.Tables["Table"].Rows.Count);
                //MaxRow = m_dsWork.Tables.Count;
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, "Sql Problem\r\n" + ex.Message);
                LogEvent("SQL Query Problem:\r\n" + ex.Message);
                Quit_Cost_Calculation();
            }

            //Note that the DataGridView column 0 is reserved for "SELECT"

            Int32 MaxRow = m_dtWork.Rows.Count;
            Int32 iCol = 0;
            iCol += 1;

            //Initialize colHeader list from dataset, the first, colHeaders(0) item has been reserved for "Select" column index=0
            //With m_dtWork
                //ReDim colHeaders(.Columns.Count);
                //Int32 row = 0;
                for (Int32 Col = 0; Col == (m_dtWork.Columns.Count - 1); Col++)
                {
                    colHeaders[iCol] = Convert.ToString(m_dtWork.Columns[Col]);
                    //DataRow dr = m_dsWork.Tables[0].Rows[0];
                    //value = Convert.ToDouble(dr["Cost"]);
                    iCol += 1;
                }
                colHeaders[0] = "Select";
            //} //End With m_dtWork

            if (MaxRow == 0)
            {
                return false;
            } //End If - number of rows is 0

            return true;
        }
        else
        {
            MessageBox.Show(this, "Sql Problem - Dataset Has Errors");
            LogEvent("Sql Problem - Dataset Has Errors");
            return false;
        }

    }

    protected void Quit_Cost_Calculation()
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);
        //window.close();
        //BCPartImages.close();
        //this.close();
    }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        bool status = false;
        Int32 MaxRow = 0;
        Double value = 0D;

        //With m_dtWork
        if (TextBox1.Text.Equals(String.Empty))
        {
            Label1.Text = "$0.00";
            //Label11.Text = "$0.00";
            //Label21.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox1.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label1.Text = "$" + Convert.ToString(value * 1.0);
            //Label11.Text = "$" + Convert.ToString(value * 1.15);
            //Label21.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox2.Text.Equals(String.Empty))
        {
            Label2.Text = "$0.00";
            //Label12.Text = "$0.00";
            //Label22.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox2.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label2.Text = "$" + Convert.ToString(value * 1.0);
            //Label12.Text = "$" + Convert.ToString(value * 1.15);
            //Label22.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox3.Text.Equals(String.Empty))
        {
            Label3.Text = "$0.00";
            //Label13.Text = "$0.00";
            //Label23.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox3.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label3.Text = "$" + Convert.ToString(value * 1.0);
            //Label13.Text = "$" + Convert.ToString(value * 1.15);
            //Label23.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox4.Text.Equals(String.Empty))
        {
            Label4.Text = "$0.00";
            //Label14.Text = "$0.00";
            //Label24.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox4.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label4.Text = "$" + Convert.ToString(value * 1.0);
            //Label14.Text = "$" + Convert.ToString(value * 1.15);
            //Label24.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox5.Text.Equals(String.Empty))
        {
            Label5.Text = "$0.00";
            //Label15.Text = "$0.00";
            //Label25.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox5.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label5.Text = "$" + Convert.ToString(value * 1.0);
            //Label15.Text = "$" + Convert.ToString(value * 1.15);
            //Label25.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox6.Text.Equals(String.Empty))
        {
            Label6.Text = "$0.00";
            //Label16.Text = "$0.00";
            //Label26.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox6.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label6.Text = "$" + Convert.ToString(value * 1.0);
            //Label16.Text = "$" + Convert.ToString(value * 1.15);
            //Label26.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox7.Text.Equals(String.Empty))
        {
            Label7.Text = "$0.00";
            //Label17.Text = "$0.00";
            //Label27.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox7.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label7.Text = "$" + Convert.ToString(value * 1.0);
            //Label17.Text = "$" + Convert.ToString(value * 1.15);
            //Label27.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox8.Text.Equals(String.Empty))
        {
            Label8.Text = "$0.00";
            //Label18.Text = "$0.00";
            //Label28.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox8.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label8.Text = "$" + Convert.ToString(value * 1.0);
            //Label18.Text = "$" + Convert.ToString(value * 1.15);
            //Label28.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox9.Text.Equals(String.Empty))
        {
            Label9.Text = "$0.00";
            //Label19.Text = "$0.00";
            //Label29.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox9.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label9.Text = "$" + Convert.ToString(value * 1.0);
            //Label19.Text = "$" + Convert.ToString(value * 1.15);
            //Label29.Text = "$" + Convert.ToString(value * 0.15)
        }
        if (TextBox10.Text.Equals(String.Empty))
        {
            Label10.Text = "$0.00";
            //Label20.Text = "$0.00";
            //Label30.Text = "$0.00";
        }
        else
        {
            status = RefreshDataSet(TextBox10.Text);
            MaxRow = m_dtWork.Rows.Count;
            //value = m_dtWork.Rows[MaxRow - 1].Item(headerStruct.Cost - 1);
            DataRow dr = m_dsWork.Tables[0].Rows[0];
            value = Convert.ToDouble(dr["Cost"]);
            Label10.Text = "$" + Convert.ToString(value * 1.0);
            //Label20.Text = "$" + Convert.ToString(value * 1.15);
            //Label30.Text = "$" + Convert.ToString(value * 0.15)
        }
        //} //End With m_dtWork
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
    }
}
