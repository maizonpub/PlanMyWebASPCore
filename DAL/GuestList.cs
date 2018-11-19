using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class GuestList
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public GuestStatus GuestStatus { get; set; }
        public Side Side { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public GuestListTables GuestListTables { get; set; }


    }
    public enum  GuestStatus
        {
        
        }
    public enum Side
    { }
}
