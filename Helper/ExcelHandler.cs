using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ExcelHandler
    {
        public static DataTable ReadExcel(string path)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                IWorkbook workbook = new HSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheetAt(0);
                int rfirst = sheet.FirstRowNum;
                int rlast = sheet.LastRowNum;
                IRow row = sheet.GetRow(rfirst);
                int cfirst = row.FirstCellNum;
                int clast = row.LastCellNum;
                for (int i = cfirst; i < clast; i++)
                {
                    if (row.GetCell(i) != null)
                    {
                        dt.Columns.Add(row.GetCell(i).StringCellValue, typeof(string));
                    }
                    else
                    {
                        throw new Exception("获取Excel行1列" + (i + 1).ToString() + "数据失败");
                    }
                }
                row = null;
                for (int i = rfirst + 1; i <= rlast; i++)
                {
                    DataRow r = dt.NewRow();
                    IRow ir = sheet.GetRow(i);
                    for (int j = cfirst; j < clast; j++)
                    {
                        if (ir != null && ir.GetCell(j) != null)
                        {
                            r[j - cfirst] = ir.GetCell(j).ToString();
                        }
                        else
                        {
                            r[j - cfirst] = string.Empty;
                        }
                    }
                    dt.Rows.Add(r);
                    ir = null;
                    r = null;
                }
                sheet = null;
                workbook = null;
                fs.Close();
            }
            return dt;
        }

        public static bool Write(string path, List<DataTable> dtList)
        {
            try
            {

                HSSFWorkbook wb = new HSSFWorkbook();
                for (int i = 0; i < dtList.Count; i++)
                {
                    //sheet
                    ISheet sheet = wb.CreateSheet("sheet" + (i + 1));
                    int rowStartIndex = 0;
                    //表头
                    IRow rowName = sheet.CreateRow(rowStartIndex);
                    rowName.HeightInPoints = 40;
                    for (int j = 0; j < dtList[i].Columns.Count; j++)
                    {
                        sheet.SetColumnWidth(j, 20 * 256);
                        ICell cell = rowName.CreateCell(j);
                        cell.SetCellValue(dtList[i].Columns[j].ColumnName);
                    }
                    //内容
                    for (int j = 0; j < dtList[i].Rows.Count; j++)
                    {
                        IRow row = sheet.CreateRow(j + rowStartIndex + 1);
                        row.HeightInPoints = 20;
                        for (int k = 0; k < dtList[i].Columns.Count; k++)
                        {
                            ICell cell = row.CreateCell(k);
                            int value = 0;
                            if (int.TryParse(dtList[i].Rows[j][k].ToString(), out value))
                            {
                                cell.SetCellValue(value);
                            }
                            else
                            {
                                cell.SetCellValue(dtList[i].Rows[j][k].ToString());
                            }
                        }
                    }
                }

                using (FileStream fs = new FileStream(path, FileMode.Create)) wb.Write(fs);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
