using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Helper
{
    public static class FilesInfo
    {
        public static string GetFilesInfo(string[] files)
        {
            int pages = 0;
            long size = 0;
            int count = files.Length;
            foreach (var file in files)
            {
                DirectoryInfo dic = new DirectoryInfo(file);
                FileInfo[] filedata = dic.GetFiles("*.*");
                pages += filedata.Length;
                foreach (var d in filedata)
                {
                    size += d.Length;
                }
            }
            float filesize = size / 1024.0f / 1024.0f;

            return "已选" + count + "本书，共" + pages + "页，共" + filesize.ToString("0.00") + "MB";
        }
    }
}
