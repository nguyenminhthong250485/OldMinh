namespace BET_BET
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdateSheet = new System.Windows.Forms.Button();
            this.betControl2 = new BET_BET.BetControl();
            this.bt_InserAccBet = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_InserAccBet);
            this.panel1.Controls.Add(this.btnUpdateSheet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 441);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(767, 41);
            this.panel1.TabIndex = 2;
            // 
            // btnUpdateSheet
            // 
            this.btnUpdateSheet.Location = new System.Drawing.Point(12, 8);
            this.btnUpdateSheet.Name = "btnUpdateSheet";
            this.btnUpdateSheet.Size = new System.Drawing.Size(116, 23);
            this.btnUpdateSheet.TabIndex = 2;
            this.btnUpdateSheet.Text = "UPDATE SHEET";
            this.btnUpdateSheet.UseVisualStyleBackColor = true;
            this.btnUpdateSheet.Click += new System.EventHandler(this.btnUpdateSheet_Click);
            // 
            // betControl2
            // 
            this.betControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.betControl2.Location = new System.Drawing.Point(0, 0);
            this.betControl2.Margin = new System.Windows.Forms.Padding(4);
            this.betControl2.Name = "betControl2";
            this.betControl2.Size = new System.Drawing.Size(767, 441);
            this.betControl2.TabIndex = 0;
            // 
            // bt_InserAccBet
            // 
            this.bt_InserAccBet.Location = new System.Drawing.Point(134, 8);
            this.bt_InserAccBet.Name = "bt_InserAccBet";
            this.bt_InserAccBet.Size = new System.Drawing.Size(116, 23);
            this.bt_InserAccBet.TabIndex = 3;
            this.bt_InserAccBet.Text = "INSERT ACC";
            this.bt_InserAccBet.UseVisualStyleBackColor = true;
            this.bt_InserAccBet.Click += new System.EventHandler(this.bt_InserAccBet_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 482);
            this.Controls.Add(this.betControl2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Main (v1.2)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BetControl betControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUpdateSheet;
        private BetControl betControl2;
        private System.Windows.Forms.Button bt_InserAccBet;
    }
}

