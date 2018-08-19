namespace PDFConverter
{
    partial class PDGFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDGFrom));
            this.btnCombine = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.SelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileNumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DHColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnExe = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCombine
            // 
            this.btnCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCombine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCombine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCombine.Location = new System.Drawing.Point(565, 4);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(75, 39);
            this.btnCombine.TabIndex = 9;
            this.btnCombine.Text = "合成PDF";
            this.btnCombine.UseVisualStyleBackColor = false;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(14, 61);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(161, 12);
            this.lblInfo.TabIndex = 8;
            this.lblInfo.Text = "已选  本书，共  页，共  MB";
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
            this.NameColumn,
            this.FileNumColumn,
            this.CheckColumn,
            this.DHColumn,
            this.FileColumn});
            this.dgvView.Location = new System.Drawing.Point(12, 79);
            this.dgvView.Name = "dgvView";
            this.dgvView.RowHeadersVisible = false;
            this.dgvView.RowTemplate.Height = 23;
            this.dgvView.Size = new System.Drawing.Size(628, 396);
            this.dgvView.TabIndex = 7;
            this.dgvView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvView_CellMouseUp);
            // 
            // SelectColumn
            // 
            this.SelectColumn.FalseValue = "False";
            this.SelectColumn.HeaderText = "";
            this.SelectColumn.Name = "SelectColumn";
            this.SelectColumn.TrueValue = "True";
            this.SelectColumn.Width = 25;
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = "书名";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NameColumn.Width = 250;
            // 
            // FileNumColumn
            // 
            this.FileNumColumn.HeaderText = "文件数";
            this.FileNumColumn.Name = "FileNumColumn";
            this.FileNumColumn.ReadOnly = true;
            // 
            // CheckColumn
            // 
            this.CheckColumn.HeaderText = "坏页";
            this.CheckColumn.Name = "CheckColumn";
            this.CheckColumn.ReadOnly = true;
            // 
            // DHColumn
            // 
            this.DHColumn.HeaderText = "分配段号";
            this.DHColumn.Name = "DHColumn";
            this.DHColumn.ReadOnly = true;
            this.DHColumn.Width = 150;
            // 
            // FileColumn
            // 
            this.FileColumn.HeaderText = "地址";
            this.FileColumn.Name = "FileColumn";
            this.FileColumn.ReadOnly = true;
            this.FileColumn.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.Maroon;
            this.lblTitle.Location = new System.Drawing.Point(69, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(90, 21);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "PDF合成";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // txtPath
            // 
            this.txtPath.AllowDrop = true;
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(289, 51);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(294, 21);
            this.txtPath.TabIndex = 10;
            this.txtPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPath_DragDrop);
            this.txtPath.DragOver += new System.Windows.Forms.DragEventHandler(this.txtPath_DragOver);
            // 
            // btnDir
            // 
            this.btnDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDir.Location = new System.Drawing.Point(589, 45);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(51, 31);
            this.btnDir.TabIndex = 11;
            this.btnDir.Text = "浏览";
            this.btnDir.UseVisualStyleBackColor = false;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnExe
            // 
            this.btnExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExe.BackColor = System.Drawing.Color.LightGreen;
            this.btnExe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExe.Location = new System.Drawing.Point(449, 4);
            this.btnExe.Name = "btnExe";
            this.btnExe.Size = new System.Drawing.Size(110, 39);
            this.btnExe.TabIndex = 12;
            this.btnExe.Text = "手动图片转换";
            this.btnExe.UseVisualStyleBackColor = false;
            this.btnExe.Click += new System.EventHandler(this.btnExe_Click);
            // 
            // PDGFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 487);
            this.Controls.Add(this.btnExe);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnCombine);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvView);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PDGFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重命名任务";
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileNumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DHColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumn;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Button btnExe;
    }
}