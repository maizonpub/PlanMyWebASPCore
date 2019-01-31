using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class UserViewModel
    {
        public Users User { get; set; }
    }
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Age { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public UserType UserType { get; set; }
        public List<Offers> Offers { get; set; }
        public List<VendorItem> VendorItems { get; set; }
        public List<WishList> WishList { get; set; }
        public List<UserPaymentToken> UserPaymentTokens { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public IFormFile Image { get; set; }
        public string ImageString { get; set; }
    }
}
