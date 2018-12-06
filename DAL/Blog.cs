﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public string Image { get; set; }
        public string HtmlDescription { get; set; }
        public DateTime PostDate { get; set; }
        public List<BlogCategoryRelation> BlogCategoryRelations { get; set; }
    }
   public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string HtmlDescription { get; set; }
        public DateTime PostDate { get; set; }
        public List<BlogCategoryRelation> BlogCategoryRelations { get; set; }
        public MediaType MediaType { get; set; }
    }
}
