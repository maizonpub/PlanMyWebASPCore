using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class BlogsViewModel
    {
        public SelectList Categories { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string HtmlDescription { get; set; }
        public DateTime PostDate { get; set; }
        public List<BlogCategoryRelation> BlogCategoryRelations { get; set; }
        public MediaType MediaType { get; set; }
        public IEnumerable<TopicGallery> topicGalleries { get; set; }   
        public List<BlogGallery> Gallery { get; set; }
    }
}
