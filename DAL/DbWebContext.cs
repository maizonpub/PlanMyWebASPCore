using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DbWebContext : IdentityDbContext
    {
        public DbWebContext(DbContextOptions<DbWebContext> options)
            : base(options)
        {
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
        public DbSet<DAL.Country> Country { get; set; }
        public DbSet<DAL.Order> Orders { get; set; }
        public DbSet<DAL.VendorType> VendorTypes { get; set; }
        public DbSet<DAL.VendorTypeValue> VendorTypeValues { get; set; }
        public DbSet<DAL.VendorItemTypeValue> VendorItemTypeValues { get; set; }
        public DbSet<DAL.PaymentSetting> PaymentSettings { get; set; }
        public DbSet<DAL.UserPaymentToken> UserPaymentTokens { get; set; }
        public DbSet<DAL.VendorItemReview> VendorItemReviews { get; set; }
        public DbSet<DAL.UserPushToken> UserPushToken { get; set; }
    }
}
