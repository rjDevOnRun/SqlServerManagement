
namespace SMO
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnBackup = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.btnCreateDatabase = new System.Windows.Forms.Button();
            this.btnImpExp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(151, 31);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(415, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "RJI5PC";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(151, 72);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(415, 20);
            this.txtDatabase.TabIndex = 3;
            this.txtDatabase.Text = "TestSMO";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUID
            // 
            this.txtUID.Location = new System.Drawing.Point(151, 113);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(415, 20);
            this.txtUID.TabIndex = 5;
            this.txtUID.Text = "sa";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "User name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(151, 160);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(415, 20);
            this.txtPwd.TabIndex = 7;
            this.txtPwd.Text = "Ladyinblue@123";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(23, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(26, 235);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(540, 18);
            this.progressBar1.TabIndex = 8;
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(26, 202);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(119, 27);
            this.btnBackup.TabIndex = 9;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblStatus.Location = new System.Drawing.Point(23, 256);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(543, 21);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status:";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPercent.Location = new System.Drawing.Point(154, 209);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(21, 13);
            this.lblPercent.TabIndex = 11;
            this.lblPercent.Text = "0%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCreateDatabase
            // 
            this.btnCreateDatabase.Location = new System.Drawing.Point(322, 202);
            this.btnCreateDatabase.Name = "btnCreateDatabase";
            this.btnCreateDatabase.Size = new System.Drawing.Size(119, 27);
            this.btnCreateDatabase.TabIndex = 12;
            this.btnCreateDatabase.Text = "New Database";
            this.btnCreateDatabase.UseVisualStyleBackColor = true;
            this.btnCreateDatabase.Click += new System.EventHandler(this.btnCreateDatabase_Click);
            // 
            // btnImpExp
            // 
            this.btnImpExp.Location = new System.Drawing.Point(447, 202);
            this.btnImpExp.Name = "btnImpExp";
            this.btnImpExp.Size = new System.Drawing.Size(119, 27);
            this.btnImpExp.TabIndex = 13;
            this.btnImpExp.Text = "Import/Export";
            this.btnImpExp.UseVisualStyleBackColor = true;
            this.btnImpExp.Click += new System.EventHandler(this.btnImpExp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 290);
            this.Controls.Add(this.btnImpExp);
            this.Controls.Add(this.btnCreateDatabase);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sql Server Management App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Button btnCreateDatabase;
        private System.Windows.Forms.Button btnImpExp;
    }
}

