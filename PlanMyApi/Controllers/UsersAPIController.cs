using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Microsoft.AspNetCore.Identity;
using PlanMyApi.Models;

namespace PlanMyApi.Controllers
{
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        DbWebContext _context;
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        public UsersAPIController(DbWebContext context, SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        [Route("api/Login")]
        public async Task<Users> Login(string Username, string Password, bool RememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(Username, Password, RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.GetUserAsync(User);
                return user;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("api/Register")]
        public async Task<IActionResult> Register(string Username, string Email, string Password, string Token, string FirstName, string LastName)
        {
            var user = new Users { UserName = Username, Email = Email, CreationDate = DateTime.Now, FirstName = FirstName, LastName = LastName };
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                user = await _userManager.GetUserAsync(User);
                var token = _context.UserPushToken.Where(x => x.Token == Token).FirstOrDefault();
                if (token != null)
                {
                    token.User = user;
                }
                _context.SaveChanges();
                string Address = !string.IsNullOrEmpty(user.Address) ? user.Address : "";
                string Age = !string.IsNullOrEmpty(user.Age) ? user.Age : "";
                string City = !string.IsNullOrEmpty(user.City) ? user.City : "";
                string Country = !string.IsNullOrEmpty(user.Country) ? user.Country : "";
                DateTime CreationDate = user.CreationDate;
                Gender VGender = user.Gender;
                string Id = user.Id;
                string Image = user.Image;
                string PhoneNumber = !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : "";
                UserType UserType = user.UserType;
                ApiUsersViewModel api = new ApiUsersViewModel { Address = Address, Age = Age, City = City, Country = Country, CreationDate = CreationDate, Email = Email, FirstName = FirstName, Gender = VGender, Id = Id, Image = Image, LastName = LastName, PhoneNumber = PhoneNumber, UserName = Username, UserType = user.UserType };
                return Ok(api);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpGet]
        [Route("api/UserStats")]
        public async Task<UserStats> UserStats(string UserId)
        {

            var guests = _context.GuestLists.Where(x => x.UserId == UserId).Count();
            var checklists = _context.CheckLists.Where(x => x.UserId == UserId).Count();
            var wishes = _context.WishLists.Where(x => x.UserId == UserId).Count();
            UserStats stats = new UserStats { guestsCount = guests, todosCount = checklists, wishesCount = wishes };
            return stats;
        }
        [HttpGet]
        [Route("api/AddPushToken")]
        public bool AddPushToken(string UserId, string OldToken, string NewToken, PushDevice PushDevice)
        {
            var old = _context.UserPushToken.Where(x => x.Token == OldToken).FirstOrDefault();
            if (old != null)
                _context.UserPushToken.Remove(old);
            var token = new UserPushToken { UserId = UserId!=""?UserId:null, Token = NewToken, PushDevice = PushDevice };
            _context.UserPushToken.Add(token);
            _context.SaveChanges();
            return true;
        }
        [HttpGet]
        [Route("api/UpdatePushToken")]
        public bool UpdatePushToken(string UserId, string Token)
        {
            var token = _context.UserPushToken.Where(x => x.Token == Token).FirstOrDefault();
            if (token != null)
            {
                token.UserId = UserId;
            }
            _context.SaveChanges();
            return true;
        }
    }
}