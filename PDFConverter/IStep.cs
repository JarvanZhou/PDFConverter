using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFConverter
{
    public interface IStep
    {
        string SavePath
        { get; set; }
        List<string> Files
        { get; set; }
        DataGridView DataView
        { get; set; }
        void Work();
        void SetStep(IStep parentStep, IStep sonStep);
    }
}
