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
                var context = new consoleDbContext();
                
                    var store = new CustomUserStore();
                    var passwordhasher = new PasswordHasher<Users>();
                    
                    var userValidators = new List<UserValidator<Users>>().AsEnumerable();
                    var passwordValidators = new List<PasswordValidator<Users>>().AsEnumerable();
                    _userManager = new UserManager<Users>(store, null, passwordhasher, userValidators, passwordValidators, null, null, null, null);
                    var categories = context.VendorCategories;
                    var itemlocations = await service.GetItemLocationsAsync();
                    var itemlocation = new VendorType { Title = "Location" }; 
                    var types = await service.GetItemTypesAsync();
                    var cities = await service.GetItemCitiesAsync();
                    var settings = await service.GetItemSettingsAsync();
                    var capacities = await service.GetCapacitiesAsync();
                    var cateringservices = await service.GetItemCateringServicesAsync();
                    var typeoffurnitures = await service.GetItemTypeOfFurnituresAsync();
                    var itemclienteles = await service.GetItemClientelesAsync();
                    var itemclothings = await service.GetItemClothingsAsync();
                    var itembeautyservices = await service.GetItemBeautyServicesAsync();
                    var itemtypeofmusicians = await service.GetItemTypeOfMusiciansAsync();
                    var honeymoonexperiences = await service.GetHoneymoonExperiencesAsync();
                    var typeofservices = await service.GetTypeOfServicesAsync();
                    var type = new VendorType { Title = "Type of venue" };
                    var city = new VendorType { Title = "City" };
                    var setting = new VendorType { Title = "Setting" };
                    var capacity = new VendorType { Title = "Capacity" };
                    var cateringservice = new VendorType { Title = "Catering Services" };
                    var typeoffurniture = new VendorType { Title = "Type of furniture"};
                    var itemclientele = new VendorType { Title = "Clientele" };
                    var itemclothing = new VendorType { Title = "Clothing" };
                    var itembeautyservice = new VendorType { Title = "Beauty Services" };
                    var itemtypeofmusician = new VendorType { Title = "Type of Entertainment" };
                    var honeymoonexperience = new VendorType { Title = "Honeymoon Experience"};
                    var typeofservice = new VendorType { Title = "Type of Service" }; 
                    
                    
                    foreach (var category in categories)
                    {
                        Console.WriteLine("Fetching Items from category " + category.Title);
                        var dbcat = await service.GetItemCategoryAsync(category.Title);
                        switch (dbcat.Id)
                        {
                            case 3:
                                type.VendorCategory = category;
                                city.VendorCategory = category;
                                setting.VendorCategory = category;
                                capacity.VendorCategory = category;
                                
                                break;
                            case 44:                                
                                cateringservice.VendorCategory = category;
                                break;
                            case 60:                                
                               typeoffurniture.VendorCategory = category;
                                break;
                            case 46:                                
                                itemclientele.VendorCategory = category;
                                itemclothing.VendorCategory = category;
                                break;
                            case 43:                                
                                itembeautyservice.VendorCategory = category;
                                break;
                            case 59:                                
                                itemtypeofmusician.VendorCategory = category;
                                break;
                            case 54:                                
                                honeymoonexperience.VendorCategory = category;
                                break;
                            case 51:                                
                                typeofservice.VendorCategory = category;
                                break;
                        }
                        if (context.VendorTypes.Where(x => x.Title == city.Title).Count() == 0)
                            context.VendorTypes.Add(city);
                        else
                            city = context.VendorTypes.Where(x => x.Title == city.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == setting.Title).Count() == 0)
                            context.VendorTypes.Add(setting);
                        else
                            setting = context.VendorTypes.Where(x => x.Title == setting.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == capacity.Title).Count() == 0)
                            context.VendorTypes.Add(capacity);
                        else
                            capacity = context.VendorTypes.Where(x => x.Title == capacity.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == cateringservice.Title).Count() == 0)
                            context.VendorTypes.Add(cateringservice);
                        else
                            cateringservice = context.VendorTypes.Where(x => x.Title == cateringservice.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == typeoffurniture.Title).Count() == 0)
                            context.VendorTypes.Add(typeoffurniture);
                        else
                            typeoffurniture = context.VendorTypes.Where(x => x.Title == typeoffurniture.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == itemclientele.Title).Count() == 0)
                            context.VendorTypes.Add(itemclientele);
                        else
                            itemclientele = context.VendorTypes.Where(x => x.Title == itemclientele.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == itemclothing.Title).Count() == 0)
                            context.VendorTypes.Add(itemclothing);
                        else
                            itemclothing = context.VendorTypes.Where(x => x.Title == itemclothing.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == itembeautyservice.Title).Count() == 0)
                            context.VendorTypes.Add(itembeautyservice);
                        else
                            setting = context.VendorTypes.Where(x => x.Title == setting.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == itemtypeofmusician.Title).Count() == 0)
                            context.VendorTypes.Add(itemtypeofmusician);
                        else
                            itemtypeofmusician = context.VendorTypes.Where(x => x.Title == itemtypeofmusician.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == honeymoonexperience.Title).Count() == 0)
                            context.VendorTypes.Add(honeymoonexperience);
                        else
                            honeymoonexperience = context.VendorTypes.Where(x => x.Title == honeymoonexperience.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == typeofservice.Title).Count() == 0)
                            context.VendorTypes.Add(typeofservice);
                        else
                            typeofservice = context.VendorTypes.Where(x => x.Title == typeofservice.Title).FirstOrDefault();
                        if (context.VendorTypes.Where(x => x.Title == itemlocation.Title).Count() == 0)
                            context.VendorTypes.Add(itemlocation);
                        else
                            itemlocation = context.VendorTypes.Where(x => x.Title == itemlocation.Title).FirstOrDefault();
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
                        foreach (var numb in item.itemtype)
                        {
                            var r = types.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = type };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                            }
                        }
                        foreach (var numb in item.itemcity)
                        {
                            var r = cities.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null && city.VendorTypeValues!=null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = city };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }
                        foreach (var numb in item.itemsetting)
                        {
                            var r = settings.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = setting };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }
                        foreach (var numb in item.itemlocation)
                        {
                            var r = itemlocations.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = itemlocation };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                        foreach (var numb in item.itemcateringservices)
                        {
                            var r = cateringservices.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = cateringservice };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                        foreach (var numb in item.itemtypeoffurniture)
                        {
                            var r = typeoffurnitures.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = typeoffurniture };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                        foreach (var numb in item.itemclientele)
                        {
                            var r = itemclienteles.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = itemclientele };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                        foreach (var numb in item.itemclothing)
                        {
                            var r = itemclothings.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = itemclothing };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                        foreach (var numb in item.itembeautyservices)
                        {
                            var r = itembeautyservices.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = itembeautyservice };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                        foreach (var numb in item.itemtypeofmusicians)
                        {
                            var r = itemtypeofmusicians.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = itemtypeofmusician };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                        foreach (var numb in item.honeymoonexperience)
                        {
                            var r = honeymoonexperiences.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = honeymoonexperience };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                        foreach (var numb in item.typeofservice)
                        {
                            var r = typeofservices.Where(x => x.Id == numb).FirstOrDefault();
                            if (r != null)
                            {
                                
                                    var v = new VendorTypeValue { Title = r.Name, VendorType = typeofservice };
                                    var res = context.VendorTypeValues.Where(x => x.Title == v.Title);
                                    if (res.FirstOrDefault() == null)
                                        context.VendorTypeValues.Add(v);
                                    else
                                        v = res.FirstOrDefault();
                                    context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorItem = di, VendorTypeValueId = v.Id });
                                
                            }
                        }

                    }
                        Console.WriteLine("Saving Items for category " + category.Title);
                }

                context.SaveChanges(true);
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

