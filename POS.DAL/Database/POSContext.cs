using Microsoft.EntityFrameworkCore;
using POS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL.Database
{
    public class POSContext : DbContext
    {
        public POSContext(DbContextOptions<POSContext> options) : base(options)
        {
        }
        DbSet<Source> Sources { get; set; }
        DbSet<SourceType> SourceTypes { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Governorate> Governorates { get; set; }
        DbSet<City> Cities { get; set; }
    }
}
