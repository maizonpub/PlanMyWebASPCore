using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class VendorItemGallery
    {
        public int Id { get; set; }
        public VendorItem Item { get; set; }
        public byte[] Image { get; set; }
    }
}
