using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;

namespace PlanMyWeb.Models
{
    public class PaymentViewModel
    {
        public Order Order { get; set; }
        public PaymentSetting PaymentSetting { get; set; }
        public IEnumerable<BasketItems> BasketItems { get; set; }
    }
}
