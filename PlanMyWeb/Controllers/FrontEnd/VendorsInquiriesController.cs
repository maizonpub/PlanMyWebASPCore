using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace PlanMyWeb.Controllers.FrontEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsInquiriesController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public VendorsInquiriesController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        // POST: api/VendorsInquiries
        [HttpPost]
        public async void Post(string name, string phone, string email, DateTime date, string guest, string sendme, string item_title)
        {
            string body = "";
            string subject = "";
            body = string.Format(@"<table>
                                <tr><th>Item</th><td>{0}</td></tr>
                                <tr><th>Name</th><td>{1}</td></tr>
                                <tr><th>Phone</th><td>{2}</td></tr>
                                <tr><th>Email</th><td>{3}</td></tr>
                                <tr><th>Date</th><td>{4}</td></tr>
                                <tr><th>Number of Guests</th><td>{5}</td></tr>
                                <tr><th>Send me info via</th><td>{6}</td></tr>", Request.Form["item_title"], Request.Form["name"], Request.Form["phone"], Request.Form["email"], Request.Form["date"], Request.Form["guest"], Request.Form["sendme"]);
            subject = "User " + Request.Form["name"] + " sent inquiry on Vendor Item : " + Request.Form["item_title"] + " from planmy.me";
            await _emailSender.SendEmailAsync("charbel@maizonpub.com", subject, body);
        }

        // PUT: api/VendorsInquiries/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
