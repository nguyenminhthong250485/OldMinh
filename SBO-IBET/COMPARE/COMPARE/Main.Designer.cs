namespace COMPARE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.bt_Quit = new System.Windows.Forms.Button();
            this.lb_Auto = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.compareControl1 = new COMPARE.CompareControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_Quit
            // 
            this.bt_Quit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Quit.Location = new System.Drawing.Point(11, 11);
            this.bt_Quit.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Quit.Name = "bt_Quit";
            this.bt_Quit.Size = new System.Drawing.Size(67, 32);
            this.bt_Quit.TabIndex = 6;
            this.bt_Quit.Text = "Quit";
            this.bt_Quit.UseVisualStyleBackColor = true;
            this.bt_Quit.Click += new System.EventHandler(this.bt_Quit_Click);
            // 
            // lb_Auto
            // 
            this.lb_Auto.AutoSize = true;
            this.lb_Auto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Auto.Location = new System.Drawing.Point(309, 18);
            this.lb_Auto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Auto.Name = "lb_Auto";
            this.lb_Auto.Size = new System.Drawing.Size(0, 24);
            this.lb_Auto.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_Quit);
            this.panel1.Controls.Add(this.lb_Auto);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 432);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 54);
            this.panel1.TabIndex = 9;
            // 
            // compareControl1
            // 
            this.compareControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareControl1.Location = new System.Drawing.Point(0, 0);
            this.compareControl1.Margin = new System.Windows.Forms.Padding(1);
            this.compareControl1.Name = "compareControl1";
            this.compareControl1.Size = new System.Drawing.Size(964, 432);
            this.compareControl1.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 486);
            this.Controls.Add(this.compareControl1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CompareControl compareControl1;
        private System.Windows.Forms.Button bt_Quit;
        private System.Windows.Forms.Label lb_Auto;
        private System.Windows.Forms.Panel panel1;
    }
}

