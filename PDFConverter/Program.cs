using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PDFConverter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //List<string> files = Directory.GetFiles(@"C:\Users\jarvan\Desktop\需求\中文书测试\测试文件\宠物公墓_12380050").ToList();
            //files.Sort(SortClass.StrCmpLogicalW);
            //PDFhandler.Convert(files, @"C:\Users\jarvan\Desktop\需求\中文书测试\测试文件\test.pdf");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
