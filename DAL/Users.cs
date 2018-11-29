using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Users: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Offers> Offers { get; set; }
        public List<VendorItem> VendorItems { get; set; }
    }
        public enum Gender
    {
        Male,
        Female
    }
    public enum UserType
    {
        Vendor,
        Planner,
        Admin
    }

}
