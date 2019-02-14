using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;

namespace PlanMyWeb.Models
{
    public class TopicGalleryViewModel
    {
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public IFormFile Image { get; set; }
    }
}
