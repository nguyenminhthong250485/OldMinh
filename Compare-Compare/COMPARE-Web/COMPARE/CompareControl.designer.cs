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
            this.dud_SboName = new System.Windows.Forms.DomainUpDown();
            this.dm_UserNameIbet = new System.Windows.Forms.DomainUpDown();
            this.bt_CompareOdd = new System.Windows.Forms.Button();
            this.rtb_Info = new System.Windows.Forms.RichTextBox();
            this.cb_Live = new System.Windows.Forms.CheckBox();
            this.cb_HdpFT = new System.Windows.Forms.CheckBox();
            this.cb_OuFT = new System.Windows.Forms.CheckBox();
            this.cb_HdpH1 = new System.Windows.Forms.CheckBox();
            this.cb_OuH1 = new System.Windows.Forms.CheckBox();
            this.cb_NonLive = new System.Windows.Forms.CheckBox();
            this.bt_Next = new System.Windows.Forms.Button();
            this.tb_Time = new System.Windows.Forms.TextBox();
            this.bt_Pre = new System.Windows.Forms.Button();
            this.tTCompare = new System.Windows.Forms.ToolTip(this.components);
            this.dm_Profit = new System.Windows.Forms.DomainUpDown();
            this.bt_CompareName = new System.Windows.Forms.Button();
            this.cb_IdLive = new System.Windows.Forms.CheckBox();
            this.lb_Group = new System.Windows.Forms.Label();
            this.groupName = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCompareOddOldVersion = new System.Windows.Forms.Button();
            this.lb_Info = new System.Windows.Forms.Label();
            this.cb_IdNonLive = new System.Windows.Forms.CheckBox();
            this.groupName.SuspendLayout();
            this.SuspendLayout();
            // 
            // dud_SboName
            // 
            this.dud_SboName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dud_SboName.Location = new System.Drawing.Point(22, 31);
            this.dud_SboName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dud_SboName.Name = "dud_SboName";
            this.dud_SboName.Size = new System.Drawing.Size(266, 38);
            this.dud_SboName.TabIndex = 3;
            // 
            // dm_UserNameIbet
            // 
            this.dm_UserNameIbet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dm_UserNameIbet.Location = new System.Drawing.Point(302, 31);
            this.dm_UserNameIbet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dm_UserNameIbet.Name = "dm_UserNameIbet";
            this.dm_UserNameIbet.Size = new System.Drawing.Size(266, 38);
            this.dm_UserNameIbet.TabIndex = 14;
            // 
            // bt_CompareOdd
            // 
            this.bt_CompareOdd.Location = new System.Drawing.Point(206, 206);
            this.bt_CompareOdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_CompareOdd.Name = "bt_CompareOdd";
            this.bt_CompareOdd.Size = new System.Drawing.Size(166, 44);
            this.bt_CompareOdd.TabIndex = 16;
            this.bt_CompareOdd.Text = "Compare Odd";
            this.bt_CompareOdd.UseVisualStyleBackColor = true;
            this.bt_CompareOdd.Click += new System.EventHandler(this.bt_CompareOdd_Click);
            // 
            // rtb_Info
            // 
            this.rtb_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Info.Location = new System.Drawing.Point(576, 27);
            this.rtb_Info.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtb_Info.Name = "rtb_Info";
            this.rtb_Info.ReadOnly = true;
            this.rtb_Info.Size = new System.Drawing.Size(612, 358);
            this.rtb_Info.TabIndex = 22;
            this.rtb_Info.Text = "";
            // 
            // cb_Live
            // 
            this.cb_Live.AutoSize = true;
            this.cb_Live.Location = new System.Drawing.Point(34, 88);
            this.cb_Live.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_Live.Name = "cb_Live";
            this.cb_Live.Size = new System.Drawing.Size(84, 29);
            this.cb_Live.TabIndex = 64;
            this.cb_Live.Text = "Live";
            this.cb_Live.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_Live.UseVisualStyleBackColor = true;
            // 
            // cb_HdpFT
            // 
            this.cb_HdpFT.AutoSize = true;
            this.cb_HdpFT.Checked = true;
            this.cb_HdpFT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_HdpFT.Location = new System.Drawing.Point(34, 144);
            this.cb_HdpFT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_HdpFT.Name = "cb_HdpFT";
            this.cb_HdpFT.Size = new System.Drawing.Size(109, 29);
            this.cb_HdpFT.TabIndex = 65;
            this.cb_HdpFT.Text = "HdpFT";
            this.cb_HdpFT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_HdpFT.UseVisualStyleBackColor = true;
            // 
            // cb_OuFT
            // 
            this.cb_OuFT.AutoSize = true;
            this.cb_OuFT.Checked = true;
            this.cb_OuFT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_OuFT.Location = new System.Drawing.Point(170, 144);
            this.cb_OuFT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_OuFT.Name = "cb_OuFT";
            this.cb_OuFT.Size = new System.Drawing.Size(94, 29);
            this.cb_OuFT.TabIndex = 66;
            this.cb_OuFT.Text = "ouFT";
            this.cb_OuFT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_OuFT.UseVisualStyleBackColor = true;
            // 
            // cb_HdpH1
            // 
            this.cb_HdpH1.AutoSize = true;
            this.cb_HdpH1.Checked = true;
            this.cb_HdpH1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_HdpH1.Location = new System.Drawing.Point(302, 144);
            this.cb_HdpH1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_HdpH1.Name = "cb_HdpH1";
            this.cb_HdpH1.Size = new System.Drawing.Size(110, 29);
            this.cb_HdpH1.TabIndex = 67;
            this.cb_HdpH1.Text = "HdpH1";
            this.cb_HdpH1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_HdpH1.UseVisualStyleBackColor = true;
            // 
            // cb_OuH1
            // 
            this.cb_OuH1.AutoSize = true;
            this.cb_OuH1.Checked = true;
            this.cb_OuH1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_OuH1.Location = new System.Drawing.Point(424, 144);
            this.cb_OuH1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_OuH1.Name = "cb_OuH1";
            this.cb_OuH1.Size = new System.Drawing.Size(99, 29);
            this.cb_OuH1.TabIndex = 68;
            this.cb_OuH1.Text = "OuH1";
            this.cb_OuH1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_OuH1.UseVisualStyleBackColor = true;
            // 
            // cb_NonLive
            // 
            this.cb_NonLive.AutoSize = true;
            this.cb_NonLive.Checked = true;
            this.cb_NonLive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_NonLive.Location = new System.Drawing.Point(168, 88);
            this.cb_NonLive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_NonLive.Name = "cb_NonLive";
            this.cb_NonLive.Size = new System.Drawing.Size(123, 29);
            this.cb_NonLive.TabIndex = 69;
            this.cb_NonLive.Text = "NonLive";
            this.cb_NonLive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_NonLive.UseVisualStyleBackColor = true;
            // 
            // bt_Next
            // 
            this.bt_Next.Location = new System.Drawing.Point(382, 269);
            this.bt_Next.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_Next.Name = "bt_Next";
            this.bt_Next.Size = new System.Drawing.Size(106, 44);
            this.bt_Next.TabIndex = 72;
            this.bt_Next.Text = "Next";
            this.bt_Next.UseVisualStyleBackColor = true;
            this.bt_Next.Click += new System.EventHandler(this.bt_Next_Click);
            // 
            // tb_Time
            // 
            this.tb_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Time.Location = new System.Drawing.Point(22, 269);
            this.tb_Time.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_Time.Name = "tb_Time";
            this.tb_Time.Size = new System.Drawing.Size(198, 38);
            this.tb_Time.TabIndex = 71;
            // 
            // bt_Pre
            // 
            this.bt_Pre.Location = new System.Drawing.Point(248, 269);
            this.bt_Pre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_Pre.Name = "bt_Pre";
            this.bt_Pre.Size = new System.Drawing.Size(106, 44);
            this.bt_Pre.TabIndex = 70;
            this.bt_Pre.Text = "Pre";
            this.bt_Pre.UseVisualStyleBackColor = true;
            this.bt_Pre.Click += new System.EventHandler(this.bt_Pre_Click);
            // 
            // dm_Profit
            // 
            this.dm_Profit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dm_Profit.Location = new System.Drawing.Point(434, 81);
            this.dm_Profit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dm_Profit.Name = "dm_Profit";
            this.dm_Profit.Size = new System.Drawing.Size(134, 38);
            this.dm_Profit.TabIndex = 74;
            this.dm_Profit.Text = "Profit";
            // 
            // bt_CompareName
            // 
            this.bt_CompareName.Location = new System.Drawing.Point(22, 206);
            this.bt_CompareName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_CompareName.Name = "bt_CompareName";
            this.bt_CompareName.Size = new System.Drawing.Size(176, 44);
            this.bt_CompareName.TabIndex = 75;
            this.bt_CompareName.Text = "Compare Name";
            this.bt_CompareName.UseVisualStyleBackColor = true;
            this.bt_CompareName.Click += new System.EventHandler(this.bt_CompareName_Click);
            // 
            // cb_IdLive
            // 
            this.cb_IdLive.AutoSize = true;
            this.cb_IdLive.Checked = true;
            this.cb_IdLive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_IdLive.Location = new System.Drawing.Point(34, 333);
            this.cb_IdLive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_IdLive.Name = "cb_IdLive";
            this.cb_IdLive.Size = new System.Drawing.Size(113, 29);
            this.cb_IdLive.TabIndex = 76;
            this.cb_IdLive.Text = "Id_Live";
            this.cb_IdLive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_IdLive.UseVisualStyleBackColor = true;
            // 
            // lb_Group
            // 
            this.lb_Group.AutoSize = true;
            this.lb_Group.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Group.Location = new System.Drawing.Point(450, 325);
            this.lb_Group.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Group.Name = "lb_Group";
            this.lb_Group.Size = new System.Drawing.Size(112, 37);
            this.lb_Group.TabIndex = 77;
            this.lb_Group.Text = "Group";
            this.lb_Group.Click += new System.EventHandler(this.lb_Group_Click);
            // 
            // groupName
            // 
            this.groupName.Controls.Add(this.button1);
            this.groupName.Controls.Add(this.btnCompareOddOldVersion);
            this.groupName.Controls.Add(this.lb_Info);
            this.groupName.Controls.Add(this.cb_IdNonLive);
            this.groupName.Controls.Add(this.dud_SboName);
            this.groupName.Controls.Add(this.lb_Group);
            this.groupName.Controls.Add(this.dm_UserNameIbet);
            this.groupName.Controls.Add(this.cb_IdLive);
            this.groupName.Controls.Add(this.bt_CompareOdd);
            this.groupName.Controls.Add(this.bt_CompareName);
            this.groupName.Controls.Add(this.rtb_Info);
            this.groupName.Controls.Add(this.dm_Profit);
            this.groupName.Controls.Add(this.cb_Live);
            this.groupName.Controls.Add(this.cb_HdpFT);
            this.groupName.Controls.Add(this.bt_Next);
            this.groupName.Controls.Add(this.cb_OuFT);
            this.groupName.Controls.Add(this.tb_Time);
            this.groupName.Controls.Add(this.cb_HdpH1);
            this.groupName.Controls.Add(this.bt_Pre);
            this.groupName.Controls.Add(this.cb_OuH1);
            this.groupName.Controls.Add(this.cb_NonLive);
            this.groupName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupName.Location = new System.Drawing.Point(0, 0);
            this.groupName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupName.Name = "groupName";
            this.groupName.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupName.Size = new System.Drawing.Size(1202, 398);
            this.groupName.TabIndex = 78;
            this.groupName.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(578, 342);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 44);
            this.button1.TabIndex = 81;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCompareOddOldVersion
            // 
            this.btnCompareOddOldVersion.Location = new System.Drawing.Point(382, 206);
            this.btnCompareOddOldVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCompareOddOldVersion.Name = "btnCompareOddOldVersion";
            this.btnCompareOddOldVersion.Size = new System.Drawing.Size(166, 44);
            this.btnCompareOddOldVersion.TabIndex = 80;
            this.btnCompareOddOldVersion.Text = "Compare Odd";
            this.btnCompareOddOldVersion.UseVisualStyleBackColor = true;
            this.btnCompareOddOldVersion.Visible = false;
            this.btnCompareOddOldVersion.Click += new System.EventHandler(this.btnCompareOddOldVersion_Click);
            // 
            // lb_Info
            // 
            this.lb_Info.AutoSize = true;
            this.lb_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Info.Location = new System.Drawing.Point(30, 362);
            this.lb_Info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Info.Name = "lb_Info";
            this.lb_Info.Size = new System.Drawing.Size(52, 26);
            this.lb_Info.TabIndex = 79;
            this.lb_Info.Text = "Info";
            // 
            // cb_IdNonLive
            // 
            this.cb_IdNonLive.AutoSize = true;
            this.cb_IdNonLive.Checked = true;
            this.cb_IdNonLive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_IdNonLive.Location = new System.Drawing.Point(164, 331);
            this.cb_IdNonLive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_IdNonLive.Name = "cb_IdNonLive";
            this.cb_IdNonLive.Size = new System.Drawing.Size(152, 29);
            this.cb_IdNonLive.TabIndex = 78;
            this.cb_IdNonLive.Text = "Id_NonLive";
            this.cb_IdNonLive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_IdNonLive.UseVisualStyleBackColor = true;
            // 
            // CompareControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupName);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CompareControl";
            this.Size = new System.Drawing.Size(1202, 398);
            this.Load += new System.EventHandler(this.CompareControl_Load);
            this.groupName.ResumeLayout(false);
            this.groupName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtb_Info;
        private System.Windows.Forms.CheckBox cb_Live;
        private System.Windows.Forms.CheckBox cb_HdpFT;
        private System.Windows.Forms.CheckBox cb_OuFT;
        private System.Windows.Forms.CheckBox cb_HdpH1;
        private System.Windows.Forms.CheckBox cb_OuH1;
        private System.Windows.Forms.CheckBox cb_NonLive;
        public System.Windows.Forms.Button bt_Next;
        private System.Windows.Forms.TextBox tb_Time;
        public System.Windows.Forms.Button bt_Pre;
        private System.Windows.Forms.ToolTip tTCompare;
        private System.Windows.Forms.DomainUpDown dm_Profit;
        private System.Windows.Forms.Button bt_CompareName;
        private System.Windows.Forms.CheckBox cb_IdLive;
        private System.Windows.Forms.Label lb_Group;
        private System.Windows.Forms.GroupBox groupName;
        public System.Windows.Forms.Button bt_CompareOdd;
        public System.Windows.Forms.DomainUpDown dud_SboName;
        public System.Windows.Forms.DomainUpDown dm_UserNameIbet;
        private System.Windows.Forms.CheckBox cb_IdNonLive;
        private System.Windows.Forms.Label lb_Info;
        public System.Windows.Forms.Button btnCompareOddOldVersion;
        private System.Windows.Forms.Button button1;
    }
}
