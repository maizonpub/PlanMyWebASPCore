using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ImportFromWordPress
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadData().Wait();
        }
        static async Task LoadData()
        {
            WordpressService service = new WordpressService();

            using (var context = new consoleDbContext())
            {

                var categories = context.VendorCategories;
                foreach (var category in categories)
                {
                    try
                    {
                        var dbcat = await service.GetItemCategoryAsync(category.Title);
                    
                    var items = await service.GetItemsByFilterAsync(dbcat.Id, null, null, null, null, null, null, null, null, null, null, null, null, null);
                    foreach (var item in items)
                    {
                        WebClient wc = new WebClient();
                        string filename = Guid.NewGuid() + ".jpg";
                        wc.DownloadFile(item.Embedded.WpFeaturedmedia.ToList()[0].SourceUrl, @"D:\Work\Web\PlanMy\PlanMyWeb\wwwroot\Media\" + filename);
                        string locator = item.ItemMeta.locators[0];
                            Velyo.Google.Services.MapsApiContext geocontext = new Velyo.Google.Services.MapsApiContext();
                            var resp = Velyo.Google.Services.MapsApi.Geocode(geocontext, locator);
                            
                        //var position = await Geolocator.CrossGeolocator.Current.GetPositionsForAddressAsync(locator);
                        var latitude = resp.FirstOrDefault().Geometry.Location.Latitude;
                        var longitude = resp.FirstOrDefault().Geometry.Location.Longitude;
                        var dbuser = await service.GetAuthorByIDAsync(item.Author);
                        var user = new Users { Address = item.ItemMeta.item_address[0], UserType = UserType.Vendor, FirstName = dbuser.FirstName, LastName = dbuser.LastName, PasswordHash = dbuser.Password, Gender = Gender.Male, UserName = dbuser.UserName, PhoneNumber = item.ItemMeta.item_phone[0] };
                        VendorItem di = new VendorItem { Address = item.ItemMeta.item_address[0], IsFeatured = false, Latitude = latitude, Longitude = longitude, HtmlDescription = item.Content.Rendered, PhoneNumber = item.ItemMeta.item_phone[0], Thumb = filename, Title = item.Title.Rendered, UserId = dbuser.Id, Email = dbuser.Email };
                        VendorItemCategory catrel = new VendorItemCategory { VendorCategory = category, VendorItem = di };
                        context.Users.Add(user);
                        context.VendorItems.Add(di);
                        context.VendorItemCategories.Add(catrel);
                        await context.SaveChangesAsync();

                    }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
