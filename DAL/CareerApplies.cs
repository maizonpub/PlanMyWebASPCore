using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class CareerApplies
    {
        public int Id { get; set; }
        public Career Career { get; set; }
        public string CV { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
