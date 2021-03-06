﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class CheckList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Timing { get; set; }
        public VendorCategory VendorCategory { get; set; }
        public int? VendorCategoryId { get; set; }
        public string Description { get; set; }
        public Users User { get; set; }
        public string UserId { get; set; }
        public CheckListStatus Status { get; set; }
        public bool IsPriority { get; set; } = false;
    }
    public enum CheckListStatus
    {
        Done,
        ToDo
    }
}
