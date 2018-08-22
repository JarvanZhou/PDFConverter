using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper
{
    public class ReName
    {
        public static int fileNewNum = 0;

        public static Dictionary<string, string> Rename(List<string> files)
        {

            Dictionary<string, string> filedic = new Dictionary<string, string>();
            
            var covs = (from string filename in files where Regex.IsMatch(Path.GetFileNameWithoutExtension(filename), @"\bcov\d+") select filename).ToList();
            var boks = (from string filename in files where Regex.IsMatch(Path.GetFileNameWithoutExtension(filename), @"\bbok\d+") select filename).ToList();
            var legs = (from string filename in files where Regex.IsMatch(Path.GetFileNameWithoutExtension(filename), @"\bleg\d+") select filename).ToList();
            var fows = (from string filename in files where Regex.IsMatch(Path.GetFileNameWithoutExtension(filename), @"\bfow\d+") select filename).ToList();
            var directorys = (from string filename in files where Regex.IsMatch(Path.GetFileNameWithoutExtension(filename), @"!\d+") select filename).ToList();
            var texts = (from string filename in files where Regex.IsMatch(Path.GetFileNameWithoutExtension(filename), @"^\d+") select filename).ToList();

            covs.Sort(SortClass.StrCmpLogicalW);
            boks.Sort(SortClass.StrCmpLogicalW);
            legs.Sort(SortClass.StrCmpLogicalW);
            fows.Sort(SortClass.StrCmpLogicalW);
            directorys.Sort(SortClass.StrCmpLogicalW);
            texts.Sort(SortClass.StrCmpLogicalW);

            //封面
            if (covs.Count > 2)
            {
                throw new Exception("封面张数有误：" + covs.Count);
            }
            else if (covs.Count > 0)
            {
                filedic.Add(covs[0], (++fileNewNum).ToString("00000000") + ".pdg");
            }
            //书名
            foreach (var bok in boks)
            {
                filedic.Add(bok, (++fileNewNum).ToString("00000000") + ".pdg");
            }
            //版权
            foreach (var leg in legs)
            {
                filedic.Add(leg, (++fileNewNum).ToString("00000000") + ".pdg");
            }
            //前言
            foreach (var fow in fows)
            {
                filedic.Add(fow, (++fileNewNum).ToString("00000000") + ".pdg");
            }
            //目录
            foreach (var dir in directorys)
            {
                filedic.Add(dir, (++fileNewNum).ToString("00000000") + ".pdg");
            }
            //正文
            foreach (var text in texts)
            {
                filedic.Add(text, (++fileNewNum).ToString("00000000") + ".pdg");
            }
            //封底
            if (covs.Count == 2)
            {
                filedic.Add(covs[1], (++fileNewNum).ToString("00000000") + ".pdg");
            }

            return filedic;
        }
    }
}
