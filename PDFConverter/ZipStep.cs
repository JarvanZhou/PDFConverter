using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFConverter
{
    class ZipStep : IStep
    {
        public ZipStep()
        { Files = new List<string>(); }
        public string SavePath
        { get; set; }
        public List<string> Files
        { get; set; }
        public DataGridView DataView
        { get; set; }
        public void SetStep(IStep parentStep, IStep sonStep)
        {
            _parent = parentStep;
            _son = sonStep;
        }
        public void Work()
        {
            string savepath = "";
            List<string> files;
            DataGridView dgvView = null;
            if (_parent == null)
            {
                savepath = SavePath;
                files = Files;
                dgvView = DataView;
            }
            else
            {
                savepath = _parent.SavePath;
                files = _parent.Files;
                dgvView = _parent.DataView;
            }
            if (files == null)
            { throw new Exception("输入Files不得为空"); }
            if (!Directory.Exists(savepath))
            { throw new Exception("SavePath为空"); }
            if (dgvView == null)
            { throw new Exception("DataView不得为NULL"); }
            foreach (var file in files)
            {
                string name = Path.GetFileNameWithoutExtension(file);
                dgvView.Invoke((MethodInvoker)(() => dgvView.Rows.Add(false, name, "", "", "", "", file)));
            }
            List<string> result = new List<string>();
            foreach (DataGridViewRow row in dgvView.Rows)
            {
                string save = savepath + "\\" + Path.GetFileNameWithoutExtension(row.Cells[1].Value.ToString()) + "\\";
                if (!Directory.Exists(save))
                { Directory.CreateDirectory(save); }
                bool flag = UnZip(row.Cells["FileColumn"].Value.ToString(), save);
                if (flag)
                { result.Add(save); }
                dgvView.Invoke((MethodInvoker)(() =>
                {
                    row.Cells[2].Value = row.Cells[1].Value;
                    row.Cells["FileColumn"].Value = save;
                }));
            }
            Files = result;
            if (_son != null)
                _son.Work();
        }

        private bool UnZip(string file, string savepath)
        {
            try
            {
                using (Stream stream = File.OpenRead(file))
                {
                    var reader = ReaderFactory.Open(stream);
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            reader.WriteEntryToDirectory(savepath);
                        }
                    }
                }
                return true;
            }
            catch
            { return false; }
        }


        private IStep _parent;
        private IStep _son;
    }
}
