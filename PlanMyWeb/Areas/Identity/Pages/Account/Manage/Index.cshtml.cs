using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlanMyWeb.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public IndexModel(
            DbWebContext context,
            IHostingEnvironment hostingEnvironment,
            UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            IEmailSender emailSender)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Title")]
            public string EventTitle { get; set; }

            [Display(Name = "Description")]
            public string EventDescription { get; set; }

            [Display(Name = "Date")]
            public DateTime EventDate { get; set; }

            [Display(Name = "Event Image")]
            public IFormFile EventImage { get; set; }
            [Display(Name = "Profile Pic")]
            public IFormFile UserImage { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var events = _context.Events.Where(x => x.User == user).FirstOrDefault();
            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            Username = userName;
            var eventTitle = events != null ? events.Title : "";
            var eventDescription = events != null ? events.Description : "";
            var eventDate = events != null ? events.Date : DateTime.Now;
            ViewData["img"] = events != null ? "/Media/" + events.Image : "";
            ViewData["pimg"] = user.Image;
            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                EventTitle = eventTitle,
                EventDescription = eventDescription,
                EventDate = eventDate
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    //var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var isconfirmed = await _userManager.IsPhoneNumberConfirmedAsync(user);
            if (isconfirmed && Input.PhoneNumber != phoneNumber)
            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, Input.PhoneNumber);
                var setPhoneResult = await _userManager.ChangePhoneNumberAsync(user, Input.PhoneNumber, token);
                if (!setPhoneResult.Succeeded)
                {
                    //var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }
            if (Input.UserImage.Length > 0)
            {
                string ext = System.IO.Path.GetExtension(Input.UserImage.FileName);
                string filename = Guid.NewGuid().ToString() + ext;
                UploadFile(Input.UserImage, filename);
                user.Image = filename;
            }
            var events = _context.Events.Where(x => x.User == user).FirstOrDefault();
            if(events!=null)
            {
                events.Title = !string.IsNullOrEmpty(Input.EventTitle) ? Input.EventTitle : events.Title;
                events.Description = !string.IsNullOrEmpty(Input.EventDescription) ? Input.EventDescription : events.Description;
                events.Date = Input.EventDate != null ? Input.EventDate : events.Date;
                if (Input.EventImage.Length > 0)
                {
                    string ext = System.IO.Path.GetExtension(Input.EventImage.FileName);
                    string filename = Guid.NewGuid().ToString() + ext;
                    UploadFile(Input.EventImage, filename);
                    events.Image = filename;
                }
            }
            else
            {
                string title = Input.EventTitle;
                string description = Input.EventDescription;
                DateTime date = Input.EventDate;
                
                events = new Events { Title = title, User = user, Date = date, Description = description };
                if (Input.EventImage.Length > 0)
                {
                    string ext = System.IO.Path.GetExtension(Input.EventImage.FileName);
                    string filename = Guid.NewGuid().ToString() + ext;
                    UploadFile(Input.EventImage, filename);
                    events.Image = filename;
                }
                _context.Events.Add(events);
            }
            //_context.SaveChanges();
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
