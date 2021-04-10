using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using DataProvider = Microsoft.Data.ConnectionUI.DataProvider;

namespace SMO
{
    public partial class ImportExportDb : Form
    {
        public ImportExportDb()
        {
            InitializeComponent();
        }

        private void btnImpExp_Click(object sender, EventArgs e)
        {
            SmoServerInfo ServerConnectionData = new SmoServerInfo();

            // Get Source info
            GetDbConnectioninfo("Source", ref ServerConnectionData);
            if (string.IsNullOrEmpty(ServerConnectionData.SourceServer)) return;

            // Get destination info
            GetDbConnectioninfo("Destination", ref ServerConnectionData);
            if (string.IsNullOrEmpty(ServerConnectionData.DestinationServer)) return;

            DoImportExport(ServerConnectionData);
        }

        ///<reference>
        ///     https://docs.microsoft.com/en-us/sql/relational-databases/server-management-objects-smo/tasks/transferring-data?view=sql-server-ver15
        ///     https://stackoverflow.com/questions/17063248/copying-table-from-one-sql-server-to-another
        ///</reference>
        private void DoImportExport(SmoServerInfo ServerConnectionData)
        {
            //Method1(ServerConnectionData);
            Method2(ServerConnectionData);
        }

        private void Method2(SmoServerInfo ServerConnectionData)
        {
            // Connect to source SQL Server. 
            Server srv = new Server(new ServerConnection(new SqlConnection(ServerConnectionData.SourceServerConnection)));
            // Reference the database.  
            Database db = srv.Databases[ServerConnectionData.SourceDatabase];

            //Server srv;
            //srv = new Server();
            ////Reference the AdventureWorks2012 database   
            //Database db;
            //db = srv.Databases["AdventureWorks2012"];

            //Create a destination database that is to be destination database.   
            string destinationDbname = ServerConnectionData.DestinationDatabase + "Copy1";
            Database dbCopy;
            dbCopy = new Database(srv, destinationDbname);
            dbCopy.Create();

            //Define a Transfer object and set the required options and properties.   
            Transfer transfer;
            transfer = new Transfer(db);
            transfer.DataTransferEvent += Xfr_DataTransferEvent;
            transfer.Options.ContinueScriptingOnError = true;

            transfer.CopyAllTables = true;
            transfer.CopyAllUsers = true;
            transfer.Options.WithDependencies = true;

            transfer.DestinationDatabase = destinationDbname;
            transfer.DestinationServer = srv.Name;
            transfer.DestinationLoginSecure = true;

            transfer.CopySchema = true;
            transfer.CopyData = true;
            
            try
            {
                transfer.TransferData();
            }
            catch (Exception ex)
            {
                string errMsg = string.Empty;

                if (ex.InnerException.InnerException != null)
                {
                    errMsg = ex.InnerException.InnerException.Message;
                }
                else
                {
                    errMsg = ex.Message;
                }

                MessageBox.Show(errMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = errMsg;
                });
            }

            ////Script the transfer. Alternatively perform immediate data transfer   
            //// with TransferData method.   
            //xfr.ScriptTransfer();

        }

        private void Xfr_DataTransferEvent(object sender, DataTransferEventArgs e)
        {
            lblStatus.Invoke((MethodInvoker)delegate
            {
                lblStatus.Text = e.Message;
            });
        }

        private static void Method1(SmoServerInfo ServerConnectionData)
        {
            // Connect to source SQL Server. 
            Server sourceServer = new Server(new ServerConnection(new SqlConnection(ServerConnectionData.SourceServerConnection)));
            // Reference the database.  
            Database db = sourceServer.Databases[ServerConnectionData.SourceDatabase];
            //Database db = sourceServer.Databases[dbName];

            // Define a Scripter object and set the required scripting options. 
            Scripter scripter = new Scripter(sourceServer);
            scripter.Options.ScriptDrops = false;
            scripter.Options.WithDependencies = true;
            scripter.Options.Indexes = true;   // To include indexes
            scripter.Options.DriAllConstraints = true;   // to include referential constraints in the script

            // Iterate through the tables in database and script each one. Display the script.   
            foreach (Table tb in db.Tables)
            {
                // check if the table is not a system table
                if (tb.IsSystemObject == false)
                {
                    Console.WriteLine("-- Scripting for table " + tb.Name);

                    // Generating script for table tb
                    System.Collections.Specialized.StringCollection sc = scripter.Script(new Urn[] { tb.Urn });
                    foreach (string st in sc)
                    {
                        //ado.net to destination 
                        Console.WriteLine(st);//SqlCommand.ExecuteNonQuery();
                    }
                    Console.WriteLine("--");
                }
            }
        }

        private void GetDbConnectioninfo(string databaseOriginType, ref SmoServerInfo smoServerInfo)
        {
            string connectionString = string.Empty;
            string ServerName = string.Empty;
            string DatabaseName = string.Empty;

            using (var dialog = new DataConnectionDialog())
            {
                // dialog settings
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.TopLevel = true;
                dialog.Title = $"Choose the {databaseOriginType} database";
                dialog.ChangeDataSourceTitle = $"Choose the {databaseOriginType} database";

                // If you want the user to select from any of the available data sources, do this:
                DataSource.AddStandardDataSources(dialog);
                
                // OR, if you want only certain data sources to be available
                // (e.g. only SQL Server), do something like this instead: 
                dialog.DataSources.Add(DataSource.SqlDataSource);
                dialog.DataSources.Add(DataSource.SqlFileDataSource);
                dialog.SelectedDataSource = DataSource.SqlDataSource;
                dialog.SelectedDataProvider = DataProvider.SqlDataProvider;

                // The way how you show the dialog is somewhat unorthodox; `dialog.ShowDialog()`
                // would throw a `NotSupportedException`. Do it this way instead:
                DialogResult userChoice = DataConnectionDialog.Show(dialog);

                // Return the resulting connection string if a connection was selected:
                if (userChoice == DialogResult.OK)
                {
                    connectionString = dialog.ConnectionString;

                    using (DbConnection connection = new SqlConnection(connectionString))
                    {
                        ServerName = connection.DataSource.ToUpper();
                        DatabaseName = connection.Database;
                    }
                }
                else
                {
                    MessageBox.Show("Error happened");
                }
            }

            if (databaseOriginType.Equals("Source", StringComparison.OrdinalIgnoreCase))
            {
                smoServerInfo.SourceServerConnection = connectionString;
                smoServerInfo.SourceServer = ServerName;
                smoServerInfo.SourceDatabase = DatabaseName;
            }
            else
            {
                smoServerInfo.DestinationServerConnection = connectionString;
                smoServerInfo.DestinationServer = ServerName;
                smoServerInfo.DestinationDatabase = DatabaseName;
            }

        }
    }

    public class SmoServerInfo
    {
        public string SourceServerConnection { get; set; }
        public string SourceServer { get; set; }
        public string SourceDatabase { get; set; }

        public string DestinationServerConnection { get; set; }
        public string DestinationServer { get; set; }
        public string DestinationDatabase { get; set; }

        public SmoServerInfo()
        { }
    }
}
