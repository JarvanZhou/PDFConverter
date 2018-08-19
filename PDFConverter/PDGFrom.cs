using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFConverter
{
    public partial class PDGFrom : Form
    {
        public PDGFrom()
        {
            InitializeComponent();
        }

        public event EventHandler<PDFEventArgs> PDFChanged
        {
            add
            { _pdfChanged += value; }
            remove
            { _pdfChanged -= value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            foreach (string file in _files)
            {
                string f = file;
                if (f.EndsWith("\\"))
                    f = f.Substring(0, f.Length - 1);
                dgvView.Rows.Add(false, f.Substring(f.LastIndexOf("\\") + 1), "", "全部完好", "", file);
            }
            foreach (DataGridViewRow row in dgvView.Rows)
            {
                string file = row.Cells[5].Value.ToString();
                string[] fs = Directory.GetFiles(file, "*.*");
                row.Cells[2].Value = fs.Length.ToString();
            }
            lblTitle.Text = _title;
        }

        public void SetData(string title, string[] files)
        {
            _files = files;
            _title = title;
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = open.SelectedPath;
            }
        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvView.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cell.EditingCellFormattedValue) == true)
                {
                    //此处添加PDF处理
                    _pdfChanged?.Invoke(dgvView, new PDFEventArgs(row.Cells["FileColumn"].Value.ToString()));
                }
            }
        }

        private void btnExe_Click(object sender, EventArgs e)
        {
            Process.Start("Pdg2Pic.exe");
        }

        private void txtPath_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void txtPath_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (!Directory.Exists(data[0])) return;
            txtPath.Text = data[0];
        }

        private void dgvView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 0)
            {
                int num = 1;
                List<string> files = new List<string>();
                foreach (DataGridViewRow row in dgvView.Rows)
                {
                    DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(cell.EditingCellFormattedValue) == true)
                    {
                        files.Add(row.Cells["FileColumn"].Value.ToString());
                        int n = Convert.ToInt32(row.Cells[2].Value);
                        if (n == 1)
                            row.Cells[4].Value = num.ToString("00000000");
                        else
                            row.Cells[4].Value = num.ToString("00000000") + "-" + (num + n - 1).ToString("00000000");
                        num += n;
                    }
                    else
                    {
                        row.Cells[4].Value = "";
                    }
                }
                string info = FilesInfo.GetFilesInfo(files.ToArray());
                lblInfo.Text = info;
            }
        }

        private string[] _files;
        private string _title;
        private EventHandler<PDFEventArgs> _pdfChanged;

    }

    public class PDFEventArgs : EventArgs
    {
        public PDFEventArgs(string file)
        {
            File = file;
        }

        public string File
        { get; private set; }
    }
}
