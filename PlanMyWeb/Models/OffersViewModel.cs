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
    public class OffersViewModel
    {
        public Offers Offers { get; set; }
        public Offers NextPage { get; set; }
        public Offers PreviousPage { get; set; }
    }
    public class AdminOffersViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Offer type")]
        public OffersType OffersType { get; set; }
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public SelectList User { get; set; }
        public string Description { get; set; }
        public Validity Validity { get; set; }
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        public decimal? Price { get; set; }
        [Display(Name = "Sale Price")]
        public decimal? SalePrice { get; set; }
        [Display(Name = "Sale Begining date")]
        public DateTime? SaleFromDate { get; set; }
        [Display(Name = "Sale end date")]
        public DateTime? SaleToDate { get; set; }
        public SelectList Categories { get; set; }
    }
}
