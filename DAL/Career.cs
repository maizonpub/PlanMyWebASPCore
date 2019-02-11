using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Career
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Thumb { get; set; }
        public string WideImage { get; set; }
        public string HtmlDescription { get; set; }
        public JobStatus JobStatus{get;set;}
        public Pages Pages { get; set; }
        
    }
        public enum JobStatus 
    {
        Available,
        NotAvailable
    }
}
