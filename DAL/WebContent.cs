﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class WebContent
    {
        public int Id { get; set; }
        public string TipsTitle { get; set; }
        public string FeaturedVendorsTitle { get; set; }
        public int BlogId { get; set; }
        public string ContactAddress { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AdminEmail { get; set; }
        public string AdminEmailPassword { get; set; }
    }
}
