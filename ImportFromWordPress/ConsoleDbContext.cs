using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportFromWordPress
{
    public class consoleDbContext : IdentityDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-BGV6L8N;Database=PlanMy;Trusted_Connection=True;MultipleActiveResultSets=true;user id=sa;password=password;");
        }
        public DbSet<DAL.HomeSlider> HomeSlider { get; set; }

        public DbSet<DAL.HomeTips> HomeTips { get; set; }

        public DbSet<DAL.WebContent> WebContents { get; set; }

        public DbSet<DAL.Pages> Pages { get; set; }

        public DbSet<DAL.SocialMedia> SocialMedias { get; set; }

        public DbSet<DAL.Blog> Blogs { get; set; }

        public DbSet<DAL.BlogCategory> BlogCategories { get; set; }

        public DbSet<DAL.BlogCategoryRelation> BlogCategoryRelations { get; set; }

        public DbSet<DAL.VendorCategory> VendorCategories { get; set; }

        public DbSet<DAL.VendorItem> VendorItems { get; set; }

        public DbSet<DAL.VendorItemGallery> VendorItemGalleries { get; set; }

        public DbSet<DAL.VendorItemCategory> VendorItemCategories { get; set; }

        public DbSet<DAL.Offers> Offers { get; set; }

        public DbSet<DAL.OffersCategory> OffersCategories { get; set; }

        public DbSet<DAL.OffersGallery> OffersGalleries { get; set; }

        public DbSet<DAL.Users> Users { get; set; }

        public DbSet<DAL.Events> Events { get; set; }

        public DbSet<DAL.CheckList> CheckLists { get; set; }

        public DbSet<DAL.GuestList> GuestLists { get; set; }

        public DbSet<DAL.GuestListTables> GuestListTables { get; set; }

        public DbSet<DAL.Budget> Budgets { get; set; }

        public DbSet<DAL.BudgetCategory> BudgetCategories { get; set; }

        public DbSet<DAL.WishList> WishLists { get; set; }

        public DbSet<DAL.BasketItems> BasketItems { get; set; }

        public DbSet<DAL.Order> Orders { get; set; }
        public DbSet<DAL.VendorType> VendorTypes { get; set; }
        public DbSet<DAL.VendorTypeValue> VendorTypeValues { get; set; }
        public DbSet<DAL.VendorItemTypeValue> VendorItemTypeValues { get; set; }
    }
}
