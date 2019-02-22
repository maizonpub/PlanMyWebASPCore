using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class GuestListTables
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Users User { get; set; }
        public string UserId { get; set; }
        public string TableName { get; set; }
        public TableType TableType { get; set; }
        public string SeatsNumber { get; set; }
    }

    public enum TableType
    {
        Circle,
        Rectangle
    }
}
