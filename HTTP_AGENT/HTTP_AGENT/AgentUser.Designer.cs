namespace HTTP_AGENT
{
    partial class AgentUser
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupIBET = new System.Windows.Forms.GroupBox();
            this.dataGridViewIBET = new System.Windows.Forms.DataGridView();
            this.panelIbet = new System.Windows.Forms.Panel();
            this.btnLoginIBET = new System.Windows.Forms.Button();
            this.groupSBO = new System.Windows.Forms.GroupBox();
            this.dataGridViewSBO = new System.Windows.Forms.DataGridView();
            this.panelSbo = new System.Windows.Forms.Panel();
            this.btnLoginSBO = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupIBET.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIBET)).BeginInit();
            this.panelIbet.SuspendLayout();
            this.groupSBO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSBO)).BeginInit();
            this.panelSbo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupIBET, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupSBO, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(953, 331);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // groupIBET
            // 
            this.groupIBET.Controls.Add(this.dataGridViewIBET);
            this.groupIBET.Controls.Add(this.panelIbet);
            this.groupIBET.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupIBET.Location = new System.Drawing.Point(3, 3);
            this.groupIBET.Name = "groupIBET";
            this.groupIBET.Size = new System.Drawing.Size(470, 325);
            this.groupIBET.TabIndex = 8;
            this.groupIBET.TabStop = false;
            this.groupIBET.Text = "IBET";
            // 
            // dataGridViewIBET
            // 
            this.dataGridViewIBET.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewIBET.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewIBET.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIBET.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewIBET.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewIBET.Name = "dataGridViewIBET";
            this.dataGridViewIBET.RowHeadersVisible = false;
            this.dataGridViewIBET.Size = new System.Drawing.Size(464, 277);
            this.dataGridViewIBET.TabIndex = 6;
            this.dataGridViewIBET.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewIBET_CellClick);
            // 
            // panelIbet
            // 
            this.panelIbet.Controls.Add(this.btnLoginIBET);
            this.panelIbet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelIbet.Location = new System.Drawing.Point(3, 293);
            this.panelIbet.Name = "panelIbet";
            this.panelIbet.Size = new System.Drawing.Size(464, 29);
            this.panelIbet.TabIndex = 7;
            // 
            // btnLoginIBET
            // 
            this.btnLoginIBET.Location = new System.Drawing.Point(3, 3);
            this.btnLoginIBET.Name = "btnLoginIBET";
            this.btnLoginIBET.Size = new System.Drawing.Size(75, 23);
            this.btnLoginIBET.TabIndex = 0;
            this.btnLoginIBET.Text = "Login";
            this.btnLoginIBET.UseVisualStyleBackColor = true;
            this.btnLoginIBET.Click += new System.EventHandler(this.btnLoginIBET_Click);
            // 
            // groupSBO
            // 
            this.groupSBO.Controls.Add(this.dataGridViewSBO);
            this.groupSBO.Controls.Add(this.panelSbo);
            this.groupSBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSBO.Location = new System.Drawing.Point(479, 3);
            this.groupSBO.Name = "groupSBO";
            this.groupSBO.Size = new System.Drawing.Size(471, 325);
            this.groupSBO.TabIndex = 7;
            this.groupSBO.TabStop = false;
            this.groupSBO.Text = "SBO";
            // 
            // dataGridViewSBO
            // 
            this.dataGridViewSBO.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewSBO.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSBO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSBO.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewSBO.Name = "dataGridViewSBO";
            this.dataGridViewSBO.RowHeadersVisible = false;
            this.dataGridViewSBO.Size = new System.Drawing.Size(465, 277);
            this.dataGridViewSBO.TabIndex = 6;
            // 
            // panelSbo
            // 
            this.panelSbo.Controls.Add(this.btnLoginSBO);
            this.panelSbo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSbo.Location = new System.Drawing.Point(3, 293);
            this.panelSbo.Name = "panelSbo";
            this.panelSbo.Size = new System.Drawing.Size(465, 29);
            this.panelSbo.TabIndex = 7;
            // 
            // btnLoginSBO
            // 
            this.btnLoginSBO.Location = new System.Drawing.Point(3, 3);
            this.btnLoginSBO.Name = "btnLoginSBO";
            this.btnLoginSBO.Size = new System.Drawing.Size(75, 23);
            this.btnLoginSBO.TabIndex = 0;
            this.btnLoginSBO.Text = "Login";
            this.btnLoginSBO.UseVisualStyleBackColor = true;
            // 
            // AgentUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AgentUser";
            this.Size = new System.Drawing.Size(953, 331);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupIBET.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIBET)).EndInit();
            this.panelIbet.ResumeLayout(false);
            this.groupSBO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSBO)).EndInit();
            this.panelSbo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupIBET;
        private System.Windows.Forms.DataGridView dataGridViewIBET;
        private System.Windows.Forms.Panel panelIbet;
        private System.Windows.Forms.Button btnLoginIBET;
        private System.Windows.Forms.GroupBox groupSBO;
        private System.Windows.Forms.DataGridView dataGridViewSBO;
        private System.Windows.Forms.Panel panelSbo;
        private System.Windows.Forms.Button btnLoginSBO;
    }
}
