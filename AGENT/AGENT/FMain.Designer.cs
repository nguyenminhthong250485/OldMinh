namespace AGENT
{
    partial class MainF
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
            this.bt_NewAcc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_NewAcc
            // 
            this.bt_NewAcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_NewAcc.Location = new System.Drawing.Point(30, 22);
            this.bt_NewAcc.Name = "bt_NewAcc";
            this.bt_NewAcc.Size = new System.Drawing.Size(154, 31);
            this.bt_NewAcc.TabIndex = 0;
            this.bt_NewAcc.Text = "New MemBer";
            this.bt_NewAcc.UseVisualStyleBackColor = true;
            this.bt_NewAcc.Click += new System.EventHandler(this.bt_NewAcc_Click);
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1749, 790);
            this.Controls.Add(this.bt_NewAcc);
            this.Name = "MainF";
            this.Text = "MainF";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_NewAcc;
    }
}

