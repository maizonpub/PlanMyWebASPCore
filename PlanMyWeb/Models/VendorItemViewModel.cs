using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class VendorItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Location { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        public SelectList User { get; set; }
        [Display(Name = "Description")]
        public string HtmlDescription { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsFeatured { get; set; }
        public IFormFile Thumb { get; set; }
        public Country Country { get; set; }
        public List<VendorItemGallery> Gallery { get; set; }
        public IEnumerable<VendorItemCategory> ItemCategories { get; set; }
        public MediaType MediaType { get; set; }
        public SelectList Categories { get; set; }
        public IEnumerable<VendorType> Taxonomies { get; set; }
        public List<VendorItemTypeValue> values { get; set; }
    }
}
