namespace HTTP_AGENT
{
    partial class FormMain
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
            this.agentUser1 = new HTTP_AGENT.AgentUser();
            this.SuspendLayout();
            // 
            // agentUser1
            // 
            this.agentUser1.Dock = System.Windows.Forms.DockStyle.Top;
            this.agentUser1.Location = new System.Drawing.Point(0, 0);
            this.agentUser1.Name = "agentUser1";
            this.agentUser1.Size = new System.Drawing.Size(1743, 768);
            this.agentUser1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1743, 768);
            this.Controls.Add(this.agentUser1);
            this.Name = "FormMain";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private AgentUser agentUser1;
    }
}

