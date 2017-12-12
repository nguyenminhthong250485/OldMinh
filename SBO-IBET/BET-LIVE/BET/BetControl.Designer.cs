namespace BET
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
            this.components = new System.ComponentModel.Container();
            this.dud_SboName = new System.Windows.Forms.DomainUpDown();
            this.dud_IbetName = new System.Windows.Forms.DomainUpDown();
            this.cb_OuH1 = new System.Windows.Forms.CheckBox();
            this.cb_HdpH1 = new System.Windows.Forms.CheckBox();
            this.cb_OuFT = new System.Windows.Forms.CheckBox();
            this.cb_HdpFT = new System.Windows.Forms.CheckBox();
            this.cb_Live = new System.Windows.Forms.CheckBox();
            this.btnBet = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.bt_Stop = new System.Windows.Forms.Button();
            this.txtGiaDoSbo = new System.Windows.Forms.TextBox();
            this.tb_IpSbo = new System.Windows.Forms.TextBox();
            this.txtGiaDoIbet = new System.Windows.Forms.TextBox();
            this.txtIPIbet = new System.Windows.Forms.TextBox();
            this.lblSboStatus = new System.Windows.Forms.Label();
            this.lblIbetStatus = new System.Windows.Forms.Label();
            this.ttBet = new System.Windows.Forms.ToolTip(this.components);
            this.dud_Style = new System.Windows.Forms.DomainUpDown();
            this.lb_GroupSbo = new System.Windows.Forms.Label();
            this.lb_GroupIbet = new System.Windows.Forms.Label();
            this.richOutput = new System.Windows.Forms.RichTextBox();
            this.BetControlNumber = new System.Windows.Forms.GroupBox();
            this.cb_NonLive = new System.Windows.Forms.CheckBox();
            this.lb_Money = new System.Windows.Forms.Label();
            this.dud_Profit = new System.Windows.Forms.DomainUpDown();
            this.dud_Leauge = new System.Windows.Forms.DomainUpDown();
            this.BetControlNumber.SuspendLayout();
            this.SuspendLayout();
            // 
            // dud_SboName
            // 
            this.dud_SboName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dud_SboName.Location = new System.Drawing.Point(9, 16);
            this.dud_SboName.Margin = new System.Windows.Forms.Padding(2);
            this.dud_SboName.Name = "dud_SboName";
            this.dud_SboName.ReadOnly = true;
            this.dud_SboName.Size = new System.Drawing.Size(133, 23);
            this.dud_SboName.TabIndex = 4;
            this.dud_SboName.SelectedItemChanged += new System.EventHandler(this.dud_SboName_SelectedItemChanged);
            // 
            // dud_IbetName
            // 
            this.dud_IbetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dud_IbetName.Location = new System.Drawing.Point(296, 15);
            this.dud_IbetName.Margin = new System.Windows.Forms.Padding(2);
            this.dud_IbetName.Name = "dud_IbetName";
            this.dud_IbetName.ReadOnly = true;
            this.dud_IbetName.Size = new System.Drawing.Size(133, 23);
            this.dud_IbetName.TabIndex = 15;
            this.dud_IbetName.SelectedItemChanged += new System.EventHandler(this.dud_IbetName_SelectedItemChanged);
            // 
            // cb_OuH1
            // 
            this.cb_OuH1.AutoSize = true;
            this.cb_OuH1.Checked = true;
            this.cb_OuH1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_OuH1.Location = new System.Drawing.Point(210, 45);
            this.cb_OuH1.Margin = new System.Windows.Forms.Padding(2);
            this.cb_OuH1.Name = "cb_OuH1";
            this.cb_OuH1.Size = new System.Drawing.Size(54, 17);
            this.cb_OuH1.TabIndex = 72;
            this.cb_OuH1.Text = "OuH1";
            this.cb_OuH1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_OuH1.UseVisualStyleBackColor = true;
            // 
            // cb_HdpH1
            // 
            this.cb_HdpH1.AutoSize = true;
            this.cb_HdpH1.Checked = true;
            this.cb_HdpH1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_HdpH1.Location = new System.Drawing.Point(145, 45);
            this.cb_HdpH1.Margin = new System.Windows.Forms.Padding(2);
            this.cb_HdpH1.Name = "cb_HdpH1";
            this.cb_HdpH1.Size = new System.Drawing.Size(60, 17);
            this.cb_HdpH1.TabIndex = 71;
            this.cb_HdpH1.Text = "HdpH1";
            this.cb_HdpH1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_HdpH1.UseVisualStyleBackColor = true;
            // 
            // cb_OuFT
            // 
            this.cb_OuFT.AutoSize = true;
            this.cb_OuFT.Checked = true;
            this.cb_OuFT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_OuFT.Location = new System.Drawing.Point(75, 45);
            this.cb_OuFT.Margin = new System.Windows.Forms.Padding(2);
            this.cb_OuFT.Name = "cb_OuFT";
            this.cb_OuFT.Size = new System.Drawing.Size(51, 17);
            this.cb_OuFT.TabIndex = 70;
            this.cb_OuFT.Text = "ouFT";
            this.cb_OuFT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_OuFT.UseVisualStyleBackColor = true;
            // 
            // cb_HdpFT
            // 
            this.cb_HdpFT.AutoSize = true;
            this.cb_HdpFT.Checked = true;
            this.cb_HdpFT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_HdpFT.Location = new System.Drawing.Point(10, 45);
            this.cb_HdpFT.Margin = new System.Windows.Forms.Padding(2);
            this.cb_HdpFT.Name = "cb_HdpFT";
            this.cb_HdpFT.Size = new System.Drawing.Size(59, 17);
            this.cb_HdpFT.TabIndex = 69;
            this.cb_HdpFT.Text = "HdpFT";
            this.cb_HdpFT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_HdpFT.UseVisualStyleBackColor = true;
            // 
            // cb_Live
            // 
            this.cb_Live.AutoSize = true;
            this.cb_Live.Checked = true;
            this.cb_Live.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Live.Location = new System.Drawing.Point(400, 105);
            this.cb_Live.Margin = new System.Windows.Forms.Padding(2);
            this.cb_Live.Name = "cb_Live";
            this.cb_Live.Size = new System.Drawing.Size(46, 17);
            this.cb_Live.TabIndex = 73;
            this.cb_Live.Text = "Live";
            this.cb_Live.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_Live.UseVisualStyleBackColor = true;
            // 
            // btnBet
            // 
            this.btnBet.Enabled = false;
            this.btnBet.Location = new System.Drawing.Point(75, 75);
            this.btnBet.Margin = new System.Windows.Forms.Padding(2);
            this.btnBet.Name = "btnBet";
            this.btnBet.Size = new System.Drawing.Size(53, 23);
            this.btnBet.TabIndex = 76;
            this.btnBet.Text = "Bet";
            this.ttBet.SetToolTip(this.btnBet, "Bet");
            this.btnBet.UseVisualStyleBackColor = true;
            this.btnBet.Click += new System.EventHandler(this.btnBet_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(10, 75);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(53, 23);
            this.btnLogin.TabIndex = 75;
            this.btnLogin.Text = "Login";
            this.ttBet.SetToolTip(this.btnLogin, "Login");
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // bt_Stop
            // 
            this.bt_Stop.Location = new System.Drawing.Point(150, 75);
            this.bt_Stop.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Stop.Name = "bt_Stop";
            this.bt_Stop.Size = new System.Drawing.Size(53, 23);
            this.bt_Stop.TabIndex = 77;
            this.bt_Stop.Text = "Stop";
            this.bt_Stop.UseVisualStyleBackColor = true;
            this.bt_Stop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtGiaDoSbo
            // 
            this.txtGiaDoSbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiaDoSbo.Location = new System.Drawing.Point(251, 15);
            this.txtGiaDoSbo.Margin = new System.Windows.Forms.Padding(2);
            this.txtGiaDoSbo.Name = "txtGiaDoSbo";
            this.txtGiaDoSbo.ReadOnly = true;
            this.txtGiaDoSbo.Size = new System.Drawing.Size(28, 23);
            this.txtGiaDoSbo.TabIndex = 79;
            // 
            // tb_IpSbo
            // 
            this.tb_IpSbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_IpSbo.Location = new System.Drawing.Point(146, 15);
            this.tb_IpSbo.Margin = new System.Windows.Forms.Padding(2);
            this.tb_IpSbo.Name = "tb_IpSbo";
            this.tb_IpSbo.ReadOnly = true;
            this.tb_IpSbo.Size = new System.Drawing.Size(101, 23);
            this.tb_IpSbo.TabIndex = 78;
            // 
            // txtGiaDoIbet
            // 
            this.txtGiaDoIbet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiaDoIbet.Location = new System.Drawing.Point(538, 14);
            this.txtGiaDoIbet.Margin = new System.Windows.Forms.Padding(2);
            this.txtGiaDoIbet.Name = "txtGiaDoIbet";
            this.txtGiaDoIbet.ReadOnly = true;
            this.txtGiaDoIbet.Size = new System.Drawing.Size(28, 23);
            this.txtGiaDoIbet.TabIndex = 81;
            // 
            // txtIPIbet
            // 
            this.txtIPIbet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIPIbet.Location = new System.Drawing.Point(433, 14);
            this.txtIPIbet.Margin = new System.Windows.Forms.Padding(2);
            this.txtIPIbet.Name = "txtIPIbet";
            this.txtIPIbet.ReadOnly = true;
            this.txtIPIbet.Size = new System.Drawing.Size(101, 23);
            this.txtIPIbet.TabIndex = 80;
            // 
            // lblSboStatus
            // 
            this.lblSboStatus.AutoSize = true;
            this.lblSboStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSboStatus.Location = new System.Drawing.Point(350, 80);
            this.lblSboStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSboStatus.Name = "lblSboStatus";
            this.lblSboStatus.Size = new System.Drawing.Size(31, 16);
            this.lblSboStatus.TabIndex = 82;
            this.lblSboStatus.Text = "Out";
            // 
            // lblIbetStatus
            // 
            this.lblIbetStatus.AutoSize = true;
            this.lblIbetStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIbetStatus.Location = new System.Drawing.Point(480, 80);
            this.lblIbetStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIbetStatus.Name = "lblIbetStatus";
            this.lblIbetStatus.Size = new System.Drawing.Size(31, 16);
            this.lblIbetStatus.TabIndex = 83;
            this.lblIbetStatus.Text = "Out";
            // 
            // dud_Style
            // 
            this.dud_Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dud_Style.Location = new System.Drawing.Point(466, 41);
            this.dud_Style.Margin = new System.Windows.Forms.Padding(2);
            this.dud_Style.Name = "dud_Style";
            this.dud_Style.Size = new System.Drawing.Size(100, 23);
            this.dud_Style.TabIndex = 84;
            // 
            // lb_GroupSbo
            // 
            this.lb_GroupSbo.AutoSize = true;
            this.lb_GroupSbo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lb_GroupSbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_GroupSbo.Location = new System.Drawing.Point(228, 80);
            this.lb_GroupSbo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_GroupSbo.Name = "lb_GroupSbo";
            this.lb_GroupSbo.Size = new System.Drawing.Size(50, 16);
            this.lb_GroupSbo.TabIndex = 85;
            this.lb_GroupSbo.Text = "Group";
            // 
            // lb_GroupIbet
            // 
            this.lb_GroupIbet.AutoSize = true;
            this.lb_GroupIbet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lb_GroupIbet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_GroupIbet.Location = new System.Drawing.Point(293, 80);
            this.lb_GroupIbet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_GroupIbet.Name = "lb_GroupIbet";
            this.lb_GroupIbet.Size = new System.Drawing.Size(50, 16);
            this.lb_GroupIbet.TabIndex = 86;
            this.lb_GroupIbet.Text = "Group";
            // 
            // richOutput
            // 
            this.richOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richOutput.Location = new System.Drawing.Point(0, 130);
            this.richOutput.Margin = new System.Windows.Forms.Padding(2);
            this.richOutput.Name = "richOutput";
            this.richOutput.Size = new System.Drawing.Size(633, 100);
            this.richOutput.TabIndex = 87;
            this.richOutput.Text = "";
            // 
            // BetControlNumber
            // 
            this.BetControlNumber.Controls.Add(this.cb_NonLive);
            this.BetControlNumber.Controls.Add(this.lb_Money);
            this.BetControlNumber.Controls.Add(this.dud_Profit);
            this.BetControlNumber.Controls.Add(this.dud_Leauge);
            this.BetControlNumber.Controls.Add(this.dud_SboName);
            this.BetControlNumber.Controls.Add(this.lb_GroupIbet);
            this.BetControlNumber.Controls.Add(this.dud_IbetName);
            this.BetControlNumber.Controls.Add(this.lb_GroupSbo);
            this.BetControlNumber.Controls.Add(this.cb_HdpFT);
            this.BetControlNumber.Controls.Add(this.bt_Stop);
            this.BetControlNumber.Controls.Add(this.cb_OuFT);
            this.BetControlNumber.Controls.Add(this.btnBet);
            this.BetControlNumber.Controls.Add(this.cb_HdpH1);
            this.BetControlNumber.Controls.Add(this.btnLogin);
            this.BetControlNumber.Controls.Add(this.dud_Style);
            this.BetControlNumber.Controls.Add(this.cb_OuH1);
            this.BetControlNumber.Controls.Add(this.lblIbetStatus);
            this.BetControlNumber.Controls.Add(this.cb_Live);
            this.BetControlNumber.Controls.Add(this.lblSboStatus);
            this.BetControlNumber.Controls.Add(this.txtGiaDoIbet);
            this.BetControlNumber.Controls.Add(this.tb_IpSbo);
            this.BetControlNumber.Controls.Add(this.txtIPIbet);
            this.BetControlNumber.Controls.Add(this.txtGiaDoSbo);
            this.BetControlNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BetControlNumber.Location = new System.Drawing.Point(0, 0);
            this.BetControlNumber.Margin = new System.Windows.Forms.Padding(2);
            this.BetControlNumber.Name = "BetControlNumber";
            this.BetControlNumber.Padding = new System.Windows.Forms.Padding(2);
            this.BetControlNumber.Size = new System.Drawing.Size(633, 130);
            this.BetControlNumber.TabIndex = 89;
            this.BetControlNumber.TabStop = false;
            this.BetControlNumber.Text = "0";
            // 
            // cb_NonLive
            // 
            this.cb_NonLive.AutoSize = true;
            this.cb_NonLive.Checked = true;
            this.cb_NonLive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_NonLive.Location = new System.Drawing.Point(500, 105);
            this.cb_NonLive.Margin = new System.Windows.Forms.Padding(2);
            this.cb_NonLive.Name = "cb_NonLive";
            this.cb_NonLive.Size = new System.Drawing.Size(66, 17);
            this.cb_NonLive.TabIndex = 90;
            this.cb_NonLive.Text = "NonLive";
            this.cb_NonLive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_NonLive.UseVisualStyleBackColor = true;
            // 
            // lb_Money
            // 
            this.lb_Money.AutoSize = true;
            this.lb_Money.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lb_Money.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Money.Location = new System.Drawing.Point(10, 110);
            this.lb_Money.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Money.Name = "lb_Money";
            this.lb_Money.Size = new System.Drawing.Size(39, 13);
            this.lb_Money.TabIndex = 89;
            this.lb_Money.Text = "Money";
            // 
            // dud_Profit
            // 
            this.dud_Profit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dud_Profit.Location = new System.Drawing.Point(358, 41);
            this.dud_Profit.Margin = new System.Windows.Forms.Padding(2);
            this.dud_Profit.Name = "dud_Profit";
            this.dud_Profit.Size = new System.Drawing.Size(40, 23);
            this.dud_Profit.TabIndex = 88;
            // 
            // dud_Leauge
            // 
            this.dud_Leauge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dud_Leauge.Location = new System.Drawing.Point(412, 41);
            this.dud_Leauge.Margin = new System.Windows.Forms.Padding(2);
            this.dud_Leauge.Name = "dud_Leauge";
            this.dud_Leauge.Size = new System.Drawing.Size(40, 23);
            this.dud_Leauge.TabIndex = 87;
            // 
            // BetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BetControlNumber);
            this.Controls.Add(this.richOutput);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BetControl";
            this.Size = new System.Drawing.Size(633, 230);
            this.Load += new System.EventHandler(this.BetControl_Load);
            this.BetControlNumber.ResumeLayout(false);
            this.BetControlNumber.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DomainUpDown dud_SboName;
        private System.Windows.Forms.DomainUpDown dud_IbetName;
        private System.Windows.Forms.CheckBox cb_OuH1;
        private System.Windows.Forms.CheckBox cb_HdpH1;
        private System.Windows.Forms.CheckBox cb_OuFT;
        private System.Windows.Forms.CheckBox cb_HdpFT;
        private System.Windows.Forms.CheckBox cb_Live;
        public System.Windows.Forms.Button btnBet;
        public System.Windows.Forms.Button btnLogin;
        public System.Windows.Forms.Button bt_Stop;
        private System.Windows.Forms.TextBox txtGiaDoSbo;
        private System.Windows.Forms.TextBox tb_IpSbo;
        private System.Windows.Forms.TextBox txtGiaDoIbet;
        private System.Windows.Forms.TextBox txtIPIbet;
        private System.Windows.Forms.Label lblSboStatus;
        private System.Windows.Forms.Label lblIbetStatus;
        private System.Windows.Forms.ToolTip ttBet;
        private System.Windows.Forms.DomainUpDown dud_Style;
        private System.Windows.Forms.Label lb_GroupSbo;
        private System.Windows.Forms.Label lb_GroupIbet;
        public System.Windows.Forms.RichTextBox richOutput;
        private System.Windows.Forms.GroupBox BetControlNumber;
        private System.Windows.Forms.DomainUpDown dud_Leauge;
        private System.Windows.Forms.DomainUpDown dud_Profit;
        private System.Windows.Forms.Label lb_Money;
        private System.Windows.Forms.CheckBox cb_NonLive;
    }
}
