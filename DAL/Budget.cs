using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Budget
    {
        public int Id { get; set; }
        public BudgetCategory BudgetCategory { get; set; }
        public string Description { get; set; }
        public string EstimatedCost { get; set; }
        public string ActualCost { get; set; }
        public string PaidCost { get; set; }
        public string Notes { get; set; }
    }
}
