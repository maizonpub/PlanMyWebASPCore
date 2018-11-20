using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public byte[] Image { get; set; }
    }
}
