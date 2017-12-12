namespace COMPARE
{
    partial class CompareControl
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
            this.components = new System.ComponentModel.Container();
            this.rtb_CompareNonLive = new System.Windows.Forms.RichTextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.btnPre = new System.Windows.Forms.Button();
            this.tTCompare = new System.Windows.Forms.ToolTip(this.components);
            this.udProfit = new System.Windows.Forms.DomainUpDown();
            this.btnCompareName = new System.Windows.Forms.Button();
            this.groupName = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtb_CompareMatch = new System.Windows.Forms.RichTextBox();
            this.rtb_CompareLive = new System.Windows.Forms.RichTextBox();
            this.rtb_Login = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIBET = new System.Windows.Forms.TextBox();
            this.txtSBO = new System.Windows.Forms.TextBox();
            this.radNonLive = new System.Windows.Forms.RadioButton();
            this.radLive = new System.Windows.Forms.RadioButton();
            this.lblCountLive = new System.Windows.Forms.Label();
            this.lblCountNonLive = new System.Windows.Forms.Label();
            this.bt_Login = new System.Windows.Forms.Button();
            this.btnCompareOddOldVersion = new System.Windows.Forms.Button();
            this.groupName.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_CompareNonLive
            // 
            this.rtb_CompareNonLive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_CompareNonLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_CompareNonLive.Location = new System.Drawing.Point(289, 154);
            this.rtb_CompareNonLive.Margin = new System.Windows.Forms.Padding(2);
            this.rtb_CompareNonLive.Name = "rtb_CompareNonLive";
            this.rtb_CompareNonLive.ReadOnly = true;
            this.rtb_CompareNonLive.Size = new System.Drawing.Size(668, 148);
            this.rtb_CompareNonLive.TabIndex = 22;
            this.rtb_CompareNonLive.Text = "";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(573, 9);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 40);
            this.btnNext.TabIndex = 72;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.bt_Next_Click);
            // 
            // txtTime
            // 
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Location = new System.Drawing.Point(393, 16);
            this.txtTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(100, 26);
            this.txtTime.TabIndex = 71;
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(498, 9);
            this.btnPre.Margin = new System.Windows.Forms.Padding(2);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(70, 40);
            this.btnPre.TabIndex = 70;
            this.btnPre.Text = "Pre";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.bt_Pre_Click);
            // 
            // udProfit
            // 
            this.udProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udProfit.Location = new System.Drawing.Point(261, 16);
            this.udProfit.Margin = new System.Windows.Forms.Padding(2);
            this.udProfit.Name = "udProfit";
            this.udProfit.Size = new System.Drawing.Size(100, 26);
            this.udProfit.TabIndex = 74;
            this.udProfit.Text = "Profit";
            this.udProfit.SelectedItemChanged += new System.EventHandler(this.udProfit_SelectedItemChanged);
            // 
            // btnCompareName
            // 
            this.btnCompareName.Location = new System.Drawing.Point(801, 9);
            this.btnCompareName.Margin = new System.Windows.Forms.Padding(2);
            this.btnCompareName.Name = "btnCompareName";
            this.btnCompareName.Size = new System.Drawing.Size(150, 40);
            this.btnCompareName.TabIndex = 75;
            this.btnCompareName.Text = "Compare Name";
            this.btnCompareName.UseVisualStyleBackColor = true;
            this.btnCompareName.Click += new System.EventHandler(this.bt_CompareName_Click);
            // 
            // groupName
            // 
            this.groupName.Controls.Add(this.tableLayoutPanel1);
            this.groupName.Controls.Add(this.panel1);
            this.groupName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupName.Location = new System.Drawing.Point(0, 0);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(965, 423);
            this.groupName.TabIndex = 78;
            this.groupName.TabStop = false;
            this.groupName.Text = "Compare";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.rtb_CompareMatch, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtb_CompareLive, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtb_Login, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rtb_CompareNonLive, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 116);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(959, 304);
            this.tableLayoutPanel1.TabIndex = 87;
            // 
            // rtb_CompareMatch
            // 
            this.rtb_CompareMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_CompareMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_CompareMatch.Location = new System.Drawing.Point(3, 3);
            this.rtb_CompareMatch.Name = "rtb_CompareMatch";
            this.rtb_CompareMatch.Size = new System.Drawing.Size(281, 146);
            this.rtb_CompareMatch.TabIndex = 83;
            this.rtb_CompareMatch.Text = "";
            // 
            // rtb_CompareLive
            // 
            this.rtb_CompareLive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_CompareLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_CompareLive.Location = new System.Drawing.Point(289, 2);
            this.rtb_CompareLive.Margin = new System.Windows.Forms.Padding(2);
            this.rtb_CompareLive.Name = "rtb_CompareLive";
            this.rtb_CompareLive.ReadOnly = true;
            this.rtb_CompareLive.Size = new System.Drawing.Size(668, 148);
            this.rtb_CompareLive.TabIndex = 85;
            this.rtb_CompareLive.Text = "";
            // 
            // rtb_Login
            // 
            this.rtb_Login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Login.Location = new System.Drawing.Point(3, 155);
            this.rtb_Login.Name = "rtb_Login";
            this.rtb_Login.Size = new System.Drawing.Size(281, 146);
            this.rtb_Login.TabIndex = 82;
            this.rtb_Login.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtIBET);
            this.panel1.Controls.Add(this.txtSBO);
            this.panel1.Controls.Add(this.radNonLive);
            this.panel1.Controls.Add(this.radLive);
            this.panel1.Controls.Add(this.lblCountLive);
            this.panel1.Controls.Add(this.lblCountNonLive);
            this.panel1.Controls.Add(this.bt_Login);
            this.panel1.Controls.Add(this.btnCompareOddOldVersion);
            this.panel1.Controls.Add(this.btnPre);
            this.panel1.Controls.Add(this.txtTime);
            this.panel1.Controls.Add(this.btnCompareName);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.udProfit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 94);
            this.panel1.TabIndex = 86;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(872, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 89;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 88;
            this.label2.Text = "IBET";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 88;
            this.label1.Text = "SBO";
            // 
            // txtIBET
            // 
            this.txtIBET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIBET.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIBET.Location = new System.Drawing.Point(454, 65);
            this.txtIBET.Name = "txtIBET";
            this.txtIBET.Size = new System.Drawing.Size(412, 20);
            this.txtIBET.TabIndex = 87;
            // 
            // txtSBO
            // 
            this.txtSBO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSBO.Location = new System.Drawing.Point(191, 65);
            this.txtSBO.Name = "txtSBO";
            this.txtSBO.Size = new System.Drawing.Size(205, 20);
            this.txtSBO.TabIndex = 87;
            // 
            // radNonLive
            // 
            this.radNonLive.AutoSize = true;
            this.radNonLive.Checked = true;
            this.radNonLive.Location = new System.Drawing.Point(83, 11);
            this.radNonLive.Name = "radNonLive";
            this.radNonLive.Size = new System.Drawing.Size(91, 24);
            this.radNonLive.TabIndex = 86;
            this.radNonLive.TabStop = true;
            this.radNonLive.Text = "NonLive";
            this.radNonLive.UseVisualStyleBackColor = true;
            // 
            // radLive
            // 
            this.radLive.AutoSize = true;
            this.radLive.Location = new System.Drawing.Point(7, 11);
            this.radLive.Name = "radLive";
            this.radLive.Size = new System.Drawing.Size(59, 24);
            this.radLive.TabIndex = 86;
            this.radLive.TabStop = true;
            this.radLive.Text = "Live";
            this.radLive.UseVisualStyleBackColor = true;
            this.radLive.CheckedChanged += new System.EventHandler(this.radLive_CheckedChanged);
            // 
            // lblCountLive
            // 
            this.lblCountLive.AutoSize = true;
            this.lblCountLive.ForeColor = System.Drawing.Color.Red;
            this.lblCountLive.Location = new System.Drawing.Point(47, 71);
            this.lblCountLive.Name = "lblCountLive";
            this.lblCountLive.Size = new System.Drawing.Size(19, 20);
            this.lblCountLive.TabIndex = 85;
            this.lblCountLive.Text = "0";
            // 
            // lblCountNonLive
            // 
            this.lblCountNonLive.AutoSize = true;
            this.lblCountNonLive.ForeColor = System.Drawing.Color.Red;
            this.lblCountNonLive.Location = new System.Drawing.Point(4, 71);
            this.lblCountNonLive.Name = "lblCountNonLive";
            this.lblCountNonLive.Size = new System.Drawing.Size(19, 20);
            this.lblCountNonLive.TabIndex = 85;
            this.lblCountNonLive.Text = "0";
            // 
            // bt_Login
            // 
            this.bt_Login.Location = new System.Drawing.Point(179, 11);
            this.bt_Login.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Login.Name = "bt_Login";
            this.bt_Login.Size = new System.Drawing.Size(70, 40);
            this.bt_Login.TabIndex = 84;
            this.bt_Login.Text = "Login";
            this.bt_Login.UseVisualStyleBackColor = true;
            this.bt_Login.Click += new System.EventHandler(this.bt_Login_Click);
            // 
            // btnCompareOddOldVersion
            // 
            this.btnCompareOddOldVersion.Location = new System.Drawing.Point(647, 9);
            this.btnCompareOddOldVersion.Margin = new System.Windows.Forms.Padding(2);
            this.btnCompareOddOldVersion.Name = "btnCompareOddOldVersion";
            this.btnCompareOddOldVersion.Size = new System.Drawing.Size(150, 40);
            this.btnCompareOddOldVersion.TabIndex = 80;
            this.btnCompareOddOldVersion.Text = "Compare Odd";
            this.btnCompareOddOldVersion.UseVisualStyleBackColor = true;
            this.btnCompareOddOldVersion.Click += new System.EventHandler(this.btnCompareOddOldVersion_Click);
            // 
            // CompareControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CompareControl";
            this.Size = new System.Drawing.Size(965, 423);
            this.Load += new System.EventHandler(this.CompareControl_Load);
            this.groupName.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtb_CompareNonLive;
        public System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtTime;
        public System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.ToolTip tTCompare;
        private System.Windows.Forms.DomainUpDown udProfit;
        private System.Windows.Forms.Button btnCompareName;
        private System.Windows.Forms.GroupBox groupName;
        public System.Windows.Forms.Button btnCompareOddOldVersion;
        private System.Windows.Forms.RichTextBox rtb_Login;
        private System.Windows.Forms.RichTextBox rtb_CompareMatch;
        public System.Windows.Forms.Button bt_Login;
        private System.Windows.Forms.RichTextBox rtb_CompareLive;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCountNonLive;
        private System.Windows.Forms.Label lblCountLive;
        private System.Windows.Forms.RadioButton radNonLive;
        private System.Windows.Forms.RadioButton radLive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIBET;
        private System.Windows.Forms.TextBox txtSBO;
        private System.Windows.Forms.Button btnSave;
    }
}
