namespace XanhDo
{
    partial class frmAgent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgent));
            this.groupMatch = new System.Windows.Forms.GroupBox();
            this.btnCreateMatch = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.txtMatch = new System.Windows.Forms.TextBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.groupOdds = new System.Windows.Forms.GroupBox();
            this.btnUpdateOdds = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGreenOdds = new System.Windows.Forms.TextBox();
            this.txtBlueOdds = new System.Windows.Forms.TextBox();
            this.txtRedOdds = new System.Windows.Forms.TextBox();
            this.groupResult = new System.Windows.Forms.GroupBox();
            this.btnUpdateResult = new System.Windows.Forms.Button();
            this.radCancel = new System.Windows.Forms.RadioButton();
            this.radHoaWin = new System.Windows.Forms.RadioButton();
            this.radBlueWin = new System.Windows.Forms.RadioButton();
            this.radRedWin = new System.Windows.Forms.RadioButton();
            this.groupTicket = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.groupMatch.SuspendLayout();
            this.groupOdds.SuspendLayout();
            this.groupResult.SuspendLayout();
            this.groupTicket.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMatch
            // 
            this.groupMatch.Controls.Add(this.dtpDate);
            this.groupMatch.Controls.Add(this.btnCreateMatch);
            this.groupMatch.Controls.Add(this.btnBack);
            this.groupMatch.Controls.Add(this.txtCompany);
            this.groupMatch.Controls.Add(this.txtMatch);
            this.groupMatch.Controls.Add(this.btnForward);
            this.groupMatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupMatch.Location = new System.Drawing.Point(0, 0);
            this.groupMatch.Name = "groupMatch";
            this.groupMatch.Size = new System.Drawing.Size(876, 74);
            this.groupMatch.TabIndex = 9;
            this.groupMatch.TabStop = false;
            this.groupMatch.Text = "Trận số";
            // 
            // btnCreateMatch
            // 
            this.btnCreateMatch.Location = new System.Drawing.Point(454, 31);
            this.btnCreateMatch.Name = "btnCreateMatch";
            this.btnCreateMatch.Size = new System.Drawing.Size(166, 28);
            this.btnCreateMatch.TabIndex = 3;
            this.btnCreateMatch.Text = "Khởi tạo trận đấu";
            this.btnCreateMatch.UseVisualStyleBackColor = true;
            this.btnCreateMatch.Click += new System.EventHandler(this.btnCreateMatch_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(167, 31);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 28);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtCompany
            // 
            this.txtCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.txtCompany.Location = new System.Drawing.Point(34, 31);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(108, 28);
            this.txtCompany.TabIndex = 1;
            this.txtCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMatch
            // 
            this.txtMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.txtMatch.Location = new System.Drawing.Point(248, 31);
            this.txtMatch.Name = "txtMatch";
            this.txtMatch.Size = new System.Drawing.Size(108, 28);
            this.txtMatch.TabIndex = 1;
            this.txtMatch.Text = "0";
            this.txtMatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(362, 31);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 28);
            this.btnForward.TabIndex = 2;
            this.btnForward.Text = ">";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // groupOdds
            // 
            this.groupOdds.Controls.Add(this.btnUpdateOdds);
            this.groupOdds.Controls.Add(this.label3);
            this.groupOdds.Controls.Add(this.label2);
            this.groupOdds.Controls.Add(this.label1);
            this.groupOdds.Controls.Add(this.txtGreenOdds);
            this.groupOdds.Controls.Add(this.txtBlueOdds);
            this.groupOdds.Controls.Add(this.txtRedOdds);
            this.groupOdds.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupOdds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupOdds.Location = new System.Drawing.Point(0, 74);
            this.groupOdds.Name = "groupOdds";
            this.groupOdds.Size = new System.Drawing.Size(876, 80);
            this.groupOdds.TabIndex = 10;
            this.groupOdds.TabStop = false;
            this.groupOdds.Text = "Cập nhật tỉ lệ cho trận đấu";
            // 
            // btnUpdateOdds
            // 
            this.btnUpdateOdds.Location = new System.Drawing.Point(661, 37);
            this.btnUpdateOdds.Name = "btnUpdateOdds";
            this.btnUpdateOdds.Size = new System.Drawing.Size(166, 28);
            this.btnUpdateOdds.TabIndex = 6;
            this.btnUpdateOdds.Text = "Cập nhật tỉ lệ";
            this.btnUpdateOdds.UseVisualStyleBackColor = true;
            this.btnUpdateOdds.Click += new System.EventHandler(this.btnUpdateOdds_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hòa Odds";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Blue Odds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Red Odds";
            // 
            // txtGreenOdds
            // 
            this.txtGreenOdds.BackColor = System.Drawing.Color.Lime;
            this.txtGreenOdds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGreenOdds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGreenOdds.Location = new System.Drawing.Point(530, 38);
            this.txtGreenOdds.Name = "txtGreenOdds";
            this.txtGreenOdds.Size = new System.Drawing.Size(100, 26);
            this.txtGreenOdds.TabIndex = 5;
            this.txtGreenOdds.Text = "1:8";
            this.txtGreenOdds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBlueOdds
            // 
            this.txtBlueOdds.BackColor = System.Drawing.Color.Blue;
            this.txtBlueOdds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBlueOdds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBlueOdds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtBlueOdds.Location = new System.Drawing.Point(323, 38);
            this.txtBlueOdds.Name = "txtBlueOdds";
            this.txtBlueOdds.Size = new System.Drawing.Size(100, 26);
            this.txtBlueOdds.TabIndex = 3;
            this.txtBlueOdds.Text = "0";
            this.txtBlueOdds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRedOdds
            // 
            this.txtRedOdds.BackColor = System.Drawing.Color.Red;
            this.txtRedOdds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRedOdds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRedOdds.ForeColor = System.Drawing.Color.Lime;
            this.txtRedOdds.Location = new System.Drawing.Point(112, 38);
            this.txtRedOdds.Name = "txtRedOdds";
            this.txtRedOdds.Size = new System.Drawing.Size(100, 26);
            this.txtRedOdds.TabIndex = 1;
            this.txtRedOdds.Text = "0";
            this.txtRedOdds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupResult
            // 
            this.groupResult.Controls.Add(this.btnUpdateResult);
            this.groupResult.Controls.Add(this.radCancel);
            this.groupResult.Controls.Add(this.radHoaWin);
            this.groupResult.Controls.Add(this.radBlueWin);
            this.groupResult.Controls.Add(this.radRedWin);
            this.groupResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupResult.Location = new System.Drawing.Point(0, 154);
            this.groupResult.Name = "groupResult";
            this.groupResult.Size = new System.Drawing.Size(876, 73);
            this.groupResult.TabIndex = 11;
            this.groupResult.TabStop = false;
            this.groupResult.Text = "Cập nhật kết quả";
            // 
            // btnUpdateResult
            // 
            this.btnUpdateResult.Location = new System.Drawing.Point(510, 33);
            this.btnUpdateResult.Name = "btnUpdateResult";
            this.btnUpdateResult.Size = new System.Drawing.Size(317, 28);
            this.btnUpdateResult.TabIndex = 6;
            this.btnUpdateResult.Text = "Cập nhật kết quả trận đấu";
            this.btnUpdateResult.UseVisualStyleBackColor = true;
            this.btnUpdateResult.Click += new System.EventHandler(this.btnUpdateResult_Click);
            // 
            // radCancel
            // 
            this.radCancel.AutoSize = true;
            this.radCancel.ForeColor = System.Drawing.Color.Black;
            this.radCancel.Location = new System.Drawing.Point(407, 37);
            this.radCancel.Name = "radCancel";
            this.radCancel.Size = new System.Drawing.Size(87, 24);
            this.radCancel.TabIndex = 0;
            this.radCancel.TabStop = true;
            this.radCancel.Text = "Hủy trận";
            this.radCancel.UseVisualStyleBackColor = true;
            // 
            // radHoaWin
            // 
            this.radHoaWin.AutoSize = true;
            this.radHoaWin.ForeColor = System.Drawing.Color.Lime;
            this.radHoaWin.Location = new System.Drawing.Point(275, 37);
            this.radHoaWin.Name = "radHoaWin";
            this.radHoaWin.Size = new System.Drawing.Size(113, 24);
            this.radHoaWin.TabIndex = 0;
            this.radHoaWin.TabStop = true;
            this.radHoaWin.Text = "Kết quả hòa";
            this.radHoaWin.UseVisualStyleBackColor = true;
            // 
            // radBlueWin
            // 
            this.radBlueWin.AutoSize = true;
            this.radBlueWin.ForeColor = System.Drawing.Color.Blue;
            this.radBlueWin.Location = new System.Drawing.Point(146, 37);
            this.radBlueWin.Name = "radBlueWin";
            this.radBlueWin.Size = new System.Drawing.Size(110, 24);
            this.radBlueWin.TabIndex = 0;
            this.radBlueWin.TabStop = true;
            this.radBlueWin.Text = "Xanh thắng";
            this.radBlueWin.UseVisualStyleBackColor = true;
            // 
            // radRedWin
            // 
            this.radRedWin.AutoSize = true;
            this.radRedWin.ForeColor = System.Drawing.Color.Red;
            this.radRedWin.Location = new System.Drawing.Point(34, 37);
            this.radRedWin.Name = "radRedWin";
            this.radRedWin.Size = new System.Drawing.Size(93, 24);
            this.radRedWin.TabIndex = 0;
            this.radRedWin.TabStop = true;
            this.radRedWin.Text = "Đỏ thắng";
            this.radRedWin.UseVisualStyleBackColor = true;
            // 
            // groupTicket
            // 
            this.groupTicket.Controls.Add(this.dataGridView1);
            this.groupTicket.Controls.Add(this.bindingNavigator1);
            this.groupTicket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupTicket.Location = new System.Drawing.Point(0, 227);
            this.groupTicket.Name = "groupTicket";
            this.groupTicket.Size = new System.Drawing.Size(876, 297);
            this.groupTicket.TabIndex = 12;
            this.groupTicket.TabStop = false;
            this.groupTicket.Text = "Danh sách cược";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(870, 247);
            this.dataGridView1.TabIndex = 0;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(3, 269);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(870, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(661, 31);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(116, 26);
            this.dtpDate.TabIndex = 4;
            // 
            // frmAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 524);
            this.Controls.Add(this.groupTicket);
            this.Controls.Add(this.groupResult);
            this.Controls.Add(this.groupOdds);
            this.Controls.Add(this.groupMatch);
            this.Name = "frmAgent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agent";
            this.Load += new System.EventHandler(this.frmAgent_Load);
            this.groupMatch.ResumeLayout(false);
            this.groupMatch.PerformLayout();
            this.groupOdds.ResumeLayout(false);
            this.groupOdds.PerformLayout();
            this.groupResult.ResumeLayout(false);
            this.groupResult.PerformLayout();
            this.groupTicket.ResumeLayout(false);
            this.groupTicket.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMatch;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtMatch;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnCreateMatch;
        private System.Windows.Forms.GroupBox groupOdds;
        private System.Windows.Forms.TextBox txtGreenOdds;
        private System.Windows.Forms.TextBox txtBlueOdds;
        private System.Windows.Forms.TextBox txtRedOdds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateOdds;
        private System.Windows.Forms.GroupBox groupResult;
        private System.Windows.Forms.Button btnUpdateResult;
        private System.Windows.Forms.RadioButton radCancel;
        private System.Windows.Forms.RadioButton radHoaWin;
        private System.Windows.Forms.RadioButton radBlueWin;
        private System.Windows.Forms.RadioButton radRedWin;
        private System.Windows.Forms.GroupBox groupTicket;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}