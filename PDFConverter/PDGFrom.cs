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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFConverter
{
    public partial class PDGFrom : Form
    {
        public PDGFrom()
        {
            InitializeComponent();
            _dirs = new List<string>();
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

        private void btn_rename_Click(object sender, EventArgs e)
        {
            _outputPath = txtPath.Text;
            List<string> errlist = new List<string>();
            
            DataTable dt = new DataTable();
            dt.Columns.Add("来源");
            dt.Columns.Add("名称1");
            dt.Columns.Add("名称2");

            foreach (DataGridViewRow row in dgvView.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cell.EditingCellFormattedValue) == true)
                {
                    string[] file = Directory.GetFiles(row.Cells["FileColumn"].Value.ToString());
                    Dictionary<string, string> dic = ReName.Rename(file.ToList());

                    foreach (var kvp in dic)
                    {

                        try
                        {
                            File.Move(kvp.Key, Path.GetDirectoryName(kvp.Key) + "\\" + kvp.Value);

                            DataRow dataRow = dt.NewRow();
                            dataRow["来源"] = Path.GetDirectoryName(kvp.Key);
                            dataRow["名称1"] = Path.GetFileName(kvp.Key);
                            dataRow["名称2"] = kvp.Value;
                            dt.Rows.Add(dataRow);
                        }
                        catch
                        {
                            errlist.Add(kvp.Key + "重命名出错");
                        }
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                ExcelHandler.Write(_outputPath+"\\重命名_"+DateTime.Now.ToString()+".xls", new List<DataTable>() { dt });
            }

            if (errlist.Count > 0)
            {
                MessageBox.Show(string.Join("\r\n", errlist));
            }
            else
            {
                MessageBox.Show("重命名完成");
            }
        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("输出路径为空");
                return;
            }

            foreach (DataGridViewRow row in dgvView.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cell.EditingCellFormattedValue) == true)
                {
                    _dirs.Add(row.Cells["FileColumn"].Value.ToString());

                }
            }


            _outputPath = txtPath.Text;
            if (_dirs.Count > 0)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = _dirs.Count;
                Thread t = new Thread(runThread);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void runThread()
        {
            List<string> errlist = new List<string>();
            foreach(var dir in _dirs)
            {
                try
                {
                    PDFhandler.ConvertPDF(dir,_outputPath);
                }
                catch (Exception ex)
                {
                    errlist.Add(ex.Message);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        progressBar1.Value++;
                    }));
                    _pdfChanged?.Invoke(dgvView, new PDFEventArgs(dir));
                }
            }

            this.Invoke(new MethodInvoker(delegate
            {
                if (errlist.Count > 0)
                {
                    MessageBox.Show(string.Join("\r\n", errlist));
                }
                else
                {
                    MessageBox.Show("转换完成");
                }
            }));
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
        private List<string> _dirs;
        private string _outputPath;

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
