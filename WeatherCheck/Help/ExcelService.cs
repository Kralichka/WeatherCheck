using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;

namespace WeatherCheck.Help
{
    public class ExcelService
    {
        /// <summary>
        /// Import data from Excel .xlsx to DataTable.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="wb"></param>
        /// <returns>DataTable with data from Excel.</returns>
        public static DataTable GetDataTableFromExcel(DataTable dt, XSSFWorkbook wb)
        {

            if (dt.Rows.Count == 0)
            {
                dt.Columns.Add("Date", typeof(DateTime));
                dt.Columns.Add("Time", typeof(DateTime));
                dt.Columns.Add("Temperature", typeof(float));
                dt.Columns.Add("RelativeHumidity", typeof(float));
                dt.Columns.Add("DewPoint", typeof(float));
                dt.Columns.Add("AtmosphericPressure", typeof(short));
                dt.Columns.Add("WindDirection", typeof(string));
                dt.Columns.Add("WindSpeed", typeof(byte));
                dt.Columns.Add("Cloudiness", typeof(byte));
                dt.Columns.Add("CloudCeiling", typeof(short));
                dt.Columns.Add("HorizontalVisibility", typeof(string));
                dt.Columns.Add("WeatherEvents", typeof(string));
            }
            for (int shNumber = 0; shNumber < 12; shNumber++)
            {
                XSSFSheet sh = (XSSFSheet)wb.GetSheetAt(shNumber);

                int xlsRow = 4;
                while (sh.GetRow(xlsRow) != null)
                {
                    if (dt.Columns.Count < sh.GetRow(xlsRow).Cells.Count)
                    {
                        for (int j = 0; j < sh.GetRow(xlsRow).Cells.Count; j++)
                        {
                            dt.Columns.Add("", typeof(string));
                        }
                    }

                    dt.Rows.Add();
                    var dtRowsCount = dt.Rows.Count;

                    for (int j = 0; j < sh.GetRow(xlsRow).Cells.Count; j++)
                    {
                        var cell = sh.GetRow(xlsRow).GetCell(j);

                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Numeric:
                                    dt.Rows[dtRowsCount - 1][j] = sh.GetRow(xlsRow).GetCell(j).NumericCellValue;
                                    break;

                                case CellType.String:
                                    if (!string.IsNullOrWhiteSpace(sh.GetRow(xlsRow).GetCell(j).StringCellValue.ToString()))
                                        dt.Rows[dtRowsCount - 1][j] = sh.GetRow(xlsRow).GetCell(j).StringCellValue;
                                    break;

                                case CellType.Blank:
                                    break;
                            }
                        }
                    }
                    xlsRow++;
                }
            }

            return dt;
        }

        /// <summary>
        /// Import data from Excel .xls to DataTable.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="wb"></param>
        /// <returns>DataTable with data from Excel.</returns>
        public static DataTable GetDataTableFromExcel(DataTable dt, HSSFWorkbook wb)
        {

            if (dt.Rows.Count == 0)
            {
                dt.Columns.Add("Date", typeof(DateTime));
                dt.Columns.Add("Time", typeof(DateTime));
                dt.Columns.Add("Temperature", typeof(float));
                dt.Columns.Add("RelativeHumidity", typeof(float));
                dt.Columns.Add("DewPoint", typeof(float));
                dt.Columns.Add("AtmosphericPressure", typeof(short));
                dt.Columns.Add("WindDirection", typeof(string));
                dt.Columns.Add("WindSpeed", typeof(byte));
                dt.Columns.Add("Cloudiness", typeof(byte));
                dt.Columns.Add("CloudCeiling", typeof(short));
                dt.Columns.Add("HorizontalVisibility", typeof(string));
                dt.Columns.Add("WeatherEvents", typeof(string));
            }
            for (int shNumber = 0; shNumber < 12; shNumber++)
            {
                HSSFSheet sh = (HSSFSheet)wb.GetSheetAt(shNumber);

                int xlsRow = 4;
                while (sh.GetRow(xlsRow) != null)
                {
                    if (dt.Columns.Count < sh.GetRow(xlsRow).Cells.Count)
                    {
                        for (int j = 0; j < sh.GetRow(xlsRow).Cells.Count; j++)
                        {
                            dt.Columns.Add("", typeof(string));
                        }
                    }

                    dt.Rows.Add();
                    var dtRowsCount = dt.Rows.Count;

                    for (int j = 0; j < sh.GetRow(xlsRow).Cells.Count; j++)
                    {
                        var cell = sh.GetRow(xlsRow).GetCell(j);

                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Numeric:
                                    dt.Rows[dtRowsCount - 1][j] = sh.GetRow(xlsRow).GetCell(j).NumericCellValue;
                                    break;

                                case CellType.String:
                                    if (!string.IsNullOrWhiteSpace(sh.GetRow(xlsRow).GetCell(j).StringCellValue.ToString()))
                                        dt.Rows[dtRowsCount - 1][j] = sh.GetRow(xlsRow).GetCell(j).StringCellValue;
                                    break;

                                case CellType.Blank:
                                    break;
                            }
                        }
                    }
                    xlsRow++;
                }
            }

            return dt;
        }
    }
}
