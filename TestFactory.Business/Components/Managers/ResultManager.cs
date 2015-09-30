using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;
using TestFactory.Business.Models;
using System.Drawing;

namespace TestFactory.Business.Components.Managers
{
    public class ResultManager
    {
        public void SaveToWord(Student student, IList<Category> category)
        {
            Microsoft.Office.Interop.Word.Application word = null;

            word = new Microsoft.Office.Interop.Word.Application();
            word.Visible = true;
            Microsoft.Office.Interop.Word.Document doc = word.Documents.Add();

            var pText = doc.Paragraphs.Add();
            pText.Format.SpaceAfter = 10f;
            pText.Range.Text = String.Format("This is line #{0}", 1);
            pText.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Chart wdChart = doc.InlineShapes.AddChart(Microsoft.Office.Core.XlChartType.xlRadarFilled).Chart;

            Microsoft.Office.Interop.Word.ChartData chartData = wdChart.ChartData;

            Microsoft.Office.Interop.Excel.Workbook dataWorkbook = (Microsoft.Office.Interop.Excel.Workbook)chartData.Workbook;
            Microsoft.Office.Interop.Excel.Worksheet dataSheet = (Microsoft.Office.Interop.Excel.Worksheet)dataWorkbook.Worksheets[1];

            Microsoft.Office.Interop.Excel.Range tRange = dataSheet.Cells.get_Range("A1", "B7");
            Microsoft.Office.Interop.Excel.ListObject tbl1 = dataSheet.ListObjects["Таблица1"];
            tbl1.Resize(tRange);
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A1")).FormulaR1C1 = "легенда A";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A2")).FormulaR1C1 = "Рациональный";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A3")).FormulaR1C1 = "Артистический";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A4")).FormulaR1C1 = "Исследовательский";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A5")).FormulaR1C1 = "Социальный";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A6")).FormulaR1C1 = "Предпринимательский";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("A7")).FormulaR1C1 = "Систематический";

            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B1")).FormulaR1C1 = "";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B2")).FormulaR1C1 = "90%";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B3")).FormulaR1C1 = "55%";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B4")).FormulaR1C1 = "85%";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B5")).FormulaR1C1 = "90%";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B6")).FormulaR1C1 = "90%";
            ((Microsoft.Office.Interop.Excel.Range)dataSheet.Cells.get_Range("B7")).FormulaR1C1 = "90%";


            wdChart.ChartTitle.Font.Size = 18;
          // wdChart.ChartTitle.Font.Color = Color.Black.ToArgb();
            wdChart.ChartTitle.Text = "Имя Фамалия";



            dataWorkbook.Application.Quit();

        }
    }
}
