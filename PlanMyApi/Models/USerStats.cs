using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyApi.Models
{
    public class UserStats
    {
        public int guestsCount { get; set; }
        public int todosCount { get; set; }
        public int wishesCount { get; set; }
    }
}
