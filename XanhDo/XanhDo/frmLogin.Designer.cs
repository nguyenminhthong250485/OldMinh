namespace XanhDo
{
    partial class frmLogin
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnDangnhap = new System.Windows.Forms.Button();
            this.radSuper = new System.Windows.Forms.RadioButton();
            this.radAgent = new System.Windows.Forms.RadioButton();
            this.radMember = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mật khẩu:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(102, 16);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(139, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(102, 39);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(139, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnDangnhap
            // 
            this.btnDangnhap.Location = new System.Drawing.Point(166, 65);
            this.btnDangnhap.Name = "btnDangnhap";
            this.btnDangnhap.Size = new System.Drawing.Size(75, 23);
            this.btnDangnhap.TabIndex = 2;
            this.btnDangnhap.Text = "Đăng nhập";
            this.btnDangnhap.UseVisualStyleBackColor = true;
            this.btnDangnhap.Click += new System.EventHandler(this.btnDangnhap_Click);
            // 
            // radSuper
            // 
            this.radSuper.AutoSize = true;
            this.radSuper.Location = new System.Drawing.Point(50, 68);
            this.radSuper.Name = "radSuper";
            this.radSuper.Size = new System.Drawing.Size(32, 17);
            this.radSuper.TabIndex = 2;
            this.radSuper.Text = "S";
            this.radSuper.UseVisualStyleBackColor = true;
            this.radSuper.Visible = false;
            // 
            // radAgent
            // 
            this.radAgent.AutoSize = true;
            this.radAgent.Location = new System.Drawing.Point(88, 68);
            this.radAgent.Name = "radAgent";
            this.radAgent.Size = new System.Drawing.Size(32, 17);
            this.radAgent.TabIndex = 3;
            this.radAgent.Text = "A";
            this.radAgent.UseVisualStyleBackColor = true;
            // 
            // radMember
            // 
            this.radMember.AutoSize = true;
            this.radMember.Checked = true;
            this.radMember.Location = new System.Drawing.Point(126, 68);
            this.radMember.Name = "radMember";
            this.radMember.Size = new System.Drawing.Size(34, 17);
            this.radMember.TabIndex = 4;
            this.radMember.TabStop = true;
            this.radMember.Text = "M";
            this.radMember.UseVisualStyleBackColor = true;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 98);
            this.Controls.Add(this.radMember);
            this.Controls.Add(this.radAgent);
            this.Controls.Add(this.radSuper);
            this.Controls.Add(this.btnDangnhap);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnDangnhap;
        private System.Windows.Forms.RadioButton radSuper;
        private System.Windows.Forms.RadioButton radAgent;
        private System.Windows.Forms.RadioButton radMember;
    }
}