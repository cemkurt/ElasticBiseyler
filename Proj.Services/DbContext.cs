using Microsoft.EntityFrameworkCore;
using Proj.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

    }
}

public static partial class ApplicationDbInitializer
{

    public static void SeedData(DatabaseContext databaseContext)
    {
        SeedCategory(databaseContext);
    }

    private static void SeedCategory(DatabaseContext databaseContext)
    {
        if (!databaseContext.Categories.Any())
        {
            var category = new List<Category>()
            {
                    new Category{ Name="Bilgisayar"},
                    new Category{ Name="Yazılım"},
                    new Category{ Name="Donanım"}
            };

            databaseContext.Categories.AddRange(category);
            databaseContext.SaveChanges();
        }
    }
}