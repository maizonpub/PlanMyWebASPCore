using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Microsoft.AspNetCore.Identity;

namespace PlanMyWeb.Controllers.Api
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
        public async Task<Users> Register(string Username, string Email, string Password, string Token)
        {
            var user = new Users { UserName = Username, Email = Email, CreationDate = DateTime.Now };
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
                return user;
            }
            else
            {
                return null;
            }
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