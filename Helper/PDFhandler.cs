using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class PDFhandler
    {

        public static void Convert(List<string> files, string PDFfile)
        {
            files.Sort(SortClass.StrCmpLogicalW);
            using (Document document = new Document())
            {

                PdfWriter.GetInstance(document, new FileStream(PDFfile, FileMode.Create));
                document.Open();
                foreach (var file in files)
                {
                    Image bitmap = Image.GetInstance(file);
                    bitmap.ScalePercent((float)55000 / bitmap.Width);
                    document.Add(bitmap);
                }
                document.Close();
            }
        }
    }
}
