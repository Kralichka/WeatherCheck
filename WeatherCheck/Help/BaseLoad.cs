using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherCheck.Help
{
    public static class BaseLoad
    {
        //public static 


        public static List<SelectListItem> MonthList = new List<SelectListItem>()
        {
                new SelectListItem("", null),
                new SelectListItem("Январь", "1"),
                new SelectListItem("Февраль", "2"),
                new SelectListItem("Март", "3"),
                new SelectListItem("Апрель", "4"),
                new SelectListItem("Май", "5"),
                new SelectListItem("Июнь", "6"),
                new SelectListItem("Июль", "7"),
                new SelectListItem("Август", "8"),
                new SelectListItem("Сентябрь", "9"),
                new SelectListItem("Октябрь", "10"),
                new SelectListItem("Ноябрь", "11"),
                new SelectListItem("Декабрь", "12")
        };
    }
}
