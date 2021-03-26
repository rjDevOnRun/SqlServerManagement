using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMO
{
    public partial class Form1 : Form
    {
        /*
         * https://www.youtube.com/watch?v=npMnqcz63Cg
         */
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            try
            {
                Server dbServer = new Server(new ServerConnection(txtServer.Text, txtUID.Text, txtPwd.Text));
                Backup backup = new Backup()
                {
                    Action = BackupActionType.Database,
                    Database = txtDatabase.Text
                };
                backup.Devices.AddDevice(@"D:\DEVELOPMENT\Visual_C\SqlServerManagement\DbBackupTest.bak",
                    DeviceType.File);

                backup.Initialize = true;
                backup.PercentComplete += Backup_PercentComplete;
                backup.Complete += Backup_Complete;
                backup.SqlBackupAsync(dbServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Backup_Complete(object sender, ServerMessageEventArgs e)
        {
            if (e.Error != null)
            {
                lblStatus.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = e.Error.Message;
                });
            }
           
        }

        private void Backup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            progressBar1.Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = e.Percent;
                progressBar1.Update();
            });

            lblPercent.Invoke((MethodInvoker)delegate
            {
                lblPercent.Text = $"{e.Percent}%";
            });
        }

        private void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/7783229/create-failed-for-database-smo-c

            Server dbServer = new Server(new ServerConnection(txtServer.Text, txtUID.Text, txtPwd.Text));
            Database database = new Database(dbServer, "NewDbUsingSMO");

            try
            {
                //bool att = false;
                database.Create();
                //dbServer.Databases[0]
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.InnerException.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImpExp_Click(object sender, EventArgs e)
        {
            //https://www.codeproject.com/Tips/767161/Export-and-Import-Database-using-SMO
            // https://stackoverflow.com/questions/17063248/copying-table-from-one-sql-server-to-another

            Server sourceServer = new Server("server");
            String dbName = "database";

            // Connect to the local, default instance of SQL Server. 

            // Reference the database.  
            Database db = sourceServer.Databases[dbName];

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

        static void CreateTableFromTable(string fromConnection, string toConnection, string dbName, string tablename, bool copyData = false)
        {
            // https://stackoverflow.com/questions/17063248/copying-table-from-one-sql-server-to-another

            Server fromServer = new Server(new ServerConnection(new SqlConnection(fromConnection)));
            Database db = fromServer.Databases[dbName];

            Transfer transfer = new Transfer(db);
            transfer.CopyAllObjects = false;
            transfer.DropDestinationObjectsFirst = false;
            transfer.CopySchema = false;   //Database schema? Or Table schema? I DO NOT want to overwrite the db schema
            transfer.CopyData = copyData;
            transfer.DestinationServer = "?";
            transfer.DestinationDatabase = dbName;
            transfer.Options.IncludeIfNotExists = true;
            transfer.ObjectList.Add(db.Tables[tablename]);

            transfer.TransferData();
        }
    }
}
