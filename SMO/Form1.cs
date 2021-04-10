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
                backup.Devices.AddDevice(@"D:\DEVELOPMENT\DATABASES\Backups\SqlServerManagement\DbBackupTest.bak",
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
            Database database = new Database(dbServer, txtDatabase.Text);

            try
            {
                //bool att = false;
                database.Create();

                lblStatus.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = $"Success: {txtDatabase.Text} database created!";
                });
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
        }

        private void btnImpExp_Click(object sender, EventArgs e)
        {
            ImportExportDb importExport = new ImportExportDb();
            importExport.ShowDialog();
            
            ////https://www.codeproject.com/Tips/767161/Export-and-Import-Database-using-SMO
            //// https://stackoverflow.com/questions/17063248/copying-table-from-one-sql-server-to-another

            
        }

        static void CreateTableFromTable(string fromConnection, string toConnection, string dbName, string tablename, bool copyData = false)
        {
            // https://stackoverflow.com/questions/17063248/copying-table-from-one-sql-server-to-another

            Server sourceDbServer = new Server(new ServerConnection(new SqlConnection(fromConnection)));
            Database sourceDatabase = sourceDbServer.Databases[dbName];

            Transfer transfer = new Transfer(sourceDatabase);
            transfer.CopyAllObjects = false;
            transfer.DropDestinationObjectsFirst = false;
            transfer.CopySchema = false;   //Database schema? Or Table schema? I DO NOT want to overwrite the db schema
            transfer.CopyData = copyData;
            transfer.DestinationServer = "?";
            transfer.DestinationDatabase = dbName;
            transfer.Options.IncludeIfNotExists = true;
            transfer.ObjectList.Add(sourceDatabase.Tables[tablename]);

            transfer.TransferData();
        }
    }
}
