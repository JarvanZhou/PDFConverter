using Helper;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            bool pic = false;
            bool pdg = false;
            foreach (DataGridViewRow row in dgvView.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].EditedFormattedValue))
                {
                    if (row.Cells[3].Value.ToString() == "图片")
                        pic = true;
                    else
                        pdg = true;
                }
            }
            if (pic && pdg)
            { MessageBox.Show("不可同时选中图片和PDG两种模式"); return; }
            else if (pic)
            {
                _dirList = new Dictionary<int, string>();
                for (int i = 0; i < dgvView.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvView.Rows[i].Cells[0].EditedFormattedValue))
                    {

                        _dirList.Add(i, dgvView.Rows[i].Cells["FileColumn"].Value.ToString());
                    }
                }

                if (_dirList.Count > 0)
                {
                    progressBar1.Maximum = _dirList.Count;
                    progressBar1.Value = 0;
                    Thread t = new Thread(run);
                    t.IsBackground = true;
                    t.Start();
                }

            }
            else if (pdg)
            {
                List<string> files = new List<string>();

                foreach (DataGridViewRow row in dgvView.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].EditedFormattedValue))
                    {
                        files.Add(row.Cells["FileColumn"].Value.ToString());
                    }
                }
                PDGFrom pdgform = new PDGFrom();
                pdgform.SetData(lblTitle.Text, files.ToArray());
                pdgform.PDFChanged += Pdgform_PDFChanged;
                pdgform.ShowDialog();
            }
        }

        private void run()
        {

            //PDGFrom pDGFrom = new PDGFrom();
            List<string> errList = new List<string>();

            Dictionary<string, string> TotalDic = new Dictionary<string, string>();
            Dictionary<string, List<string>> resDic = new Dictionary<string, List<string>>();
            foreach(var kvp in _dirList)
            {

                string[] file = Directory.GetFiles(kvp.Value);
                Dictionary<string, string> dic = ReName.Rename(file.ToList());
                dic.ToList().ForEach(x => TotalDic.Add(x.Key, x.Value));

            }

            foreach(var kvp in TotalDic)
            {
                string outFilePath = Path.GetDirectoryName(kvp.Key) + "\\" + Path.GetFileNameWithoutExtension(kvp.Value) + Path.GetExtension(kvp.Key);
                File.Move(kvp.Key, outFilePath);
                string bookname = Path.GetFileName(Path.GetDirectoryName(kvp.Key));
                if (resDic.ContainsKey(bookname))
                {
                    resDic[bookname].Add(outFilePath);
                }
                else
                {
                    resDic.Add(bookname, new List<string>() { outFilePath });
                }
            }

            foreach (var kvp in resDic)
            {

                try
                {
                    PDFhandler.ConvertPDF(Path.GetDirectoryName(kvp.Value[0]), _dirpath);
                }
                catch (Exception ex)
                {
                    errList.Add(ex.Message);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        progressBar1.Value++;
                        //dgvView.Rows[kvp.Key].Cells[4].Value = "合成";
                    }));
                }
            }

            this.Invoke(new MethodInvoker(delegate
            {
                if (errList.Count > 0)
                {
                    MessageBox.Show(string.Join("\r\n", errList));
                }
                else
                {
                    MessageBox.Show("转换完成");
                }
            }));
        }

        private void Pdgform_PDFChanged(object sender, PDFEventArgs e)
        {
            foreach (DataGridViewRow row in dgvView.Rows)
            {
                if (e.File == row.Cells["FileColumn"].Value.ToString())
                {
                    row.Cells[5].Value = "导出";
                }
            }
        }

        private void dgvView_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (Directory.Exists(data[0]))
            {
                _dirpath = data[0];
                _filepath = "";
            }
            else if (File.Exists(data[0]) && (data[0].EndsWith(".zip") || data[0].EndsWith(".rar")))
            {
                _dirpath = Path.GetDirectoryName(data[0]);
                _filepath = data[0];
            }
            lblTitle.Text = _dirpath.Substring(_dirpath.LastIndexOf("\\") + 1);
            Start(_dirpath);
        }

        private void dgvView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 0)
            {
                if (dgvView.Rows[e.RowIndex].Cells[2].Value.ToString() == "") return;
                List<string> files = new List<string>();
                foreach (DataGridViewRow row in dgvView.Rows)
                {
                    DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(cell.EditingCellFormattedValue) == true)
                    {
                        files.Add(row.Cells["FileColumn"].Value.ToString());
                    }
                }
                string info = FilesInfo.GetFilesInfo(files.ToArray());
                lblInfo.Text = info;
            }
        }

        private void dgvView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void Start(string dirpath)
        {
            Task.Factory.StartNew(() =>
            {
                ZipStep zip = new ZipStep();
                CheckStep check = new CheckStep();
                zip.SavePath = dirpath;
                if (!string.IsNullOrWhiteSpace(_filepath))
                {
                    zip.Files.Add(_filepath);
                }
                else
                {
                    string[] zipfiles = Directory.GetFiles(_dirpath, "*.zip");
                    string[] rarfiles = Directory.GetFiles(_dirpath, "*.rar");
                    if (zipfiles != null && zipfiles.Length > 0)
                        zip.Files.AddRange(zipfiles);
                    if (rarfiles != null && rarfiles.Length > 0)
                        zip.Files.AddRange(rarfiles);
                }
                zip.Files.Sort();
                zip.SetStep(null, check);
                zip.DataView = dgvView;
                check.DataView = dgvView;
                check.SavePath = dirpath;
                check.SetStep(zip, null);
                zip.Work();
            });
        }


        private string _dirpath;
        private Dictionary<int, string> _dirList;
        private string _filepath;
    }
}
