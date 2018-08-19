namespace PDFConverter
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.SelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ImportColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZipColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherExeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.Maroon;
            this.lblTitle.Location = new System.Drawing.Point(69, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(90, 21);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "PDF合成";
            // 
            // dgvView
            // 
            this.dgvView.AllowDrop = true;
            this.dgvView.AllowUserToAddRows = false;
            this.dgvView.AllowUserToResizeRows = false;
            this.dgvView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvView.ColumnHeadersHeight = 40;
            this.dgvView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectColumn,
            this.ImportColumn,
            this.ZipColumn,
            this.CheckColumn,
            this.CombineColumn,
            this.OtherExeColumn,
            this.FileColumn});
            this.dgvView.Location = new System.Drawing.Point(12, 77);
            this.dgvView.Name = "dgvView";
            this.dgvView.RowHeadersVisible = false;
            this.dgvView.RowTemplate.Height = 23;
            this.dgvView.Size = new System.Drawing.Size(709, 405);
            this.dgvView.TabIndex = 2;
            this.dgvView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvView_CellMouseUp);
            this.dgvView.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvView_DragDrop);
            this.dgvView.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvView_DragOver);
            // 
            // SelectColumn
            // 
            this.SelectColumn.FalseValue = "False";
            this.SelectColumn.HeaderText = "";
            this.SelectColumn.Name = "SelectColumn";
            this.SelectColumn.TrueValue = "True";
            this.SelectColumn.Width = 25;
            // 
            // ImportColumn
            // 
            this.ImportColumn.HeaderText = "导入";
            this.ImportColumn.Name = "ImportColumn";
            this.ImportColumn.ReadOnly = true;
            this.ImportColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ImportColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ImportColumn.Width = 200;
            // 
            // ZipColumn
            // 
            this.ZipColumn.HeaderText = "解压";
            this.ZipColumn.Name = "ZipColumn";
            this.ZipColumn.ReadOnly = true;
            this.ZipColumn.Width = 200;
            // 
            // CheckColumn
            // 
            this.CheckColumn.HeaderText = "检测";
            this.CheckColumn.Name = "CheckColumn";
            this.CheckColumn.ReadOnly = true;
            this.CheckColumn.Width = 80;
            // 
            // CombineColumn
            // 
            this.CombineColumn.HeaderText = "默认合成";
            this.CombineColumn.Name = "CombineColumn";
            this.CombineColumn.ReadOnly = true;
            // 
            // OtherExeColumn
            // 
            this.OtherExeColumn.HeaderText = "默认程序2";
            this.OtherExeColumn.Name = "OtherExeColumn";
            this.OtherExeColumn.ReadOnly = true;
            // 
            // FileColumn
            // 
            this.FileColumn.HeaderText = "Zip地址";
            this.FileColumn.Name = "FileColumn";
            this.FileColumn.ReadOnly = true;
            this.FileColumn.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(14, 59);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(161, 12);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "已选  本书，共  页，共  MB";
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Location = new System.Drawing.Point(645, 32);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 39);
            this.btnYes.TabIndex = 4;
            this.btnYes.Text = "确认";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(733, 494);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvView);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF合成";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImportColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZipColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherExeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumn;
    }
}

