﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(DbWebContext))]
    [Migration("20181126111202_fixdb")]
    partial class fixdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.BasketItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OffersId");

                    b.Property<string>("Quantity");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("OffersId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("DAL.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HtmlDescription");

                    b.Property<byte[]>("Image");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("DAL.BlogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("BlogCategories");
                });

            modelBuilder.Entity("DAL.BlogCategoryRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BlogId");

                    b.Property<int?>("CategoryId");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BlogCategoryRelations");
                });

            modelBuilder.Entity("DAL.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualCost");

                    b.Property<int?>("BudgetCategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("EstimatedCost");

                    b.Property<string>("Notes");

                    b.Property<string>("PaidCost");

                    b.HasKey("Id");

                    b.HasIndex("BudgetCategoryId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("DAL.BudgetCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("BudgetCategories");
                });

            modelBuilder.Entity("DAL.CheckList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("Timing");

                    b.Property<string>("Title");

                    b.Property<int?>("VendorCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("VendorCategoryId");

                    b.ToTable("CheckLists");
                });

            modelBuilder.Entity("DAL.Events", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DAL.GuestList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<int?>("GuestListTablesId");

                    b.Property<int>("GuestStatus");

                    b.Property<string>("Phone");

                    b.Property<int>("Side");

                    b.HasKey("Id");

                    b.HasIndex("GuestListTablesId");

                    b.ToTable("GuestLists");
                });

            modelBuilder.Entity("DAL.GuestListTables", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("GuestListTables");
                });

            modelBuilder.Entity("DAL.HomeSlider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Media");

                    b.Property<int>("MediaType");

                    b.HasKey("Id");

                    b.ToTable("HomeSlider");
                });

            modelBuilder.Entity("DAL.HomeTips", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("HomeTips");
                });

            modelBuilder.Entity("DAL.Offers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<byte[]>("Image");

                    b.Property<int>("OffersType");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("SaleFromDate");

                    b.Property<decimal>("SalePrice");

                    b.Property<DateTime>("SaleToDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.Property<int>("Validity");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("DAL.OffersCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OffersId");

                    b.Property<int?>("VendorCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("OffersId");

                    b.HasIndex("VendorCategoryId");

                    b.ToTable("OffersCategories");
                });

            modelBuilder.Entity("DAL.OffersGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image");

                    b.Property<int?>("OffersId");

                    b.HasKey("Id");

                    b.HasIndex("OffersId");

                    b.ToTable("OffersGalleries");
                });

            modelBuilder.Entity("DAL.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ReferenceNumber");

                    b.Property<string>("Status");

                    b.Property<decimal>("Total");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<string>("TransactionUUID");

                    b.Property<int?>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DAL.Pages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HtmlText");

                    b.Property<string>("title");

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("DAL.SocialMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image");

                    b.Property<string>("Link");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("DAL.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("Age");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.Property<int>("UserType");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.VendorCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("VendorCategories");
                });

            modelBuilder.Entity("DAL.VendorItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("HtmlDescription");

                    b.Property<bool>("IsFeatured");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Thumb");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("VendorItems");
                });

            modelBuilder.Entity("DAL.VendorItemCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("VendorCategoryId");

                    b.Property<int?>("VendorItemId");

                    b.HasKey("Id");

                    b.HasIndex("VendorCategoryId");

                    b.HasIndex("VendorItemId");

                    b.ToTable("VendorItemCategories");
                });

            modelBuilder.Entity("DAL.VendorItemGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image");

                    b.Property<int?>("ItemId");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("VendorItemGalleries");
                });

            modelBuilder.Entity("DAL.WebContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminEmail");

                    b.Property<string>("AdminEmailPassword");

                    b.Property<string>("BlogTitle");

                    b.Property<string>("ContactAddress");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactPhone");

                    b.Property<string>("FeaturedVendorsTitle");

                    b.Property<string>("TipsTitle");

                    b.HasKey("Id");

                    b.ToTable("WebContents");
                });

            modelBuilder.Entity("DAL.WishList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("VendorItemId");

                    b.HasKey("Id");

                    b.HasIndex("VendorItemId");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("DAL.BasketItems", b =>
                {
                    b.HasOne("DAL.Offers", "Offers")
                        .WithMany()
                        .HasForeignKey("OffersId");
                });

            modelBuilder.Entity("DAL.BlogCategoryRelation", b =>
                {
                    b.HasOne("DAL.Blog", "Blog")
                        .WithMany()
                        .HasForeignKey("BlogId");

                    b.HasOne("DAL.BlogCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("DAL.Budget", b =>
                {
                    b.HasOne("DAL.BudgetCategory", "BudgetCategory")
                        .WithMany()
                        .HasForeignKey("BudgetCategoryId");
                });

            modelBuilder.Entity("DAL.CheckList", b =>
                {
                    b.HasOne("DAL.VendorCategory", "VendorCategory")
                        .WithMany()
                        .HasForeignKey("VendorCategoryId");
                });

            modelBuilder.Entity("DAL.GuestList", b =>
                {
                    b.HasOne("DAL.GuestListTables", "GuestListTables")
                        .WithMany()
                        .HasForeignKey("GuestListTablesId");
                });

            modelBuilder.Entity("DAL.OffersCategory", b =>
                {
                    b.HasOne("DAL.Offers", "Offers")
                        .WithMany()
                        .HasForeignKey("OffersId");

                    b.HasOne("DAL.VendorCategory", "VendorCategory")
                        .WithMany()
                        .HasForeignKey("VendorCategoryId");
                });

            modelBuilder.Entity("DAL.OffersGallery", b =>
                {
                    b.HasOne("DAL.Offers", "Offers")
                        .WithMany()
                        .HasForeignKey("OffersId");
                });

            modelBuilder.Entity("DAL.Order", b =>
                {
                    b.HasOne("DAL.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("DAL.VendorItemCategory", b =>
                {
                    b.HasOne("DAL.VendorCategory", "VendorCategory")
                        .WithMany()
                        .HasForeignKey("VendorCategoryId");

                    b.HasOne("DAL.VendorItem", "VendorItem")
                        .WithMany("Categories")
                        .HasForeignKey("VendorItemId");
                });

            modelBuilder.Entity("DAL.VendorItemGallery", b =>
                {
                    b.HasOne("DAL.VendorItem", "Item")
                        .WithMany("Gallery")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("DAL.WishList", b =>
                {
                    b.HasOne("DAL.VendorItem", "VendorItem")
                        .WithMany()
                        .HasForeignKey("VendorItemId");
                });
#pragma warning restore 612, 618
        }
    }
}
