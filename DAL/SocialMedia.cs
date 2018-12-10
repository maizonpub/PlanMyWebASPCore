﻿using Microsoft.AspNetCore.Http;
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
        public string Image { get; set; }
        public MediaType MediaType { get; set; }
    }
    public class SocialMediaViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public IFormFile Image { get; set; }
        public MediaType MediaType { get; set; }
    }

}
