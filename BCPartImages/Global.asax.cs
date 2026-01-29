using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using BCPartImages;

using System.ComponentModel; // CancelEventArgs
using System.Data;
using System.Data.SqlClient;
using System.Deployment;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Xml;

namespace BCPartImages
{
    public class Global : HttpApplication
    {
#if DEBUG
        //public string xmlfn = "C:\\Apps\\BCPartImages\\BCPartImages.xml";
        public string xmlfn = "\\\\OmegaFS2\\Apps\\BCPartImages\\BCPartImages.xml";
#else
        public string xmlfn = "\\\\OmegaFS2\\Apps\\BCPartImages\\BCPartImages.xml";
#endif
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet m_dsWork = new DataSet();
        DataTable m_dtWork = new DataTable();

        //Global items
        //string ConnectionString = "";
        public string hostName = System.Net.Dns.GetHostName();
        public string AppLogDir = "C:\\Log\\";
        public string AppLog = "BCPartImages.txt";
        //string MyApplication = "Parts Images";

        //Omega_Cost.XML file initializations
        public string AppVersion = "1.0.0.0";

        public string BaseSQL = String.Empty; //SQL command that gets data to fill the m_dsWork DataSet
        public string BaseSQLFile = String.Empty;
        public string daConnNAV = String.Empty;
        public string daConnSOLO = String.Empty;
        public string daConnBC = String.Empty;
        public string SQLTimeout = "";
        public string CostDir = "";
        public string[] colHeaders = new string[100];

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            if (xmlfn == null)
            {
                xmlfn = "\\\\OmegaFS2\\Apps\\BCPartImages\\BCPartImages.xml";
            }
            if (AppLogDir == null)
            {
                AppLogDir = "C:\\Log\\";
            }
            if (AppLog == null)
            {
                AppLog = "BCPartImages.txt";
            }
            if (AppVersion == null)
            {
                AppVersion = "1.0.0.0";
            }
            if (BaseSQL == null)
            {
                BaseSQL = ""; //SQL command that gets data to fill the m_dsWork DataSet
            }
            if (daConnNAV == null)
            {
                daConnNAV = "";
            }
            if (daConnSOLO == null)
            {
                daConnSOLO = "";
            }
            if (daConnBC == null)
            {
                daConnBC = "";
            }
            if (SQLTimeout == null)
            {
                SQLTimeout = "";
            }
            if (CostDir == null)
            {
                CostDir = "";
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
