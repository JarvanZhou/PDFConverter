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
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("输出路径为空");
                return;
            }
            _outputPath = txtPath.Text;

            _dirs = new List<string>();

            foreach (DataGridViewRow row in dgvView.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cell.EditingCellFormattedValue) == true)
                {
                    _dirs.Add(row.Cells["FileColumn"].Value.ToString());
                }
            }

            if (_dirs.Count > 0)
            {
                ReName.fileNewNum = 0;
                SetBtnEnable(false);

                Thread t = new Thread(runRename);
                t.IsBackground = true;
                t.Start();
            }

        }

        private void runRename()
        {
            _outputPath = txtPath.Text;
            List<string> errlist = new List<string>();

            DataTable dt = new DataTable();
            dt.Columns.Add("来源");
            dt.Columns.Add("名称1");
            dt.Columns.Add("名称2");
            Dictionary<string, string> TotalDic = new Dictionary<string, string>();

            foreach (var dir in _dirs)
            {
                try
                {
                    string[] file = Directory.GetFiles(dir);
                    Dictionary<string, string> dic = ReName.Rename(file.ToList());

                    dic.ToList().ForEach(x => TotalDic.Add(x.Key, x.Value));
                }
                catch (Exception ex)
                {
                    errlist.Add(ex.Message);
                }
            }

            this.Invoke(new MethodInvoker(delegate
            {

                progressBar1.Value = 0;
                progressBar1.Maximum = TotalDic.Count;
            }));

            foreach (var kvp in TotalDic)
            {

                try
                {
                    File.Move(kvp.Key, _outputPath + "\\" + kvp.Value);

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
                finally
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        progressBar1.Value++;
                    }));
                }
            }

            if (dt.Rows.Count > 0)
            {
                string path = _outputPath + "\\重命名_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                ExcelHandler.Write(path, new List<DataTable>() { dt });
            }

            this.Invoke(new MethodInvoker(delegate
            {

                if (errlist.Count > 0)
                {
                    MessageBox.Show(string.Join("\r\n", errlist));
                }
                else
                {
                    MessageBox.Show("重命名完成");
                }
                SetBtnEnable(true);
            }));
        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("输出路径为空");
                return;
            }
            _dirs = new List<string>();
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
                //progressBar1.Value = 0;
                //progressBar1.Maximum = _dirs.Count;
                SetBtnEnable(false);

                Thread t = new Thread(runThread);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void runThread()
        {
            List<string> errlist = new List<string>();
            try
            {
                PDFhandler.ConvertPDF(_outputPath, _outputPath);
            }
            catch (Exception ex)
            {
                errlist.Add(ex.Message);
            }
            finally
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    foreach (var dir in _dirs)
                    {
                        _pdfChanged?.Invoke(dgvView, new PDFEventArgs(dir));
                    }
                }));

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
                SetBtnEnable(true);
            }));
        }

        private void SetBtnEnable(bool enable)
        {
            btnCombine.Enabled = enable;
            btnDir.Enabled = enable;
            btnExe.Enabled = enable;
            btn_rename.Enabled = enable;
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
