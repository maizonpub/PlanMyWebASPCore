using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PlanMyWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DbWebContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _hostingEnvironment;
        public OrdersController(DbWebContext context, IEmailSender emailSender, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _emailSender = emailSender;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<Order> GetOrders(string UserId)
        {
            return _context.Orders.Where(x=>x.UsersId == UserId).Include(x=>x.BasketItems);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        [HttpPost]
        public async Task<Order> PostOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            order.TransactionDate = DateTime.Now;
            order.ReferenceNumber = Get8Digits();
            string requestId = Guid.NewGuid().ToString().Replace("-", "");
            order.TransactionUUID = requestId;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            StreamReader sr = new StreamReader(_hostingEnvironment.WebRootPath + "/emailtemplate/OrderConfirmed.html");
            string temp = sr.ReadToEnd();
            var basketitems = _context.BasketItems.Where(x => x.Order.Id == order.Id).Include(x => x.Offers).Include(x=>x.Offers.User).ToList();
            string ordertable = "";
            foreach (var item in basketitems)
            {
                ordertable += @"<tr class=""m_-5965336264313205829order_item"">
                                                                                                        <td class=""m_-5965336264313205829td"" style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word"">
                                                                                                            $productname$<ul class=""m_-5965336264313205829wc-item-meta"" style=""font-size:small;margin:1em 0 0;padding:0;list-style:none"">
                                                                                                                <li style=""margin:0.5em 0 0;padding:0"">
                                                                                                                    <strong class=""m_-5965336264313205829wc-item-meta-label"" style=""float:left;margin-right:.25em;clear:both"">Sold By:</strong>"+item.Offers.User.UserName+@"</p>
                                                                                                                </li>
                                                                                                            </ul>
                                                                                                        </td>
                                                                                                        <td class=""m_-5965336264313205829td"" style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif"">
                                                                                                            "+item.Quantity+@"
                                                                                                        </td>
                                                                                                        <td class=""m_-5965336264313205829td"" style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif"">
                                                                                                            <span class=""m_-5965336264313205829woocommerce-Price-amount m_-5965336264313205829amount""><span class=""m_-5965336264313205829woocommerce-Price-currencySymbol"">$</span>"+item.TotalPrice+@"</span>
                                                                                                        </td>
                                                                                                    </tr>";
            }
            
            temp = temp.Replace("$username$", order.Users.UserName).Replace("$referencenumber$", order.ReferenceNumber).Replace("$date$", order.TransactionDate.ToString()).Replace("$order$", ordertable).Replace("$subtotal$", order.Total.ToString()).Replace("$total$", order.Total.ToString());
            sr.Close();
            await _emailSender.SendEmailAsync(
                order.Users.Email,
                "Order confirmed from planmy",
                temp);
            return order;
        }
        public string Get8Digits()
        {

            var bytes = new byte[4];

            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(bytes);

            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;

            return String.Format("{0:D8}", random);

        }
        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}