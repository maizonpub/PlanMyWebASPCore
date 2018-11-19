using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DbWebContext: DbContext
    {
        public DbWebContext(DbContextOptions<DbWebContext> options)
            : base(options)
        {
        }

        public DbSet<DAL.HomeSlider> HomeSlider { get; set; }

        public DbSet<DAL.HomeTips> HomeTips { get; set; }
    }
}
