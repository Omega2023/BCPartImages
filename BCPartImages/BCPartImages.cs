using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

//using System.Deployment;
//using System.Net;
//using System.Windows;

namespace BCPartImages
{
    public partial class BCPartImages : Form
    {
#if DEBUG
        string xmlfn = "C:\\Apps\\PartImages\\BCPartImages.xml";
#else
        string xmlfn = ".\BCPartImages.xml";
#endif
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet m_dsWork = new DataSet();
        DataTable m_dtWork = new DataTable();

        //Global items
        String hostName = System.Net.Dns.GetHostName();
        String AppLogDir = "C:\\Log\\";
        String AppLog = "BCPartImages.txt";

        //Omega_Images.XML file initializations
        String AppVersion = "1.0.0.0";

        string BaseSQL = String.Empty; //SQL command that gets data to fill the m_dsWork DataSet
        string BaseSQLFile = String.Empty;
        string daConnNAV = String.Empty;
        string daConnSOLO = String.Empty;
        string daConnBC = String.Empty;
        String SQLTimeout = "";
        String ImagesDir = "";

        public struct headerStruct
        {
            public static int ProductNo = 1;
            public static int Path = 2;
            public static int Images = 12;
        }

        public BCPartImages()
        {
            InitializeComponent();
            bool status = false;
            bool loaded = false;

            if (!loaded)
            {
                File.Delete(Path.Combine(AppLogDir, AppLog));
                imagePath.Text = "";
                pictureBox.ImageLocation = "Images/OET_Banner.jpg";
                loaded = true;
            }
            else
            {
                LogEvent("Page Load Reloaded");
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
            LogEvent("*** Part Images Version " + AppVersion + " ***");

            GetIPAddress(); //Get IP Address and PC Name

            LogEvent("Part Images xmlfn " + xmlfn);
            status = ImportXmlInit(xmlfn, status); //Load XML file
            LogEvent("Part Images xmlfn " + xmlfn);
            if (!status)
            {
                MessageBox.Show(this, "Cannot Load XML File:\r\n" + xmlfn);
                LogEvent("Cannot Load XML File:\r\n" + xmlfn);
                Application.Exit();
            } //End If

            BaseSQLFile = BaseSQL;
            LogEvent("Part Images BaseSQLFile " + BaseSQLFile);
            BaseSQL = ImportSQL(BaseSQLFile); //Load SQL file
            LogEvent("Part Images BaseSQL " + BaseSQL);

            txbxPartNumber.Focus();
        }

        private void txbxPartNumber_TextChanged(object sender, EventArgs e)
        {
            imagePath.Text = "";
        }

        protected void LogEvent(String msgLog)
        {
            String fn = Path.Combine(AppLogDir, AppLog);
            StreamWriter sw;

            try
            {
                if (Directory.Exists(AppLogDir))
                {
                    if (File.Exists(fn))
                    {
                        using (StreamWriter wrt = File.AppendText(fn))
                        {
                            wrt.WriteLine(DateTime.Now + " - " + msgLog);
                            wrt.Flush();
                            wrt.Close();
                        }
                    }
                    else
                    {
                        sw = new StreamWriter(fn, true, Encoding.Default);
                        sw.WriteLine(DateTime.Now + " - " + msgLog);
                        sw.Flush();
                        sw.Close();
                    } //End If File Exists
                }
                else
                {
                    Directory.CreateDirectory(AppLogDir);
                    File.Create(fn);
                    sw = new StreamWriter(fn, true, Encoding.Default);
                    sw.WriteLine(DateTime.Now + " - " + msgLog);
                    sw.Flush();
                    sw.Close();
                } //End If Directory Exists
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            } //End Try
            // Set a variable to the My Documents path.
            String mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the String array to a new file named "WriteLines.txt".
            using (StreamWriter wrt = File.AppendText(Path.Combine(AppLogDir, AppLog)))
            {
                wrt.WriteLine(DateTime.Now + " - " + msgLog);
            }
        }

        protected void GetIPAddress()
        {
            String strHostName;
            String strIPAddress;

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
                    connectionString = connectionString.Replace("OMEGABCDATA", "OETSQL01.corp.omega-holdings.com");
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

        protected string ImportSQL(String sr)
        {
            String fn = sr; //Defined in C:\Omega_Labels.xml at <BaseSQL>C:\\NAV Primary Images.sql</BaseSQL>
            try
            {
                LogEvent("Import SQL fn " + fn);
#if DEBUG
                fn = fn.Replace("\\\\OmegaFS2", "C:");
#endif
                LogEvent("Import SQL fn " + fn);
                if (File.Exists(fn))
                {
                    try
                    {   // Open the text file using a stream reader.
                        using (StreamReader filereader = new StreamReader(fn))
                        {
                            // Read the stream to a String, and write the String to the console.
                            sr = filereader.ReadToEnd();
                            LogEvent(sr); //post sql to C:\Log\Omega_Images.txt
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "The SQL query file could not be read: " + ex.Message);
                        LogEvent("The SQL query file could not be read:\r\n" + fn + "\r\n" + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Unable to find file: " + fn);
                    LogEvent("Unable to find file: " + fn);
                } //End If
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                LogEvent("Import SQL " + ex.Message);
                Application.Exit();
            } //End Try
            return sr;
        }

        public bool ImportXmlInit(String fn, bool status)
        {
            XmlDocument m_xmld = new XmlDocument();
            String Parsing = "ImportXmlInit";

            try
            {
                LogEvent("Import Xml Init fn " + fn);
#if DEBUG
                fn = fn.Replace("\\\\OmegaFS2", "C:");
#endif
                LogEvent("Import Xml Init fn " + fn);
                if (File.Exists(fn))
                {
                    LogEvent("Load File: " + fn);
                    m_xmld.Load(fn);
                    XmlNodeList elemList;

                    LogEvent("Import Xml Init function");

                    Parsing = "AppVersion";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing);
                    String AppVer = elemList[0].InnerText;
                    if (!AppVer.Equals(AppVersion))
                    {
                        LogEvent("Warning - Application and Omega_Images.XML Versions Do Not Match.");
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
                    LogEvent("Import BaseSQL from: " + elemList[0].InnerText);

                    Parsing = "ImagesDir";
                    LogEvent(Parsing);
                    elemList = m_xmld.GetElementsByTagName(Parsing);
                    ImagesDir = elemList[0].InnerText;
                    LogEvent("ImagesDir: " + ImagesDir);

                    status = true;
                    LogEvent("Return Status: " + status);
                }
                else
                {
                    MessageBox.Show(this, "Settings file not found: " + fn + "\r\n");
                    LogEvent("Settings file not found: " + fn + "\r\n");
                } //End If
            } //End Try
            catch (Exception ex)
            {
                status = false;
                if (ex.Message.Contains("Could not find file "))
                {
                    MessageBox.Show(this, "Settings file not found: " + fn + "\r\n" + ex.Message);
                    LogEvent("Settings file not found: " + fn + "\r\n" + ex.Message);
                }
                else
                {
                    MessageBox.Show(this, "A fatal error was caused when parsing " + Parsing + " in file " + fn + "\r\n" + ex.Message + "\r\n");
                    LogEvent("A fatal error was caused when parsing " + Parsing + " in file " + fn + "\r\n" + ex.Message);
                } //End If
                Application.Exit();
            } //End Catch
            return status;
        }

        protected bool RefreshDataSet(String sTmp)
        {
            Int32 iRow = 0;
            Int32 iCol = 0;
            Int32 MaxRow = 0;
            Int32 MaxCol = 0;

            //Replace Part Number in Template SQL with the one the operator entered
            String sTemp = BaseSQL;
            Int32 num = sTemp.IndexOf("A.[ProductNo] = '");
            if (num > 0)
            { //found String
                String strTmp = "A.[ProductNo] = '";
                num += strTmp.Length;
                Int32 m = sTemp.IndexOf("'", num);
                sTemp = sTemp.Substring(num, m - num);
                LogEvent("Replace Part # " + sTemp + " with " + sTmp);
                BaseSQL = BaseSQL.Replace(sTemp, sTmp);
            }

            m_dsWork.Clear();

            // The omegasql1 Server's Omega-NAV-2009-A.dbo and solochain databases are needed to successfully run the base querries
            // The SQL querry is imported by the application using a file name defined in BCPartImages.xml see <ConnectionString> and select Part #
            m_dsWork.DataSetName = "BCPartImages";  //Set data set name
            LogEvent("Processing Parts # " + sTmp);

            //LogEvent("BaseSQL:\r\n" + BaseSQL);
            LogEvent("Refresh Data Set daConnBC: " + daConnBC);
            LogEvent("Refresh Data Set SQLTimeout: " + SQLTimeout);
            //m_dsWork = SelectRows(m_dsWork, BaseSQL, daConnNAV, SQLTimeout); //Import the data from the NAV database tables
            m_dsWork = SelectRows(m_dsWork, BaseSQL, daConnBC, SQLTimeout); //Import the data from the NAV database tables
            LogEvent("Refresh Data Set m_dsWork Tables Count: " + m_dsWork.Tables.Count);
            m_dtWork = m_dsWork.Tables[0];

            if (!m_dsWork.HasErrors)
            {
                try
                {
                    MaxRow = m_dtWork.Rows.Count;
                    MaxCol = m_dtWork.Columns.Count;
                    LogEvent("btn Submit MaxRow " + MaxRow + " MaxCol " + MaxCol);
                    for (iRow = 0; iRow < MaxRow; iRow++)
                    {
                        for (iCol = 0; iCol < MaxCol; iCol++)
                        {
                            LogEvent("btn Submit m_dtWork.Rows[" + iRow + "].ItemArray[" + iCol + "] = " + m_dtWork.Rows[iRow].ItemArray[iCol]);
                        }
                    }
                    LogEvent("Dataset Column Count: " + m_dsWork.Tables["Table"].Columns.Count);
                    LogEvent("Dataset Row Count: " + m_dsWork.Tables["Table"].Rows.Count);
                    //MaxRow = m_dsWork.Tables.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Sql Query Problem: " + "\r\n" + ex.Message);
                    LogEvent("SQL Query Problem:\r\n" + ex.Message);
                    Application.Exit();
                }

                //Note that the DataGridView column 0 is reserved for "SELECT"

                MaxRow = m_dtWork.Rows.Count;
                iCol += 1;

                if (MaxRow == 0)
                {
                    return false;
                } //End If - number of rows is 0

                return true;
            }
            else
            {
                MessageBox.Show(this, "Refresh DataSet - Dataset Has Errors");
                LogEvent("Refresh DataSet - Dataset Has Errors");
                return false;
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            bool status = false;
            Int32 row = 0;
            Int32 MaxRow = 0;
            String value = "";
            String fn = "";

            try
            {
                if (txbxPartNumber.Text.Equals(String.Empty))
                {
                    imagePath.Text = "";
                }
                else
                {
                    String upperTextBox = txbxPartNumber.Text.ToUpper();
                    status = RefreshDataSet(upperTextBox);
                    MaxRow = m_dtWork.Rows.Count;

                    DataRow dr = m_dsWork.Tables[0].Rows[0];
                    MaxRow = dr.Table.Rows.Count;
                    LogEvent("btnSubmit Click MaxRow " + MaxRow);
                    for (row = 0; row < MaxRow; row++)
                    {
                        dr = m_dsWork.Tables[0].Rows[row];
                        String upperItemArray = Convert.ToString(dr.ItemArray[0]).ToUpper();
                        if (upperTextBox.Equals(upperItemArray))
                        {
                            value = Convert.ToString(dr["Path"]);
                            txbxPartNumber.Text = Convert.ToString(dr.ItemArray[0]);
                            break;
                        }
                        LogEvent("btn Submit fn " + fn);
                        //fn = Convert.ToString(dr.ItemArray[1]);
                        //if (File.Exists(fn))
                        //{
                        //    LogEvent("Image File " + fn + " For Part Number " + upperItemArray + " Found. DataRow=" + row);
                        //}
                        //else
                        //{
                        //    LogEvent("Image File " + fn + " For Part Number " + upperItemArray + " Not Found. DataRow=" + row);
                        //}
                    }
                    imagePath.Text = Convert.ToString(value);
                }
                fn = imagePath.Text;
                LogEvent("btn Submit fn " + fn);
                if (File.Exists(fn))
                {
                    if (fn.Contains("pdf"))
                    {
                        pictureBox.ImageLocation = "\\\\OmegaFS2\\NAVGIF\\NoImage.png";
                    }
                    else
                    {
                        pictureBox.ImageLocation = imagePath.Text;
                    }
                    String path = imagePath.Text;

                    if (File.Exists(path))
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                }
                else
                {
                    if (imagePath.Text.Equals(String.Empty))
                    {
                        pictureBox.ImageLocation = "";
                    }
                    else
                    {
                        pictureBox.ImageLocation = "\\\\OmegaFS2\\NAVGIF\\NoImage.png";
                    }
                } //End If
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error Path: " + fn + "\r\n" + ex.Message);
                LogEvent("Error Path: " + fn + "\r\n" + ex.Message);
            }
        }

        private void txbxPartNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.Equals(13))
            {
                bool status = false;
                Int32 MaxRow = 0;
                String value = "";
                String fn = "";
                String upperItemArray;

                try
                {
                    if (txbxPartNumber.Text.Equals(String.Empty))
                    {
                        imagePath.Text = "";
                    }
                    else
                    {
                        String upperTextBox = txbxPartNumber.Text.ToUpper();
                        status = RefreshDataSet(upperTextBox);
                        MaxRow = m_dtWork.Rows.Count;

                        DataRow dr = m_dsWork.Tables[0].Rows[0];
                        Int32 row = 0;
                        MaxRow = dr.Table.Rows.Count;
                        LogEvent("txbxPart Number KeyUp MaxRow " + MaxRow);
                        for (row = 0; row < MaxRow; row++)
                        {
                            dr = m_dsWork.Tables[0].Rows[row];
                            upperItemArray = Convert.ToString(dr.ItemArray[0]).ToUpper();
                            if (upperTextBox.Equals(upperItemArray))
                            {
                                value = Convert.ToString(dr["Path"]);
                                txbxPartNumber.Text = Convert.ToString(dr.ItemArray[0]);
                                break;
                            }
                            LogEvent("txbxPartNumber KeyUp fn " + fn);
                            //fn = Convert.ToString(dr.ItemArray[1]);
                            //LogEvent("txbxPart Number KeyUp fn " + fn);
                            //if (File.Exists(fn))
                            //{
                            //    LogEvent("Image File " + fn + " For Part Number " + upperItemArray + " Found. DataRow=" + row);
                            //}
                            //else
                            //{
                            //    LogEvent("Image File " + fn + " For Part Number " + upperItemArray + " Not Found. DataRow=" + row);
                            //}
                        }
                        imagePath.Text = Convert.ToString(value);
                    }
                    fn = imagePath.Text;
                    LogEvent("txbxPart Number KeyUp fn " + fn);
                    if (File.Exists(fn))
                    {
                        if (fn.Contains("pdf"))
                        {
                            pictureBox.ImageLocation = "\\\\OmegaFS2\\NAVGIF\\NoImage.png";
                        }
                        else
                        {
                            pictureBox.ImageLocation = imagePath.Text;
                        }
                        String path = imagePath.Text;

                        if (File.Exists(path))
                        {
                            System.Diagnostics.Process.Start(path);
                        }
                    }
                    else
                    {
                        pictureBox.ImageLocation = "\\\\OmegaFS2\\NAVGIF\\NoImage.png";
                    } //End If
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error Path: " + fn + "\r\n" + ex.Message);
                    LogEvent("Error Path: " + fn + "\r\n" + ex.Message);
                }
            }
        }

        //public static void Exit() { }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
