﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Offers
    {
        public int Id { get; set; }
        public OffersType OffersType { get;set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public Users User { get; set; }
        public string Description { get; set; }
        public Validity Validity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTime SaleFromDate { get; set; }
        public DateTime SaleToDate { get; set; }
    }
    public enum OffersType
    {
        HotDeals,
        Bundles
    }

    public enum Validity
    {
        Valid,
        NotValid
    }
    public class OffersViewModel
    {
        public int Id { get; set; }
        public OffersType OffersType { get; set; }
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public Users User { get; set; }
        public string Description { get; set; }
        public Validity Validity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTime SaleFromDate { get; set; }
        public DateTime SaleToDate { get; set; }
    }
}
