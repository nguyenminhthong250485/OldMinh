namespace GenerateLoto
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.richOutput = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLoai = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtTien = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSoPhoi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(848, 12);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // richOutput
            // 
            this.richOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richOutput.Location = new System.Drawing.Point(0, 44);
            this.richOutput.Name = "richOutput";
            this.richOutput.Size = new System.Drawing.Size(935, 757);
            this.richOutput.TabIndex = 1;
            this.richOutput.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtLoai);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtUnit);
            this.panel1.Controls.Add(this.txtTien);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSoPhoi);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 44);
            this.panel1.TabIndex = 2;
            // 
            // txtLoai
            // 
            this.txtLoai.Location = new System.Drawing.Point(389, 9);
            this.txtLoai.Name = "txtLoai";
            this.txtLoai.Size = new System.Drawing.Size(231, 20);
            this.txtLoai.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Loại:";
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(259, 9);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(58, 20);
            this.txtUnit.TabIndex = 2;
            this.txtUnit.Text = "1000";
            this.txtUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTien
            // 
            this.txtTien.Location = new System.Drawing.Point(186, 9);
            this.txtTien.Name = "txtTien";
            this.txtTien.Size = new System.Drawing.Size(58, 20);
            this.txtTien.TabIndex = 1;
            this.txtTien.Text = "10";
            this.txtTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tiền:";
            // 
            // txtSoPhoi
            // 
            this.txtSoPhoi.Location = new System.Drawing.Point(62, 9);
            this.txtSoPhoi.Name = "txtSoPhoi";
            this.txtSoPhoi.Size = new System.Drawing.Size(74, 20);
            this.txtSoPhoi.TabIndex = 0;
            this.txtSoPhoi.Text = "4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Số Phơi:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 801);
            this.Controls.Add(this.richOutput);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.RichTextBox richOutput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSoPhoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtLoai;
        private System.Windows.Forms.Label label3;
    }
}

