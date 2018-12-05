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
using WordPressPCL.Models;

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
                    var itemlocations = await service.GetItemLocationsAsync();
                    var itemlocation = context.VendorTypes.Add(new VendorType { Title = "Location", CategoryId = null });
                    foreach (var r in itemlocations)
                    {
                        context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = itemlocation.Entity.Id });
                    }
                    foreach (var category in categories)
                    {
                        Console.WriteLine("Fetching Items from category " + category.Title);
                        var dbcat = await service.GetItemCategoryAsync(category.Title);
                        switch (dbcat.Id)
                        {

                            case 3:
                                var types = await service.GetItemTypesAsync();
                                var cities = await service.GetItemCitiesAsync();
                                var settings = await service.GetItemSettingsAsync();
                                var capacities = await service.GetCapacitiesAsync();
                                
                                var type = context.VendorTypes.Add(new VendorType { Title = "Type of venue", CategoryId = category.Id });
                                var city = context.VendorTypes.Add(new VendorType { Title = "City", CategoryId = category.Id });
                                var setting = context.VendorTypes.Add(new VendorType { Title = "Setting", CategoryId = category.Id });
                                var capacity = context.VendorTypes.Add(new VendorType { Title = "Capacity", CategoryId = category.Id });
                                foreach(var r in types)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = type.Entity.Id });
                                }
                                foreach (var r in cities)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = city.Entity.Id });
                                }
                                foreach (var r in settings)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = setting.Entity.Id });
                                }
                                foreach (var r in capacities)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = capacity.Entity.Id });
                                }
                                break;
                            case 44:
                                var cateringservices = await service.GetItemCateringServicesAsync();
                                var cateringservice = context.VendorTypes.Add(new VendorType { Title = "Catering Services", CategoryId = category.Id });
                                foreach (var r in cateringservices)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = cateringservice.Entity.Id });
                                }
                                break;
                            case 60:
                                var typeoffurnitures = await service.GetItemTypeOfFurnituresAsync();
                                var typeoffurniture = context.VendorTypes.Add(new VendorType { Title = "Type of furniture", CategoryId = category.Id });
                                foreach (var r in typeoffurnitures)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = typeoffurniture.Entity.Id });
                                }
                                break;
                            case 46:
                                var itemclienteles = await service.GetItemClientelesAsync();
                                var itemclothings = await service.GetItemClothingsAsync();
                                var itemclientele = context.VendorTypes.Add(new VendorType { Title = "Clientele", CategoryId = category.Id });
                                foreach (var r in itemclienteles)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = itemclientele.Entity.Id });
                                }
                                var itemclothing = context.VendorTypes.Add(new VendorType { Title = "Clothing", CategoryId = category.Id });
                                foreach (var r in itemclothings)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = itemclothing.Entity.Id });
                                }
                                break;
                            case 43:
                                var itembeautyservices = await service.GetItemBeautyServicesAsync();
                                var itembeautyservice = context.VendorTypes.Add(new VendorType { Title = "Beauty Services", CategoryId = category.Id });
                                foreach (var r in itembeautyservices)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = itembeautyservice.Entity.Id });
                                }
                                break;
                            case 59:
                                var itemtypeofmusicians = await service.GetItemTypeOfMusiciansAsync();
                                var itemtypeofmusician = context.VendorTypes.Add(new VendorType { Title = "Type of Entertainment", CategoryId = category.Id });
                                foreach (var r in itemtypeofmusicians)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = itemtypeofmusician.Entity.Id });
                                }
                                break;
                            case 54:
                                var honeymoonexperiences = await service.GetHoneymoonExperiencesAsync();
                                var honeymoonexperience = context.VendorTypes.Add(new VendorType { Title = "Honeymoon Experience", CategoryId = category.Id });
                                foreach (var r in honeymoonexperiences)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = honeymoonexperience.Entity.Id });
                                }
                                break;
                            case 51:
                                var typeofservices = await service.GetTypeOfServicesAsync();
                                var typeofservice = context.VendorTypes.Add(new VendorType { Title = "Type of Service", CategoryId = category.Id });
                                foreach (var r in typeofservices)
                                {
                                    context.VendorTypeValues.Add(new VendorTypeValue { Title = r.Name, VendorTypeId = typeofservice.Entity.Id });
                                }
                                break;
                        }

                        var items = await service.GetItemsByFilterAsync(dbcat.Id, null, null, null, null, null, null, null, null, null, null, null, null, null);
                        foreach (var item in items)
                        {
                            WebClient wc = new WebClient();
                            string filename = "";
                            string galleryfile = "";
                            if (item.Embedded.WpFeaturedmedia != null && item.Embedded.WpFeaturedmedia.Count() > 0 && !string.IsNullOrEmpty(item.Embedded.WpFeaturedmedia.ToList()[0].SourceUrl))
                            {
                                filename = System.Guid.NewGuid() + ".jpg";
                                galleryfile = System.Guid.NewGuid() + ".jpg";
                                wc.DownloadFile(item.Embedded.WpFeaturedmedia.ToList()[0].MediaDetails.Sizes.FirstOrDefault().Value.SourceUrl, @"D:\Work\Web\PlanMy\PlanMyWeb\wwwroot\Media\" + filename);
                                wc.DownloadFile(item.Embedded.WpFeaturedmedia.ToList()[0].SourceUrl, @"D:\Work\Web\PlanMy\PlanMyWeb\wwwroot\Media\" + galleryfile);
                            }
                            string locator = item.ItemMeta.locators[0];
                            var gallery = await service.GetItemMedia(item.Id);

                            //var position = await Geolocator.CrossGeolocator.Current.GetPositionsForAddressAsync(locator);
                            var dbuser = await service.GetAuthorByIDAsync(item.Author);
                            string desc = System.Net.WebUtility.HtmlDecode(item.Content.Rendered);
                            var user = new Users { Address = item.ItemMeta.item_address[0], UserType = UserType.Vendor, FirstName = !string.IsNullOrEmpty(dbuser.FirstName) ? dbuser.FirstName : dbuser.Name, LastName = !string.IsNullOrEmpty(dbuser.LastName) ? dbuser.LastName : dbuser.Name, PasswordHash = !string.IsNullOrEmpty(dbuser.Password) ? dbuser.Password : "123456", Gender = Gender.Male, UserName = dbuser.UserName, PhoneNumber = (item.ItemMeta.item_phone != null && item.ItemMeta.item_phone.Count() > 0) ? item.ItemMeta.item_phone[0] : "", Email = !string.IsNullOrEmpty(dbuser.Email) ? dbuser.Email : "" };
                            VendorItem di = new VendorItem { Address = (item.ItemMeta.item_address != null && item.ItemMeta.item_address.Count() > 0) ? item.ItemMeta.item_address[0] : "", IsFeatured = false, Location = locator, HtmlDescription = desc, PhoneNumber = (item.ItemMeta.item_phone != null && item.ItemMeta.item_phone.Count() > 0) ? item.ItemMeta.item_phone[0] : "", Thumb = filename, Title = item.Title.Rendered, User = user, Email = dbuser.Email };
                            VendorItemCategory catrel = new VendorItemCategory { VendorCategory = category, VendorItem = di };
                            foreach (var img in gallery)
                            {
                                string file = System.Guid.NewGuid() + ".jpg";
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
        
        protected static int[] AddSwitches(IEnumerable<ItemCategory> types)
        {
            List<int> par = new List<int>();
            try
            {
                foreach (var row in types)
                {

                    if (!par.Contains(row.Id))
                        par.Add(row.Id);
                }
            }
            catch (Exception ex)
            {

            }
            return par.ToArray();
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

