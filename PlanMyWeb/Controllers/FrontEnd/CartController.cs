using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PlanMyWeb.Controllers.FrontEnd
{
    [Authorize(Roles = "Admin,Vendor,Planner")]
    public class CartController : Controller
    {
        protected DbWebContext _context;
        private readonly UserManager<DAL.Users> _userManager;
        public CartController(DbWebContext context, UserManager<DAL.Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var basketItems = _context.BasketItems.Where(x => x.Order.OrderStatus == OrderStatus.Pending_Payment && x.Order.Users.Id == userId).Include(x=>x.Offers).AsEnumerable();
            return View(basketItems);
        }
        public async Task<IActionResult> AddToBasket(int ItemId, int Quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            var offer = _context.Offers.Where(x => x.Id == ItemId).SingleOrDefault();
            
            var order = _context.Orders.Where(x => x.Users == user && x.OrderStatus == OrderStatus.Pending_Payment).OrderByDescending(x=>x.Id).FirstOrDefault();
            if (order == null)
            {
                var refnumber = Get8Digits();
                string requestId = Guid.NewGuid().ToString().Replace("-", "");
                order = new Order { OrderStatus = OrderStatus.Pending_Payment, Users = user, ReferenceNumber = refnumber, TransactionUUID = requestId };
                _context.Orders.Add(order);
            }
            var basket = _context.BasketItems.Where(x => x.Offers == offer && x.Order == order).FirstOrDefault();
            if (basket == null)
            {
                decimal total = (decimal)(offer.Price * Quantity);
                if (offer.SaleFromDate <= DateTime.Now && offer.SaleToDate >= DateTime.Now)
                    total = (decimal)((offer.SalePrice != null || offer.SalePrice != 0) ? offer.SalePrice * Quantity : offer.Price * Quantity);
                basket = new BasketItems { Offers = offer, Quantity = Quantity, TotalPrice = total, Order = order };
                _context.BasketItems.Add(basket);
            }
            else
            {
                basket.Quantity+=1;
                decimal total = (decimal)(offer.Price * basket.Quantity);
                if (offer.SaleFromDate <= DateTime.Now && offer.SaleToDate >= DateTime.Now)
                    total = (decimal)((offer.SalePrice != null || offer.SalePrice != 0) ? offer.SalePrice * basket.Quantity : offer.Price * basket.Quantity);
                basket.TotalPrice = total;
                
            }
            order.Total += basket.TotalPrice;


            await _context.SaveChangesAsync();
            return View("success");
        }
        public async Task<IActionResult> UpdateBasket()
        {
            foreach(var key in Request.Form.Keys)
            {
                if(key.Contains("quantity_"))
                {
                    var id = int.Parse(key.Split('_')[1]);
                    var quantity = int.Parse(Request.Form[key]);
                    var offer = _context.Offers.Where(x => x.Id == id).SingleOrDefault();
                    var userId = _userManager.GetUserId(User);
                    var basket = _context.BasketItems.Where(x => x.Order.OrderStatus == OrderStatus.Pending_Payment && x.Order.Users.Id == userId && x.Offers == offer).OrderByDescending(x=>x.Id).FirstOrDefault();
                    if (basket != null)
                    {
                        basket.Quantity = quantity;
                        decimal total = (decimal)(offer.Price * basket.Quantity);
                        if (offer.SaleFromDate <= DateTime.Now && offer.SaleToDate >= DateTime.Now)
                            total = (decimal)((offer.SalePrice != null || offer.SalePrice != 0) ? offer.SalePrice * basket.Quantity : offer.Price * basket.Quantity);
                        await _context.SaveChangesAsync();
                    }

                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveFromBasket(int BasketId)
        {
            var basket = _context.BasketItems.Where(x => x.Id == BasketId).SingleOrDefault();
            if (basket != null)
            {
                _context.BasketItems.Remove(basket);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public string Get8Digits()
        {

            var bytes = new byte[4];

            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(bytes);

            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;

            return String.Format("{0:D8}", random);

        }
    }
}