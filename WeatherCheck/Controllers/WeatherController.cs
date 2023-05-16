using System;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherCheck.Data;
using WeatherCheck.Models;
using System.Data;
using WeatherCheck.Help;
using Microsoft.Data.SqlClient;

namespace WeatherCheck.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherCheckContext _context;

        public WeatherController(WeatherCheckContext context)
        {
            _context = context;
        }

        public PaginatedList<Weather> WeatherPaginatedList { get; set; }


        public async Task<IActionResult> Index(int? pageNumber, string searchMonth, string searchYear)
        {
            int pageSize = 10;

            IQueryable<Weather> weatherLog =
                _context.Weather.OrderByDescending(x => x.Date);

            if (!string.IsNullOrEmpty(searchMonth) && int.TryParse(searchMonth, out var month))
            {
                weatherLog = weatherLog.Where(x => x.Date.Month == month);
            }
            if (!string.IsNullOrEmpty(searchYear) && int.TryParse(searchYear, out var year))
            {
                weatherLog = weatherLog.Where(x => x.Date.Year == year);
            }
            WeatherPaginatedList = await PaginatedList<Weather>.CreateAsync(weatherLog.AsNoTracking(), pageNumber ?? 1, pageSize);

            return View(WeatherPaginatedList);
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(List<IFormFile> files)
        {
            if (files.Count <= 0)
                return View();

            DataTable dt = new DataTable();

            foreach (var file in files)
            {
                string FileName = Path.GetExtension(file.FileName);
                if (FileName != ".xls" && FileName != ".xlsx")
                {
                    continue;
                }
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {

                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    try
                    {
                        if (FileName == ".xls")
                        {
                            HSSFWorkbook workbook = new HSSFWorkbook(stream);
                            dt = ExcelService.GetDataTableFromExcel(dt, workbook);
                        }
                        else
                        {
                            XSSFWorkbook workbook = new XSSFWorkbook(stream);
                            dt = ExcelService.GetDataTableFromExcel(dt, workbook);
                        }

                    }
                    catch (Exception e)
                    {
                        ViewData["ErrorImport"] = "Ошибка при импортировании данных: \n" + e.Message;
                        continue;
                    }
                }
            }

            try
            {
                string consString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WeatherCheckContext-83b70fb5-d984-477b-a6b7-8f823f1039bf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.Weather";
                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Date", "Date");
                        sqlBulkCopy.ColumnMappings.Add("Time", "Time");
                        sqlBulkCopy.ColumnMappings.Add("Temperature", "Temperature");
                        sqlBulkCopy.ColumnMappings.Add("RelativeHumidity", "RelativeHumidity");
                        sqlBulkCopy.ColumnMappings.Add("DewPoint", "DewPoint");
                        sqlBulkCopy.ColumnMappings.Add("AtmosphericPressure", "AtmosphericPressure");
                        sqlBulkCopy.ColumnMappings.Add("WindDirection", "WindDirection");
                        sqlBulkCopy.ColumnMappings.Add("WindSpeed", "WindSpeed");
                        sqlBulkCopy.ColumnMappings.Add("Cloudiness", "Cloudiness");
                        sqlBulkCopy.ColumnMappings.Add("CloudCeiling", "CloudCeiling");
                        sqlBulkCopy.ColumnMappings.Add("HorizontalVisibility", "HorizontalVisibility");
                        sqlBulkCopy.ColumnMappings.Add("WeatherEvents", "WeatherEvents");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
                ViewData["SQLResult"] = "Данные успешно добавлены. Количество строк: " + dt.Rows.Count;
            }
            catch(Exception e)
            {
                ViewData["SQLResult"] = "Ошибка при сохранении в БД: \n" + e.Message;         
            }

            return View();

        }
    }    
  
}
