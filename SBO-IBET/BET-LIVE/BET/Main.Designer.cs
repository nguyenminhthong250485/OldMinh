namespace BET
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tt_Main = new System.Windows.Forms.ToolTip(this.components);
            this.btnLoginAuto = new System.Windows.Forms.Button();
            this.bt_Quit = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.bt_Next = new System.Windows.Forms.Button();
            this.btnBetOne = new System.Windows.Forms.Button();
            this.bt_DelBetList = new System.Windows.Forms.Button();
            this.btnBetAuto = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBetAccountSetting = new System.Windows.Forms.GroupBox();
            this.dm_Partner = new System.Windows.Forms.DomainUpDown();
            this.btnUpdateBetAccount = new System.Windows.Forms.Button();
            this.groupMain = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.dudMinTime = new System.Windows.Forms.DomainUpDown();
            this.groupControl = new System.Windows.Forms.GroupBox();
            this.dudMaxTime = new System.Windows.Forms.DomainUpDown();
            this.lb_RandomRangeTime = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.betControl1 = new BET.BetControl();
            this.betControl2 = new BET.BetControl();
            this.betControl3 = new BET.BetControl();
            this.betControl4 = new BET.BetControl();
            this.betControl5 = new BET.BetControl();
            this.betControl6 = new BET.BetControl();
            this.betControl7 = new BET.BetControl();
            this.betControl8 = new BET.BetControl();
            this.betControl9 = new BET.BetControl();
            this.betControl10 = new BET.BetControl();
            this.betControl11 = new BET.BetControl();
            this.betControl12 = new BET.BetControl();
            this.panel1.SuspendLayout();
            this.groupBetAccountSetting.SuspendLayout();
            this.groupMain.SuspendLayout();
            this.groupControl.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tt_Main
            // 
            this.tt_Main.ToolTipTitle = "Main";
            // 
            // btnLoginAuto
            // 
            this.btnLoginAuto.Location = new System.Drawing.Point(17, 18);
            this.btnLoginAuto.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoginAuto.Name = "btnLoginAuto";
            this.btnLoginAuto.Size = new System.Drawing.Size(100, 26);
            this.btnLoginAuto.TabIndex = 28;
            this.btnLoginAuto.Text = "Login Auto";
            this.tt_Main.SetToolTip(this.btnLoginAuto, "Login Auto");
            this.btnLoginAuto.UseVisualStyleBackColor = true;
            this.btnLoginAuto.Click += new System.EventHandler(this.btnLoginAuto_Click);
            // 
            // bt_Quit
            // 
            this.bt_Quit.Location = new System.Drawing.Point(235, 18);
            this.bt_Quit.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Quit.Name = "bt_Quit";
            this.bt_Quit.Size = new System.Drawing.Size(100, 26);
            this.bt_Quit.TabIndex = 27;
            this.bt_Quit.Text = "Quit";
            this.tt_Main.SetToolTip(this.bt_Quit, "Quit");
            this.bt_Quit.UseVisualStyleBackColor = true;
            this.bt_Quit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(6, 17);
            this.btnLoadData.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(100, 26);
            this.btnLoadData.TabIndex = 26;
            this.btnLoadData.Text = "Load Data";
            this.tt_Main.SetToolTip(this.btnLoadData, "Load Data");
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // bt_Next
            // 
            this.bt_Next.Location = new System.Drawing.Point(317, 18);
            this.bt_Next.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Next.Name = "bt_Next";
            this.bt_Next.Size = new System.Drawing.Size(100, 26);
            this.bt_Next.TabIndex = 25;
            this.bt_Next.Text = "Next";
            this.tt_Main.SetToolTip(this.bt_Next, "Next");
            this.bt_Next.UseVisualStyleBackColor = true;
            this.bt_Next.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBetOne
            // 
            this.btnBetOne.Enabled = false;
            this.btnBetOne.Location = new System.Drawing.Point(217, 18);
            this.btnBetOne.Margin = new System.Windows.Forms.Padding(2);
            this.btnBetOne.Name = "btnBetOne";
            this.btnBetOne.Size = new System.Drawing.Size(100, 26);
            this.btnBetOne.TabIndex = 24;
            this.btnBetOne.Text = "Bet One";
            this.tt_Main.SetToolTip(this.btnBetOne, "Bet One");
            this.btnBetOne.UseVisualStyleBackColor = true;
            this.btnBetOne.Click += new System.EventHandler(this.btnBetOne_Click);
            // 
            // bt_DelBetList
            // 
            this.bt_DelBetList.Location = new System.Drawing.Point(131, 18);
            this.bt_DelBetList.Margin = new System.Windows.Forms.Padding(2);
            this.bt_DelBetList.Name = "bt_DelBetList";
            this.bt_DelBetList.Size = new System.Drawing.Size(100, 26);
            this.bt_DelBetList.TabIndex = 23;
            this.bt_DelBetList.Text = "Del Bet List";
            this.tt_Main.SetToolTip(this.bt_DelBetList, "Del Bet List");
            this.bt_DelBetList.UseVisualStyleBackColor = true;
            this.bt_DelBetList.Click += new System.EventHandler(this.btnDelBetList_Click);
            // 
            // btnBetAuto
            // 
            this.btnBetAuto.Enabled = false;
            this.btnBetAuto.Location = new System.Drawing.Point(117, 18);
            this.btnBetAuto.Margin = new System.Windows.Forms.Padding(2);
            this.btnBetAuto.Name = "btnBetAuto";
            this.btnBetAuto.Size = new System.Drawing.Size(100, 26);
            this.btnBetAuto.TabIndex = 22;
            this.btnBetAuto.Text = "Bet Auto";
            this.tt_Main.SetToolTip(this.btnBetAuto, "Bet Auto");
            this.btnBetAuto.UseVisualStyleBackColor = true;
            this.btnBetAuto.Click += new System.EventHandler(this.btnBetAuto_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.groupBetAccountSetting);
            this.panel1.Controls.Add(this.groupMain);
            this.panel1.Controls.Add(this.dudMinTime);
            this.panel1.Controls.Add(this.groupControl);
            this.panel1.Controls.Add(this.dudMaxTime);
            this.panel1.Controls.Add(this.lb_RandomRangeTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 656);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1748, 63);
            this.panel1.TabIndex = 22;
            // 
            // groupBetAccountSetting
            // 
            this.groupBetAccountSetting.Controls.Add(this.btnLoadData);
            this.groupBetAccountSetting.Controls.Add(this.dm_Partner);
            this.groupBetAccountSetting.Controls.Add(this.btnUpdateBetAccount);
            this.groupBetAccountSetting.Location = new System.Drawing.Point(779, 3);
            this.groupBetAccountSetting.Name = "groupBetAccountSetting";
            this.groupBetAccountSetting.Size = new System.Drawing.Size(375, 53);
            this.groupBetAccountSetting.TabIndex = 92;
            this.groupBetAccountSetting.TabStop = false;
            this.groupBetAccountSetting.Text = "Bet Account Setting";
            // 
            // dm_Partner
            // 
            this.dm_Partner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dm_Partner.Location = new System.Drawing.Point(106, 19);
            this.dm_Partner.Margin = new System.Windows.Forms.Padding(2);
            this.dm_Partner.Name = "dm_Partner";
            this.dm_Partner.Size = new System.Drawing.Size(133, 23);
            this.dm_Partner.TabIndex = 29;
            // 
            // btnUpdateBetAccount
            // 
            this.btnUpdateBetAccount.Location = new System.Drawing.Point(254, 19);
            this.btnUpdateBetAccount.Name = "btnUpdateBetAccount";
            this.btnUpdateBetAccount.Size = new System.Drawing.Size(113, 23);
            this.btnUpdateBetAccount.TabIndex = 88;
            this.btnUpdateBetAccount.Text = "Update Bet Account";
            this.btnUpdateBetAccount.UseVisualStyleBackColor = true;
            this.btnUpdateBetAccount.Click += new System.EventHandler(this.btnUpdateBetAccount_Click);
            // 
            // groupMain
            // 
            this.groupMain.Controls.Add(this.bt_Quit);
            this.groupMain.Controls.Add(this.bt_DelBetList);
            this.groupMain.Controls.Add(this.btnStart);
            this.groupMain.Location = new System.Drawing.Point(3, 3);
            this.groupMain.Name = "groupMain";
            this.groupMain.Size = new System.Drawing.Size(338, 53);
            this.groupMain.TabIndex = 91;
            this.groupMain.TabStop = false;
            this.groupMain.Text = "Main";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(18, 18);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(108, 26);
            this.btnStart.TabIndex = 89;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dudMinTime
            // 
            this.dudMinTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dudMinTime.Location = new System.Drawing.Point(1214, 23);
            this.dudMinTime.Margin = new System.Windows.Forms.Padding(2);
            this.dudMinTime.Name = "dudMinTime";
            this.dudMinTime.Size = new System.Drawing.Size(46, 23);
            this.dudMinTime.TabIndex = 30;
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.btnLoginAuto);
            this.groupControl.Controls.Add(this.btnBetAuto);
            this.groupControl.Controls.Add(this.btnBetOne);
            this.groupControl.Controls.Add(this.bt_Next);
            this.groupControl.Enabled = false;
            this.groupControl.Location = new System.Drawing.Point(346, 3);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(427, 53);
            this.groupControl.TabIndex = 90;
            this.groupControl.TabStop = false;
            this.groupControl.Text = "Control";
            // 
            // dudMaxTime
            // 
            this.dudMaxTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dudMaxTime.Location = new System.Drawing.Point(1264, 23);
            this.dudMaxTime.Margin = new System.Windows.Forms.Padding(2);
            this.dudMaxTime.Name = "dudMaxTime";
            this.dudMaxTime.Size = new System.Drawing.Size(46, 23);
            this.dudMaxTime.TabIndex = 87;
            // 
            // lb_RandomRangeTime
            // 
            this.lb_RandomRangeTime.AutoSize = true;
            this.lb_RandomRangeTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lb_RandomRangeTime.Location = new System.Drawing.Point(1164, 28);
            this.lb_RandomRangeTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_RandomRangeTime.Name = "lb_RandomRangeTime";
            this.lb_RandomRangeTime.Size = new System.Drawing.Size(30, 13);
            this.lb_RandomRangeTime.TabIndex = 86;
            this.lb_RandomRangeTime.Text = "Time";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.betControl1);
            this.flowLayoutPanel1.Controls.Add(this.betControl2);
            this.flowLayoutPanel1.Controls.Add(this.betControl3);
            this.flowLayoutPanel1.Controls.Add(this.betControl4);
            this.flowLayoutPanel1.Controls.Add(this.betControl5);
            this.flowLayoutPanel1.Controls.Add(this.betControl6);
            this.flowLayoutPanel1.Controls.Add(this.betControl7);
            this.flowLayoutPanel1.Controls.Add(this.betControl8);
            this.flowLayoutPanel1.Controls.Add(this.betControl9);
            this.flowLayoutPanel1.Controls.Add(this.betControl10);
            this.flowLayoutPanel1.Controls.Add(this.betControl11);
            this.flowLayoutPanel1.Controls.Add(this.betControl12);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1748, 656);
            this.flowLayoutPanel1.TabIndex = 23;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::BET.Properties.Resources.SonicRun;
            this.pictureBox1.Location = new System.Drawing.Point(1327, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 93;
            this.pictureBox1.TabStop = false;
            // 
            // betControl1
            // 
            this.betControl1.Location = new System.Drawing.Point(1, 1);
            this.betControl1.Margin = new System.Windows.Forms.Padding(1);
            this.betControl1.Name = "betControl1";
            this.betControl1.NumberControl = 1;
            this.betControl1.Size = new System.Drawing.Size(295, 241);
            this.betControl1.TabIndex = 0;
            // 
            // betControl2
            // 
            this.betControl2.Location = new System.Drawing.Point(298, 1);
            this.betControl2.Margin = new System.Windows.Forms.Padding(1);
            this.betControl2.Name = "betControl2";
            this.betControl2.NumberControl = 2;
            this.betControl2.Size = new System.Drawing.Size(295, 241);
            this.betControl2.TabIndex = 1;
            // 
            // betControl3
            // 
            this.betControl3.Location = new System.Drawing.Point(595, 1);
            this.betControl3.Margin = new System.Windows.Forms.Padding(1);
            this.betControl3.Name = "betControl3";
            this.betControl3.NumberControl = 3;
            this.betControl3.Size = new System.Drawing.Size(295, 241);
            this.betControl3.TabIndex = 2;
            // 
            // betControl4
            // 
            this.betControl4.Location = new System.Drawing.Point(892, 1);
            this.betControl4.Margin = new System.Windows.Forms.Padding(1);
            this.betControl4.Name = "betControl4";
            this.betControl4.NumberControl = 4;
            this.betControl4.Size = new System.Drawing.Size(295, 241);
            this.betControl4.TabIndex = 3;
            // 
            // betControl5
            // 
            this.betControl5.Location = new System.Drawing.Point(1189, 1);
            this.betControl5.Margin = new System.Windows.Forms.Padding(1);
            this.betControl5.Name = "betControl5";
            this.betControl5.NumberControl = 5;
            this.betControl5.Size = new System.Drawing.Size(295, 241);
            this.betControl5.TabIndex = 4;
            // 
            // betControl6
            // 
            this.betControl6.Location = new System.Drawing.Point(1, 244);
            this.betControl6.Margin = new System.Windows.Forms.Padding(1);
            this.betControl6.Name = "betControl6";
            this.betControl6.NumberControl = 6;
            this.betControl6.Size = new System.Drawing.Size(295, 241);
            this.betControl6.TabIndex = 5;
            // 
            // betControl7
            // 
            this.betControl7.Location = new System.Drawing.Point(298, 244);
            this.betControl7.Margin = new System.Windows.Forms.Padding(1);
            this.betControl7.Name = "betControl7";
            this.betControl7.NumberControl = 7;
            this.betControl7.Size = new System.Drawing.Size(295, 241);
            this.betControl7.TabIndex = 6;
            // 
            // betControl8
            // 
            this.betControl8.Location = new System.Drawing.Point(595, 244);
            this.betControl8.Margin = new System.Windows.Forms.Padding(1);
            this.betControl8.Name = "betControl8";
            this.betControl8.NumberControl = 8;
            this.betControl8.Size = new System.Drawing.Size(295, 241);
            this.betControl8.TabIndex = 7;
            // 
            // betControl9
            // 
            this.betControl9.Location = new System.Drawing.Point(892, 244);
            this.betControl9.Margin = new System.Windows.Forms.Padding(1);
            this.betControl9.Name = "betControl9";
            this.betControl9.NumberControl = 9;
            this.betControl9.Size = new System.Drawing.Size(295, 241);
            this.betControl9.TabIndex = 8;
            // 
            // betControl10
            // 
            this.betControl10.Location = new System.Drawing.Point(1189, 244);
            this.betControl10.Margin = new System.Windows.Forms.Padding(1);
            this.betControl10.Name = "betControl10";
            this.betControl10.NumberControl = 10;
            this.betControl10.Size = new System.Drawing.Size(295, 241);
            this.betControl10.TabIndex = 9;
            // 
            // betControl11
            // 
            this.betControl11.Location = new System.Drawing.Point(1, 487);
            this.betControl11.Margin = new System.Windows.Forms.Padding(1);
            this.betControl11.Name = "betControl11";
            this.betControl11.NumberControl = 11;
            this.betControl11.Size = new System.Drawing.Size(295, 241);
            this.betControl11.TabIndex = 10;
            // 
            // betControl12
            // 
            this.betControl12.Location = new System.Drawing.Point(298, 487);
            this.betControl12.Margin = new System.Windows.Forms.Padding(1);
            this.betControl12.Name = "betControl12";
            this.betControl12.NumberControl = 12;
            this.betControl12.Size = new System.Drawing.Size(295, 241);
            this.betControl12.TabIndex = 11;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1748, 719);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBetAccountSetting.ResumeLayout(false);
            this.groupMain.ResumeLayout(false);
            this.groupControl.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip tt_Main;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DomainUpDown dm_Partner;
        private System.Windows.Forms.Button btnLoginAuto;
        private System.Windows.Forms.Button bt_Quit;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button bt_Next;
        private System.Windows.Forms.Button btnBetOne;
        private System.Windows.Forms.Button bt_DelBetList;
        private System.Windows.Forms.Button btnBetAuto;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private BetControl betControl12;
        private BetControl betControl11;
        private BetControl betControl10;
        private BetControl betControl9;
        private BetControl betControl8;
        private BetControl betControl7;
        private BetControl betControl6;
        private BetControl betControl5;
        private BetControl betControl4;
        private BetControl betControl3;
        private BetControl betControl2;
        private BetControl betControl1;
        public System.Windows.Forms.DomainUpDown dudMinTime;
        private System.Windows.Forms.Label lb_RandomRangeTime;
        public System.Windows.Forms.DomainUpDown dudMaxTime;
        private System.Windows.Forms.Button btnUpdateBetAccount;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupControl;
        private System.Windows.Forms.GroupBox groupMain;
        private System.Windows.Forms.GroupBox groupBetAccountSetting;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

