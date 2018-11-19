using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace PlanMyWeb.Models
{
    public class PlanMyWebContext : DbContext
    {
        public PlanMyWebContext (DbContextOptions<PlanMyWebContext> options)
            : base(options)
        {
        }

        public DbSet<DAL.HomeTips> HomeTips { get; set; }
    }
}
