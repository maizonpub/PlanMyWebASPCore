﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class WishList
    {
        public int Id { get; set; }
        public VendorItem VendorItem { get; set; }
        public int VendorItemId { get; set; }
        public Users User { get; set; }
        public string UserId { get; set; }
    }
}
