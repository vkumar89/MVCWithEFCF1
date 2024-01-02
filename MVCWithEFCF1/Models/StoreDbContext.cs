using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCWithEFCF1.Models
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }

    }
}