using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Microsoft.AspNetCore.Identity;
using PlanMyWeb.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace PlanMyWeb.Controllers.Api
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        DbWebContext _context;
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly IEmailSender _emailSender;
        public UsersController(DbWebContext context, SignInManager<Users> signInManager, UserManager<Users> userManager, IEmailSender emailSender)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string Username, string Password, bool RememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(Username, Password, RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = _context.Users.Where(x => x.UserName == Username).FirstOrDefault();
                if (user != null)
                {
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
                    var events = _context.Events.Where(x => x.UserId == user.Id).FirstOrDefault();
                    if (events != null)
                    {
                        if (!string.IsNullOrEmpty(events.Image))
                            events.Image = "http://" + Request.Host + "/Media/" + events.Image;
                    }
                    else
                        events = new Events { };

                    ApiUsersViewModel api = new ApiUsersViewModel { Address = Address, Age = Age, City = City, Country = Country, CreationDate = CreationDate, Email = user.Email, FirstName = user.FirstName, Gender = VGender, Id = Id, Image = Image, LastName = user.LastName, PhoneNumber = PhoneNumber, UserName = Username, UserType = user.UserType, Events = events };
                    return Ok(api);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] string Username)
        {
            var user = _context.Users.Where(x => x.UserName == Username || x.Email == Username).FirstOrDefault();
            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                return Ok("We sent you a message on your email to reset your password. ");
            }
            else
                return Ok("Sorry, this Username was not found in our database");

        }
        [HttpGet("Register")]
        public async Task<IActionResult> Register(string Username, string Email, string Password, string Token, string FirstName, string LastName, string FBToken)
        {
            Users user = null;
            if(!string.IsNullOrEmpty(FBToken))
            {
                user = _context.Users.Where(x => x.FBToken == FBToken).FirstOrDefault();
                if(user==null)
                {
                    user = _context.Users.Where(x => x.Email == Email).FirstOrDefault();
                }
            }
            if (user == null)
            {
                user = new Users { UserName = Username, Email = Email, CreationDate = DateTime.Now, FirstName = FirstName, LastName = LastName, UserType = UserType.Planner, FBToken = FBToken };
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Planner");


                    await _signInManager.SignInAsync(user, isPersistent: false);
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
                    var events = _context.Events.Where(x => x.UserId == user.Id).FirstOrDefault();
                    if (events != null)
                    {
                        if (!string.IsNullOrEmpty(events.Image))
                            events.Image = "http://" + Request.Host + "/Media/" + events.Image;
                    }
                    else
                        events = new Events { };
                    ApiUsersViewModel api = new ApiUsersViewModel { Address = Address, Age = Age, City = City, Country = Country, CreationDate = CreationDate, Email = Email, FirstName = FirstName, Gender = VGender, Id = Id, Image = Image, LastName = LastName, PhoneNumber = PhoneNumber, UserName = Username, UserType = user.UserType, Events = events };
                    return Ok(api);
                }
                else
                {
                    return Ok(result);
                }
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
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
                var events = _context.Events.Where(x => x.UserId == user.Id).FirstOrDefault();
                if (events != null)
                {
                    if (!string.IsNullOrEmpty(events.Image))
                        events.Image = "http://" + Request.Host + "/Media/" + events.Image;
                }
                else
                    events = new Events { };
                ApiUsersViewModel api = new ApiUsersViewModel { Address = Address, Age = Age, City = City, Country = Country, CreationDate = CreationDate, Email = Email, FirstName = FirstName, Gender = VGender, Id = Id, Image = Image, LastName = LastName, PhoneNumber = PhoneNumber, UserName = Username, UserType = user.UserType, Events = events };
                return Ok(api);
            }
        }
        [HttpGet("UserStats")]
        public async Task<UserStats> UserStats(string UserId)
        {

            var guests = _context.GuestLists.Where(x => x.UserId == UserId).Count();
            var checklists = _context.CheckLists.Where(x => x.UserId == UserId).Count();
            var wishes = _context.WishLists.Where(x => x.UserId == UserId).Count();
            UserStats stats = new UserStats { guestsCount = guests, todosCount = checklists, wishesCount = wishes };
            return stats;
        }
        [HttpGet("AddPushToken")]
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
        [HttpGet("UpdatePushToken")]
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