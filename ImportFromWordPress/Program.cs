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
        static void Main(string[] args)
        {
            Console.WriteLine("Loading...");
            try
            {
                var context = new consoleDbContext();
                var items = context.VendorItems;


                foreach (var item in items)
                {
                    try
                    {
                        Console.WriteLine("Getting item " + item.Title);
                        WebClient wc = new WebClient();
                        string resp = wc.DownloadString("https://planmy.me/maizonpub-api/singleitem.php?itemId=" + item.Title);
                        WordPressItem single = Newtonsoft.Json.JsonConvert.DeserializeObject<WordPressItem>(resp);
                        item.Instagram = single.instagram;
                        item.Facebook = single.facebook;
                        item.Email = single.item_email;
                        item.PhoneNumber = single.item_phone;
                        item.Address = single.item_address;
                    }
                    catch { }
                }
                context.SaveChanges(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Thread.Sleep(10000);
            }
            
        }
    }
    
    public class WordPressItem
    {
        public string item_address { get; set; }

        public string item_phone { get; set; }

        public string item_email { get; set; }
        public string facebook { get; set; }
        public string googleplus { get; set; }

        public string twitter { get; set; }

        public string youtube { get; set; }

        public string linkedin { get; set; }

        public string pinterest { get; set; }

        public string instagram { get; set; }
    }
}

