namespace BET_BET
{
    partial class frmAddAcc
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
            this.bt_AddAcc = new System.Windows.Forms.Button();
            this.bt_AddAccNew = new System.Windows.Forms.Button();
            this.rtb_Data = new System.Windows.Forms.RichTextBox();
            this.tb_Ip = new System.Windows.Forms.TextBox();
            this.bt_UpdateIp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_AddAcc
            // 
            this.bt_AddAcc.Location = new System.Drawing.Point(12, 12);
            this.bt_AddAcc.Name = "bt_AddAcc";
            this.bt_AddAcc.Size = new System.Drawing.Size(119, 23);
            this.bt_AddAcc.TabIndex = 4;
            this.bt_AddAcc.Text = "Add Acc";
            this.bt_AddAcc.UseVisualStyleBackColor = true;
            this.bt_AddAcc.Click += new System.EventHandler(this.bt_AddAcc_Click);
            // 
            // bt_AddAccNew
            // 
            this.bt_AddAccNew.Location = new System.Drawing.Point(182, 12);
            this.bt_AddAccNew.Name = "bt_AddAccNew";
            this.bt_AddAccNew.Size = new System.Drawing.Size(119, 23);
            this.bt_AddAccNew.TabIndex = 5;
            this.bt_AddAccNew.Text = "Add Acc New";
            this.bt_AddAccNew.UseVisualStyleBackColor = true;
            this.bt_AddAccNew.Click += new System.EventHandler(this.bt_AddAccNew_Click);
            // 
            // rtb_Data
            // 
            this.rtb_Data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_Data.Location = new System.Drawing.Point(0, 64);
            this.rtb_Data.Name = "rtb_Data";
            this.rtb_Data.Size = new System.Drawing.Size(1297, 506);
            this.rtb_Data.TabIndex = 6;
            this.rtb_Data.Text = "";
            // 
            // tb_Ip
            // 
            this.tb_Ip.BackColor = System.Drawing.SystemColors.Info;
            this.tb_Ip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tb_Ip.Location = new System.Drawing.Point(0, 44);
            this.tb_Ip.Name = "tb_Ip";
            this.tb_Ip.Size = new System.Drawing.Size(1297, 20);
            this.tb_Ip.TabIndex = 7;
            // 
            // bt_UpdateIp
            // 
            this.bt_UpdateIp.Location = new System.Drawing.Point(339, 12);
            this.bt_UpdateIp.Name = "bt_UpdateIp";
            this.bt_UpdateIp.Size = new System.Drawing.Size(119, 23);
            this.bt_UpdateIp.TabIndex = 8;
            this.bt_UpdateIp.Text = "Update Ip";
            this.bt_UpdateIp.UseVisualStyleBackColor = true;
            this.bt_UpdateIp.Click += new System.EventHandler(this.bt_UpdateIp_Click);
            // 
            // frmAddAcc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 570);
            this.Controls.Add(this.bt_UpdateIp);
            this.Controls.Add(this.tb_Ip);
            this.Controls.Add(this.rtb_Data);
            this.Controls.Add(this.bt_AddAccNew);
            this.Controls.Add(this.bt_AddAcc);
            this.Name = "frmAddAcc";
            this.Text = "AddAcc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_AddAcc;
        private System.Windows.Forms.Button bt_AddAccNew;
        private System.Windows.Forms.RichTextBox rtb_Data;
        private System.Windows.Forms.TextBox tb_Ip;
        private System.Windows.Forms.Button bt_UpdateIp;
    }
}