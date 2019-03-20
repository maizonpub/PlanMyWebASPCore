using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using DAL;
using System.Net;
using System.Text.RegularExpressions;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorItemsController : ControllerBase
    {
        private readonly DbWebContext _context;
        protected int PageSize = 7;
        public VendorItemsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/VendorItems
        [HttpGet]
        public IPagedList<VendorItem> GetVendorItems(int page = 1)
        {
            var vendors = _context.VendorItems.Include(x=>x.VendorItemReviews).Include(x => x.Gallery).OrderBy(x=>x.Title).ToPagedList(page, PageSize);
            foreach(var row in vendors)
            {
                row.Thumb = "https://planmy.me/Media/" + row.Thumb;
                foreach(var g in row.Gallery)
                {
                    g.Image = "https://planmy.me/Media/" + g.Image;
                }
            }
            return vendors;
        }
        [HttpGet("{CategoryId}")]
        public IPagedList<VendorItem> GetVendorItems([FromRoute] int CategoryId, int page = 1)
        {
            var vendors = _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Where(x=> x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0).Include(x=>x.VendorBranches).OrderBy(x => x.Title).ToPagedList(page, PageSize);
            foreach (var row in vendors)
            {
                row.Thumb = "https://planmy.me/Media/" + row.Thumb;
                foreach (var g in row.Gallery)
                {
                    g.Image = "https://planmy.me/Media/" + g.Image;
                }
            }
            return vendors;
        }
        [HttpGet("Random/{CategoryId}")]
        public IEnumerable<VendorItem> GetRandomVendorItems([FromRoute] int CategoryId)
        {
            var vendors = _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Where(x => x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0).Include(x => x.VendorBranches).OrderBy(x=>Guid.NewGuid()).Take(10).ToList();
            for (int i=0;i<vendors.Count;i++)
            {
                vendors[i].Thumb = "https://planmy.me/Media/" + vendors[i].Thumb;
                foreach (var g in vendors[i].Gallery)
                {
                    g.Image = "https://planmy.me/Media/" + g.Image;
                }
            }
            
            return vendors;
        }
        [HttpGet("Featured/{CategoryId}")]
        public IEnumerable<VendorItem> GetFeaturedVendorItems([FromRoute] int CategoryId)
        {
            var vendors = _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Include(x => x.VendorBranches).Where(x => x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0 && x.IsFeatured == true).OrderBy(x => x.Title);
            foreach (var row in vendors)
            {
                row.Thumb = "https://planmy.me/Media/" + row.Thumb;
                foreach (var g in row.Gallery)
                {
                    g.Image = "https://planmy.me/Media/" + g.Image;
                }
            }
            return vendors;
        }
        [HttpGet("Featured")]
        public IEnumerable<VendorItem> GetFeaturedVendorItems()
        {
            var vendors = _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Include(x => x.VendorBranches).Where(x => x.IsFeatured == true).OrderBy(x => x.Title);
            foreach (var row in vendors)
            {
                row.Thumb = "https://planmy.me/Media/" + row.Thumb;
                foreach (var g in row.Gallery)
                {
                    g.Image = "https://planmy.me/Media/" + g.Image;
                }
            }
            return vendors;
        }
        [HttpGet("Favorites/{UserId}")]
        public IEnumerable<VendorItem> GetFavoritesVendorItems([FromRoute] string UserId)
        {
            var vendors = _context.VendorItems.Include(x => x.VendorItemReviews).Include(x=>x.Gallery).Include(x => x.VendorBranches).Where(x => x.WishLists!=null && x.WishLists.Where(y=>y.UserId == UserId).Count()>0).OrderBy(x => x.Title);
            foreach (var row in vendors)
            {
                row.Thumb = "https://planmy.me/Media/" + row.Thumb;
                foreach (var g in row.Gallery)
                {
                    g.Image = "https://planmy.me/Media/" + g.Image;
                }
            }
            return vendors;
        }
        [Route("Search")]
        [HttpPost]
        public List<BindingItem> GetVendorItems([FromForm] int CategoryId, [FromForm] List<VendorTypeValue> Values, [FromForm] string UserId, [FromForm] int page = 1)
        {
            IPagedList<VendorItem> vendors = new List<VendorItem>().ToPagedList();
            var values = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VendorTypeValue>>(Request.Form["Values"].ToString());
            if(values.Count>0)
                vendors =  _context.VendorItems.Include(x => x.VendorItemReviews).Include(x=>x.Gallery).Where(x=>x.Categories.Where(y=>y.VendorCategory.Id == CategoryId).Count()>0 && x.VendorItemTypeValues.Where(y=> values.Contains(y.VendorTypeValue)).Count()>0).OrderBy(x => x.Title).ToPagedList(page, PageSize);
            else
                vendors = _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Where(x => x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0).OrderBy(x => x.Title).ToPagedList(page, PageSize);
            var bindings = new List<BindingItem>();
            foreach (var row in vendors)
            {
                row.Thumb = "https://planmy.me/Media/" + row.Thumb;
                foreach (var g in row.Gallery)
                {
                    g.Image = "https://planmy.me/Media/" + g.Image;
                }
                string favImg = "fav.png";
                if (!string.IsNullOrEmpty(UserId))
                {
                    var rec = _context.WishLists.Where(x => x.VendorItemId == row.Id && x.UserId == UserId).FirstOrDefault();
                    if (rec != null)
                        favImg = "favselected.png";
                    else
                        favImg = "fav.png";
                    
                }
                string rendered = WebUtility.HtmlDecode(Regex.Replace(row.HtmlDescription, "<.*?>", ""));
                rendered = rendered.Length > 100 ? rendered.Substring(0, 100) + "more..." : rendered;
                string title = WebUtility.HtmlDecode(row.Title);
                //string src = post.Thumb != null ? Statics.MediaLink + post.Thumb : "";
                BindingItem binding = new BindingItem { Desc = rendered, Title = title, item = row, Src = row.Thumb, FavImg = favImg, Id = row.Id };
                bindings.Add(binding);

            }
            return bindings;
        }
        [Route("SearchByTerm")]
        [HttpPost]
        public List<BindingItem> GetVendorItems([FromForm] string term, [FromForm] string UserId, [FromForm] int page = 1)
        {
            IPagedList<VendorItem> vendors = new List<VendorItem>().ToPagedList();
            string q = term.ToLower();
            vendors = _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Where(x => x.Title.ToLower().Contains(q) || x.HtmlDescription.ToLower().Contains(q)).OrderBy(x => x.Title).ToPagedList(page, PageSize);
            var bindings = new List<BindingItem>();
            foreach (var row in vendors)
            {
                row.Thumb = "https://planmy.me/Media/" + row.Thumb;
                foreach (var g in row.Gallery)
                {
                    g.Image = "https://planmy.me/Media/" + g.Image;
                }
                string favImg = "fav.png";
                if (!string.IsNullOrEmpty(UserId))
                {
                    var rec = _context.WishLists.Where(x => x.VendorItemId == row.Id && x.UserId == UserId).FirstOrDefault();
                    if (rec != null)
                        favImg = "favselected.png";
                    else
                        favImg = "fav.png";

                }
                string rendered = WebUtility.HtmlDecode(Regex.Replace(row.HtmlDescription, "<.*?>", ""));
                rendered = rendered.Length > 100 ? rendered.Substring(0, 100) + "more..." : rendered;
                string title = WebUtility.HtmlDecode(row.Title);
                //string src = post.Thumb != null ? Statics.MediaLink + post.Thumb : "";
                BindingItem binding = new BindingItem { Desc = rendered, Title = title, item = row, Src = row.Thumb, FavImg = favImg, Id = row.Id };
                bindings.Add(binding);
            }
            return bindings;
        }
        // GET: api/VendorItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorItem = await _context.VendorItems.FindAsync(id);

            if (vendorItem == null)
            {
                return NotFound();
            }
            vendorItem.Thumb = "https://planmy.me/Media/" + vendorItem.Thumb;
            
            return Ok(vendorItem);
        }

        // PUT: api/VendorItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendorItem([FromRoute] int id, [FromBody] VendorItem vendorItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendorItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VendorItems
        [HttpPost]
        public async Task<IActionResult> PostVendorItem([FromBody] VendorItem vendorItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VendorItems.Add(vendorItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendorItem", new { id = vendorItem.Id }, vendorItem);
        }

        // DELETE: api/VendorItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendorItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorItem = await _context.VendorItems.FindAsync(id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            _context.VendorItems.Remove(vendorItem);
            await _context.SaveChangesAsync();

            return Ok(vendorItem);
        }

        private bool VendorItemExists(int id)
        {
            return _context.VendorItems.Any(e => e.Id == id);
        }
    }
}