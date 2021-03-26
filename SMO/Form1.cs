using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMO
{
    public partial class Form1 : Form
    {
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
                //Database database = new Database();
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
    }
}
