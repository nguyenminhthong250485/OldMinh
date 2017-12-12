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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUpdateSheet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 489);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1148, 41);
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
            this.betControl2.Size = new System.Drawing.Size(1148, 489);
            this.betControl2.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 530);
            this.Controls.Add(this.betControl2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Main (v1.8)";
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
    }
}

