using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherCheck.Models;

namespace WeatherCheck.Data
{
    public class WeatherCheckContext : DbContext
    {
        public WeatherCheckContext (DbContextOptions<WeatherCheckContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherCheck.Models.Weather> Weather { get; set; }
    }
}
