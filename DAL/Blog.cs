using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public byte[] Image { get; set; }
        public string HtmlDescription { get; set; }
        public DateTime PostDate { get; set; }
    }
}
