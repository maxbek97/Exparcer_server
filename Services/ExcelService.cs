//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace server.Services
//{
//    public class ExcelService
//    {
//        private readonly Stream _excelStream;
//        private int STR_IND_COUNT = 11;
//        private int COL_NUM_INDEX = 1;
//        private int COL_NAME_INDEX = 4;
//        private int COL_APRDATE_INDEX = 5;
//        private int COL_DESIGNATION_INDEX = 6;
//        private int COL_CHECKDATE_INDEX = 7;
//        public ExcelService(Stream excelStream)
//        {
//            _excelStream = excelStream;
//        }

//        public string Process()
//        {
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//            using var package = new ExcelPackage(_excelStream);
//            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
//            if (worksheet == null)
//            {
//                Console.WriteLine("❌ В файле нет листов");
//                return "❌ В файле нет листов";
//            }

//            var rowCount = worksheet.Dimension.Rows;

//            // Регексы для проверки
//            var regexNumber = new Regex(@"^[0-9.]+$");
//            var regexDate = new Regex(@"^\d{2}\.\d{2}\.\d{4}( ?г\.)?$");

//            var unaccepted_rows_indicies = new List<int>();
//            var current_category = "";
//            for (int row = STR_IND_COUNT + 1; row <= rowCount; row++)
//            {
//                var cell = worksheet.Cells[row, COL_NAME_INDEX];
//                if (cell.Merge)
//                {
//                    var mergedId = worksheet.Cells[row, COL_NAME_INDEX].;
//                    var mergedRange = ws.MergedCells[mergedId - 1];
//                    var topLeft = new ExcelAddress(mergedRange).Start;
//                    return ws.Cells[topLeft.Row, topLeft.Column].Text?.Trim();
//                    current_category = worksheet.Cells[row, COL_NAME_INDEX].Text?.Trim();
//                    continue;
//                }
//                var number = worksheet.Cells[row, COL_NUM_INDEX].Text?.Trim();
//                var name = worksheet.Cells[row, COL_NAME_INDEX].Text?.Trim();
//                var approvalDate = worksheet.Cells[row, COL_APRDATE_INDEX].Text?.Trim();
//                var designation = worksheet.Cells[row, COL_DESIGNATION_INDEX].Text?.Trim();
//                var checkDate = worksheet.Cells[row, COL_CHECKDATE_INDEX].Text?.Trim();

//                var rowData = $"{number} | {name} | {approvalDate} | {designation} | {checkDate} | {current_category}";
//                if (
//                    !string.IsNullOrWhiteSpace(number) && regexNumber.IsMatch(number) &&
//                    !string.IsNullOrWhiteSpace(name) &&
//                    !string.IsNullOrWhiteSpace(approvalDate) && regexDate.IsMatch(approvalDate) &&
//                    !string.IsNullOrWhiteSpace(designation) &&
//                    !string.IsNullOrWhiteSpace(checkDate) && regexDate.IsMatch(checkDate)
//                   )
//                {
//                    Console.WriteLine($"ПРАВИЛЬНО: {rowData}");
//                    //var record = new 
//                }
//                else
//                {
//                    Console.WriteLine($"НЕПРАВИЛЬНО: {rowData}");
//                    unaccepted_rows_indicies.Add(row);
//                }
//            }
//            return $"Проверьте индексы: {String.Join(", ", unaccepted_rows_indicies)}";
//        }
//    }
//}
