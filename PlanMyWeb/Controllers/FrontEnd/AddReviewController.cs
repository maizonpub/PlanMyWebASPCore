using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlanMyWeb.Controllers.FrontEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddReviewController : ControllerBase
    {
        protected DbWebContext _context;
        public AddReviewController(DbWebContext context)
        {
            this._context = context;
        }

        // POST: api/AddReview
        [HttpPost]
        public void Post(string comment, string author, string email, string subject, int ratinghidden, int VendorItemId)
        {
            comment = Request.Form["comment"];
            author = Request.Form["author"];
            email = Request.Form["email"];
            subject = Request.Form["subject"];
            ratinghidden = int.Parse(Request.Form["ratinghidden"]);
            VendorItemId = int.Parse(Request.Form["VendorItemId"]);
            var vendorItem = _context.VendorItems.Where(x => x.Id == VendorItemId).FirstOrDefault();
            _context.VendorItemReviews.Add(new VendorItemReview { comment = comment, email = email, subject = subject, VendorItem = vendorItem, rating = ratinghidden, Status = ReviewStatus.Pending, DateIn = DateTime.Now });
        }

        // PUT: api/AddReview/5
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
