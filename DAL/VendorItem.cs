using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class VendorItem
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public string HtmlDescription { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsFeatured { get; set; }
        List<VendorItemGallery> Gallery { get; set; }
    }
}
