using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cShapSolution.Data.EntitiesFramewor
{
    public class cShopDbContextFactory : IDesignTimeDbContextFactory<cShopDbContext>
    {
        public cShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("cShopSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<cShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new cShopDbContext(optionsBuilder.Options);

        }
    }

}
