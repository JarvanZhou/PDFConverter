using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFConverter
{
    class CheckStep : IStep
    {
        public CheckStep()
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
            if (_parent == null)
            { throw new Exception("parentStep 不得为NULL"); }
            if (_parent.Files == null)
            { throw new Exception("输入Files不得为空"); }
            if (!Directory.Exists(_parent.SavePath))
            { throw new Exception("SavePath为空"); }
            if (_parent.DataView == null)
            { throw new Exception("DataView不得为NULL"); }
            Files = _parent.Files;
            foreach (DataGridViewRow row in _parent.DataView.Rows)
            {
                string filepath = row.Cells["FileColumn"].Value.ToString();
                string[] files = Directory.GetFiles(filepath, "*.*");
                int count = (from file in files where (!file.EndsWith(".jpg") && !file.EndsWith(".pdg") && !file.EndsWith(".bmp") && !file.EndsWith(".png")&& !file.EndsWith(".tif")) select file).Count();
                if (count > 0)
                {
                    foreach (var f in files)
                    {
                        if (!f.EndsWith(".jpg") && !f.EndsWith(".pdg") && !f.EndsWith(".bmp") && !f.EndsWith(".png") && !f.EndsWith(".tif"))
                            File.Delete(f);
                    }
                }
                count = (from file in files where file.EndsWith(".pdg") select file).Count();
                _parent.DataView.Invoke((MethodInvoker)(() =>
                {
                    if (count > 0)
                        row.Cells[3].Value = "pdg";
                    else
                        row.Cells[3].Value = "图片";
                }));
            }
            if (_son != null)
                _son.Work();
        }

        private IStep _parent;
        private IStep _son;
    }
}
