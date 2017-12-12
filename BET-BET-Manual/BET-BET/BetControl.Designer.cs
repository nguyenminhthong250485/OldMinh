namespace BET_BET
{
    partial class BetControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLoginSBO = new System.Windows.Forms.Button();
            this.dataGridViewSBO = new System.Windows.Forms.DataGridView();
            this.groupSBO = new System.Windows.Forms.GroupBox();
            this.picSBO = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupIBET = new System.Windows.Forms.GroupBox();
            this.dataGridViewIBET = new System.Windows.Forms.DataGridView();
            this.picIBET = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoginIBET = new System.Windows.Forms.Button();
            this.btnBet = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richChienThuat = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBetUnder = new System.Windows.Forms.Button();
            this.btnBetLive = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSheet = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.richLog = new System.Windows.Forms.RichTextBox();
            this.chkDuoi = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSBO)).BeginInit();
            this.groupSBO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSBO)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupIBET.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIBET)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIBET)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoginSBO
            // 
            this.btnLoginSBO.Location = new System.Drawing.Point(3, 3);
            this.btnLoginSBO.Name = "btnLoginSBO";
            this.btnLoginSBO.Size = new System.Drawing.Size(75, 23);
            this.btnLoginSBO.TabIndex = 0;
            this.btnLoginSBO.Text = "Login";
            this.btnLoginSBO.UseVisualStyleBackColor = true;
            this.btnLoginSBO.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // dataGridViewSBO
            // 
            this.dataGridViewSBO.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewSBO.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewSBO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSBO.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewSBO.Name = "dataGridViewSBO";
            this.dataGridViewSBO.RowHeadersVisible = false;
            this.dataGridViewSBO.Size = new System.Drawing.Size(666, 332);
            this.dataGridViewSBO.TabIndex = 0;
            this.dataGridViewSBO.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSBO_CellClick);
            // 
            // groupSBO
            // 
            this.groupSBO.Controls.Add(this.dataGridViewSBO);
            this.groupSBO.Controls.Add(this.picSBO);
            this.groupSBO.Controls.Add(this.panel1);
            this.groupSBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSBO.Location = new System.Drawing.Point(680, 3);
            this.groupSBO.Name = "groupSBO";
            this.groupSBO.Size = new System.Drawing.Size(672, 380);
            this.groupSBO.TabIndex = 7;
            this.groupSBO.TabStop = false;
            this.groupSBO.Text = "SBO";
            // 
            // picSBO
            // 
            this.picSBO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSBO.Enabled = false;
            this.picSBO.Image = global::BET_BET.Properties.Resources.progress_bar;
            this.picSBO.Location = new System.Drawing.Point(417, 0);
            this.picSBO.Name = "picSBO";
            this.picSBO.Size = new System.Drawing.Size(210, 13);
            this.picSBO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSBO.TabIndex = 3;
            this.picSBO.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLoginSBO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 348);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 29);
            this.panel1.TabIndex = 7;
            // 
            // groupIBET
            // 
            this.groupIBET.Controls.Add(this.dataGridViewIBET);
            this.groupIBET.Controls.Add(this.picIBET);
            this.groupIBET.Controls.Add(this.panel2);
            this.groupIBET.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupIBET.Location = new System.Drawing.Point(3, 3);
            this.groupIBET.Name = "groupIBET";
            this.groupIBET.Size = new System.Drawing.Size(671, 380);
            this.groupIBET.TabIndex = 8;
            this.groupIBET.TabStop = false;
            this.groupIBET.Text = "IBET";
            // 
            // dataGridViewIBET
            // 
            this.dataGridViewIBET.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewIBET.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewIBET.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIBET.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewIBET.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewIBET.Name = "dataGridViewIBET";
            this.dataGridViewIBET.RowHeadersVisible = false;
            this.dataGridViewIBET.Size = new System.Drawing.Size(665, 332);
            this.dataGridViewIBET.TabIndex = 0;
            this.dataGridViewIBET.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewIBET_CellClick);
            // 
            // picIBET
            // 
            this.picIBET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picIBET.Enabled = false;
            this.picIBET.Image = global::BET_BET.Properties.Resources.progress_bar;
            this.picIBET.Location = new System.Drawing.Point(415, 0);
            this.picIBET.Name = "picIBET";
            this.picIBET.Size = new System.Drawing.Size(210, 13);
            this.picIBET.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picIBET.TabIndex = 3;
            this.picIBET.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnLoginIBET);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 348);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(665, 29);
            this.panel2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(95, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mode: iw (IBET WIN), sw (SBO WIN) - w, l (bet sbo or bet ibet)";
            // 
            // btnLoginIBET
            // 
            this.btnLoginIBET.Location = new System.Drawing.Point(3, 3);
            this.btnLoginIBET.Name = "btnLoginIBET";
            this.btnLoginIBET.Size = new System.Drawing.Size(75, 23);
            this.btnLoginIBET.TabIndex = 0;
            this.btnLoginIBET.Text = "Login";
            this.btnLoginIBET.UseVisualStyleBackColor = true;
            this.btnLoginIBET.Click += new System.EventHandler(this.btnLoginIBET_Click);
            // 
            // btnBet
            // 
            this.btnBet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBet.Location = new System.Drawing.Point(1276, 20);
            this.btnBet.Name = "btnBet";
            this.btnBet.Size = new System.Drawing.Size(121, 23);
            this.btnBet.TabIndex = 9;
            this.btnBet.Text = "BET NON LIVE";
            this.btnBet.UseVisualStyleBackColor = true;
            this.btnBet.Click += new System.EventHandler(this.btnBET_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Controls.Add(this.richChienThuat);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.richLog);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1404, 649);
            this.panel3.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupIBET, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupSBO, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 67);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1355, 386);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // richChienThuat
            // 
            this.richChienThuat.Dock = System.Windows.Forms.DockStyle.Right;
            this.richChienThuat.Location = new System.Drawing.Point(1355, 67);
            this.richChienThuat.Name = "richChienThuat";
            this.richChienThuat.Size = new System.Drawing.Size(47, 386);
            this.richChienThuat.TabIndex = 0;
            this.richChienThuat.Text = "1,2\n2,1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDuoi);
            this.groupBox1.Controls.Add(this.btnBetUnder);
            this.groupBox1.Controls.Add(this.btnBetLive);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBet);
            this.groupBox1.Controls.Add(this.cboSheet);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1402, 67);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // btnBetUnder
            // 
            this.btnBetUnder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBetUnder.Location = new System.Drawing.Point(1014, 20);
            this.btnBetUnder.Name = "btnBetUnder";
            this.btnBetUnder.Size = new System.Drawing.Size(121, 23);
            this.btnBetUnder.TabIndex = 21;
            this.btnBetUnder.Text = "BET UNDER";
            this.btnBetUnder.UseVisualStyleBackColor = true;
            this.btnBetUnder.Click += new System.EventHandler(this.btnBetUnder_Click);
            // 
            // btnBetLive
            // 
            this.btnBetLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBetLive.Location = new System.Drawing.Point(1141, 20);
            this.btnBetLive.Name = "btnBetLive";
            this.btnBetLive.Size = new System.Drawing.Size(121, 23);
            this.btnBetLive.TabIndex = 8;
            this.btnBetLive.Text = "BET LIVE";
            this.btnBetLive.UseVisualStyleBackColor = true;
            this.btnBetLive.Click += new System.EventHandler(this.btnBetLive_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sheet:";
            // 
            // cboSheet
            // 
            this.cboSheet.FormattingEnabled = true;
            this.cboSheet.Location = new System.Drawing.Point(93, 24);
            this.cboSheet.Name = "cboSheet";
            this.cboSheet.Size = new System.Drawing.Size(94, 21);
            this.cboSheet.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(192, 22);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // richLog
            // 
            this.richLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLog.Location = new System.Drawing.Point(0, 453);
            this.richLog.Name = "richLog";
            this.richLog.Size = new System.Drawing.Size(1402, 194);
            this.richLog.TabIndex = 1;
            this.richLog.Text = "";
            // 
            // chkDuoi
            // 
            this.chkDuoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDuoi.AutoSize = true;
            this.chkDuoi.Location = new System.Drawing.Point(1040, 45);
            this.chkDuoi.Name = "chkDuoi";
            this.chkDuoi.Size = new System.Drawing.Size(72, 17);
            this.chkDuoi.TabIndex = 22;
            this.chkDuoi.Text = "Only Duoi";
            this.chkDuoi.UseVisualStyleBackColor = true;
            // 
            // BetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Name = "BetControl";
            this.Size = new System.Drawing.Size(1404, 649);
            this.Load += new System.EventHandler(this.BetControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSBO)).EndInit();
            this.groupSBO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSBO)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupIBET.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIBET)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIBET)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLoginSBO;
        private System.Windows.Forms.DataGridView dataGridViewSBO;
        private System.Windows.Forms.GroupBox groupSBO;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picSBO;
        private System.Windows.Forms.GroupBox groupIBET;
        private System.Windows.Forms.PictureBox picIBET;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLoginIBET;
        private System.Windows.Forms.DataGridView dataGridViewIBET;
        private System.Windows.Forms.Button btnBet;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cboSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richChienThuat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBetLive;
        private System.Windows.Forms.Button btnBetUnder;
        private System.Windows.Forms.CheckBox chkDuoi;
    }
}
