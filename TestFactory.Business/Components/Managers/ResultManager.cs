using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;
using TestFactory.Business.Models;
using System.Drawing;
using Ionic.Zip;
using Microsoft.Win32;

namespace TestFactory.Business.Components.Managers
{
    public class ResultManager
    {
        public void SaveToZip(IList<Student> students, IList<Category> category)
        {
            ZipFile zip = new ZipFile("E:\\GroupSave.zip");
            foreach (Student st in students)
            {
                var doc = ConvertToWord(st, category);
                string docName = "E:\\GroupSave\\" + st.FirstName + st.LastName + ".docx";
                doc.SaveAs2(docName);
                break;
            }
            zip.Save();
            
        }

        public void SaveToWord(Student student, IList<Category> category)
        {
            var doc = ConvertToWord(student, category);
            try
            {
                doc.Save();
                doc.Close();
            }
            catch (Exception)
            {
                //doc.Close();
            }
            
        }

        public Microsoft.Office.Interop.Word.Document ConvertToWord(Student student, IList<Category> category)
        {
            Microsoft.Office.Interop.Word.Application word = null;

            word = new Microsoft.Office.Interop.Word.Application();
            //word.Visible = true;
            Microsoft.Office.Interop.Word.Document doc = word.Documents.Add();

            var pText = doc.Paragraphs.Add();
            pText.Range.Font.Size = 14;
            pText.Range.Text += String.Format(student.FirstName + " " + student.LastName);
            pText.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            pText.Range.Bold = 1;

            Microsoft.Office.Interop.Word.Chart wdChart = doc.InlineShapes.AddChart(Microsoft.Office.Core.XlChartType.xlRadarFilled).Chart;

            Microsoft.Office.Interop.Word.ChartData chartData = wdChart.ChartData;

            Microsoft.Office.Interop.Excel.Workbook dataWorkbook = (Microsoft.Office.Interop.Excel.Workbook)chartData.Workbook;
            dataWorkbook.Application.Visible = false;
            
            Microsoft.Office.Interop.Excel.Worksheet dataSheet = (Microsoft.Office.Interop.Excel.Worksheet)dataWorkbook.Worksheets[1];

            Microsoft.Office.Interop.Excel.Range tRange = dataSheet.Cells.get_Range("A1", "B7");
            Microsoft.Office.Interop.Excel.ListObject tbl1 = dataSheet.ListObjects["Таблица1"];
            tbl1.Resize(tRange);

            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A1")).FormulaR1C1 = "легенда A";
            for (int i = 0; i < category.Count; i++)
            {
                int j = i + 2;
                ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A" + j)).FormulaR1C1 = category[i].Name;
            }


            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B1")).FormulaR1C1 = "Результати тесту по профорієнтації ДЖ. Холланда (" + student.FirstName + " " + student.LastName + ")";
            for (int i = 0; i < student.Marks.Count; i++)
            {
                int j = i + 2;
                ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B" + j)).FormulaR1C1 = student.Marks[i].Value + "%";
            }

            wdChart.Legend.Delete();

            for (int i = 0; i < category.Count; i++)
            {
                pText.Range.Text += String.Format(category[i].Code + "-(" + student.Marks[i].Value + ")—" + category[i].Name);
                pText.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;
                pText.Range.Bold = 0;
            }

            for (int i = 0; i < category.Count; i++)
            {
                pText.Range.InsertParagraphAfter();
                pText.Range.Font.Size = 14;
                pText.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                pText.Range.Bold = 1;
                pText.Range.Text += String.Format(category[i].Name + " тип");
                pText.Range.InsertParagraphAfter();
                pText.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;
                
                pText.Range.Text += String.Format("Короткий опис: ");
                pText.Range.Bold = 1;
                pText.Range.Text += String.Format(category[i].ShortDescription);
                pText.Range.Bold = 0;
                pText.Range.Text += String.Format("Детальний опис: ");
                pText.Range.Bold = 1;
                pText.Range.Text += String.Format(category[i].LongDescription);
                pText.Range.Bold = 0;
            }
            dataWorkbook.Application.Quit();

            return doc;
        }
    }
}
