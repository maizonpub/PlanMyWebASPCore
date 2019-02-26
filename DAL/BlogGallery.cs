using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DAL
{
    public class BlogGallery
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public Blog Blog { get; set; }
        public string UserId { get; set; }
    }

    public class BlogGalleryViewModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public Blog Blog { get; set; }
    }
}
