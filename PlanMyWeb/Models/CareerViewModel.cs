using DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class CareerViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile WideImage { get; set; }
        public string HtmlDescription { get; set; }
        public JobStatus JobStatus { get; set; }
        public Pages Pages { get; set; }
        public IFormFile Thumb { get; set; }

    }
}
