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
                foreach (DataGridViewRow row in dgvView.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].EditedFormattedValue))
                    {
                        //此处添加合成功能
                        row.Cells[4].Value = "合成";
                    }
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
            if (!Directory.Exists(data[0])) return;
            _dirpath = data[0];
            lblTitle.Text = _dirpath.Substring(_dirpath.LastIndexOf("\\") + 1);
            Start(_dirpath);
        }

        private void dgvView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 0)
            {
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
                string[] zipfiles = Directory.GetFiles(_dirpath, "*.zip");
                string[] rarfiles = Directory.GetFiles(_dirpath, "*.rar");
                zip.Files.AddRange(zipfiles);
                zip.Files.AddRange(rarfiles);
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

    }
}
