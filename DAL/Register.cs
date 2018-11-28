using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Register
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Checklist { get; set; }
        public string Budget { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
    }
}
