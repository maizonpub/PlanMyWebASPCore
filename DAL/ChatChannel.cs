﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ChatChannel
    {
        public int Id { get; set; }
        public Users User { get; set; }
        public string UserId { get; set; }
        public Users Vendor { get; set; }
        public string VendorId { get; set; }
        public string ChannelUrl { get; set; }
    }
}
