using DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class CareerAppliesViewModel
    {
        public int Id { get; set; }
        public Career Career { get; set; }
        public IFormFile CV { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

