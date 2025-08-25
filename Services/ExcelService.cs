using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;

namespace server.Services
{
    public class ExcelService
    {
        private readonly Stream _excelStream;
        private int STR_IND_COUNT = 11;
        public ExcelService(Stream excelStream)
        {
            _excelStream = excelStream;
        }

        public void Process()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(_excelStream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                Console.WriteLine("❌ В файле нет листов");
                return;
            }

            var rowCount = worksheet.Dimension.Rows;

            // Регексы для проверки
            var regexNumber = new Regex(@"^[0-9.]+$");
            var regexDate = new Regex(@"^\d{2}\.\d{2}\.\d{4}( ?г\.)?$");

            for (int row = STR_IND_COUNT + 1; row <= rowCount; row++)
            {
                var number = worksheet.Cells[row, 1].Text?.Trim();
                var name = worksheet.Cells[row, 4].Text?.Trim();
                var approvalDate = worksheet.Cells[row, 5].Text?.Trim();
                var designation = worksheet.Cells[row, 6].Text?.Trim();
                var checkDate = worksheet.Cells[row, 7].Text?.Trim();

                bool valid = true;

                if (string.IsNullOrWhiteSpace(number) || !regexNumber.IsMatch(number))
                {
                    number += "false";
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    name += "false";
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(approvalDate) || !regexDate.IsMatch(approvalDate))
                {
                    approvalDate += "false";
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(designation))
                {
                    designation += "false";
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(checkDate) || !regexDate.IsMatch(checkDate))
                {
                    checkDate += "false";
                    valid = false;
                }

                var rowData = $"{number} | {name} | {approvalDate} | {designation} | {checkDate}";

                if (valid)
                    Console.WriteLine($"ПРАВИЛЬНО: {rowData}");
                else
                    Console.WriteLine($"НЕПРАВИЛЬНО: {rowData}");
            }
        }
    }
}
