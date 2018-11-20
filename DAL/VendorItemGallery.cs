using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class VendorItemGallery
    {
        public int Id { get; set; }
        public VendorItem Item { get; set; }
        public byte[] Image { get; set; }
    }
}
