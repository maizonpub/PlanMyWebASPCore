﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class VendorCategory
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public List<VendorItemCategory> Categories { get; set; }
    }
}
