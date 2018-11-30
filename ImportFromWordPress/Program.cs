using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ImportFromWordPress
{
    class Program
    {
        static UserManager<Users> _userManager;
        static void Main(string[] args)
        {
            LoadData().Wait();
        }
        static async Task LoadData()
        {
            WordpressService service = new WordpressService();
            
            try
            {
                using (var context = new consoleDbContext())
                {
                    var store = new CustomUserStore();
                    var passwordhasher = new PasswordHasher<Users>();
                    
                    var userValidators = new List<UserValidator<Users>>().AsEnumerable();
                    var passwordValidators = new List<PasswordValidator<Users>>().AsEnumerable();
                    _userManager = new UserManager<Users>(store, null, passwordhasher, userValidators, passwordValidators, null, null, null, null);
                    var categories = context.VendorCategories;
                    foreach (var category in categories)
                    {
                        Console.WriteLine("Fetching Items from category " + category.Title);
                        var dbcat = await service.GetItemCategoryAsync(category.Title);

                        var items = await service.GetItemsByFilterAsync(dbcat.Id, null, null, null, null, null, null, null, null, null, null, null, null, null);
                        foreach (var item in items)
                        {
                            WebClient wc = new WebClient();
                            string filename = "";
                            string galleryfile = "";
                            if (item.Embedded.WpFeaturedmedia != null && item.Embedded.WpFeaturedmedia.Count() > 0 && !string.IsNullOrEmpty(item.Embedded.WpFeaturedmedia.ToList()[0].SourceUrl))
                            {
                                filename = Guid.NewGuid() + ".jpg";
                                galleryfile = Guid.NewGuid() + ".jpg";
                                wc.DownloadFile(item.Embedded.WpFeaturedmedia.ToList()[0].MediaDetails.Sizes.FirstOrDefault().Value.SourceUrl, @"D:\Work\Web\PlanMy\PlanMyWeb\wwwroot\Media\" + filename);
                                wc.DownloadFile(item.Embedded.WpFeaturedmedia.ToList()[0].SourceUrl, @"D:\Work\Web\PlanMy\PlanMyWeb\wwwroot\Media\" + galleryfile);
                            }
                            string locator = item.ItemMeta.locators[0];
                            var gallery = await service.GetItemMedia(item.Id);
                            
                            //var position = await Geolocator.CrossGeolocator.Current.GetPositionsForAddressAsync(locator);
                            var dbuser = await service.GetAuthorByIDAsync(item.Author);
                            string desc = System.Net.WebUtility.HtmlDecode(item.Content.Rendered);
                            var user = new Users { Address = item.ItemMeta.item_address[0], UserType = UserType.Vendor, FirstName = !string.IsNullOrEmpty(dbuser.FirstName)?dbuser.FirstName:dbuser.Name, LastName = !string.IsNullOrEmpty(dbuser.LastName) ? dbuser.LastName : dbuser.Name, PasswordHash = !string.IsNullOrEmpty(dbuser.Password) ? dbuser.Password : "123456", Gender = Gender.Male, UserName = dbuser.UserName, PhoneNumber = (item.ItemMeta.item_phone!=null && item.ItemMeta.item_phone.Count()>0) ?item.ItemMeta.item_phone[0]:"", Email = !string.IsNullOrEmpty(dbuser.Email)?dbuser.Email:"" };
                            VendorItem di = new VendorItem { Address = (item.ItemMeta.item_address!=null && item.ItemMeta.item_address.Count()>0) ?item.ItemMeta.item_address[0]:"", IsFeatured = false, Location = locator, HtmlDescription = desc, PhoneNumber = (item.ItemMeta.item_phone != null && item.ItemMeta.item_phone.Count() > 0) ? item.ItemMeta.item_phone[0] : "", Thumb = filename, Title = item.Title.Rendered, User = user, Email = dbuser.Email };
                            VendorItemCategory catrel = new VendorItemCategory { VendorCategory = category, VendorItem = di };
                            foreach (var img in gallery)
                            {
                                string file = Guid.NewGuid() + ".jpg";
                                wc.DownloadFile(img.SourceUrl, @"D:\Work\Web\PlanMy\PlanMyWeb\wwwroot\Media\" + file);
                                context.VendorItemGalleries.Add(new VendorItemGallery { Image = file, Item = di });
                            }
                            passwordhasher.HashPassword(user, !string.IsNullOrEmpty(dbuser.Password) ? dbuser.Password : "123456");
                            var result = await _userManager.CreateAsync(user, !string.IsNullOrEmpty(dbuser.Password) ? dbuser.Password : "123456");
                            context.VendorItems.Add(di);
                            context.VendorItemGalleries.Add(new VendorItemGallery { Image = galleryfile, Item = di });
                            context.VendorItemCategories.Add(catrel);
                            

                        }
                        Console.WriteLine("Saving Items for category " + category.Title);
                    }
                    Console.WriteLine("Saving Changes");
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
    
    public class CustomUserStore : IUserStore<Users>, IUserPasswordStore<Users>, IUserSecurityStampStore<Users>
    {
        UserStore<Users> userStore = new UserStore<Users>(new consoleDbContext());
        public Task<IdentityResult> CreateAsync(Users user, CancellationToken cancellationToken)
        {
            
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            
        }

        public Task<Users> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var context = userStore.Context as consoleDbContext;
            var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();
            return Task.FromResult<Users>(user);
        }

        public Task<Users> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var context = userStore.Context as consoleDbContext;
            var user = context.Users.Where(x => x.NormalizedUserName == normalizedUserName).FirstOrDefault();
            return Task.FromResult<Users>(user);
        }

        public Task<string> GetNormalizedUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetSecurityStampAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<string> GetUserIdAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task SetNormalizedUserNameAsync(Users user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(Users user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetSecurityStampAsync(Users user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(Users user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public Task<IdentityResult> UpdateAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}

