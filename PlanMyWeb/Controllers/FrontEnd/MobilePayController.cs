using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.FrontEnd
{
    public class MobilePayController : Controller
    {
        protected DbWebContext _context;
        private readonly UserManager<DAL.Users> _userManager;
        private readonly IEmailSender _emailSender;
        IHostingEnvironment _hostingEnvironment;
        public MobilePayController(DbWebContext context, UserManager<DAL.Users> userManager, IEmailSender emailSender, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(int OrderId)
        {
            var order = _context.Orders.Where(x => x.Id == OrderId).Include(x=>x.Users).Include(x=>x.Users.UserPaymentTokens).Include(x=>x.BasketItems).SingleOrDefault();
            var paymentsettings = _context.PaymentSettings.FirstOrDefault();
            PaymentViewModel model = new PaymentViewModel { Order = order, BasketItems = order.BasketItems, PaymentSetting = paymentsettings };
            return View(model);
        }
        public IActionResult Confirm()
        {
            PaymentType paymentType = PaymentType.Tokenization;
            var paymentsettings = _context.PaymentSettings.Where(x => x.PaymentType == paymentType).FirstOrDefault();
            return View(paymentsettings);
        }
        public async Task<IActionResult> Receipt(string reason_code, string payment_token, string req_transaction_uuid)
        {
            var order = _context.Orders.Where(x => x.TransactionUUID == req_transaction_uuid).Include(x => x.Users).Include(x => x.BasketItems).OrderByDescending(x => x.Id).FirstOrDefault();
            ViewData["PaymentTitle"] = "Thank You";
            if (reason_code == "100")
            {
                PaymentType paymentType = PaymentType.Tokenization;
                var paymentsettings = _context.PaymentSettings.Where(x => x.PaymentType == paymentType).FirstOrDefault();
                if (!string.IsNullOrEmpty(payment_token))
                {

                    var dbtoken = _context.UserPaymentTokens.Where(x => x.Token == payment_token && x.TokenStatus == TokenStatus.Valid).FirstOrDefault();
                    if (dbtoken == null)
                    {
                        dbtoken = new UserPaymentToken { Token = payment_token, TokenStatus = TokenStatus.Valid, PaymentSetting = paymentsettings, User = order.Users };
                        _context.UserPaymentTokens.Add(dbtoken);
                    }
                    var oldtokens = _context.UserPaymentTokens.Where(x => x.Token != payment_token && x.User == order.Users && x.TokenStatus == TokenStatus.Valid).AsEnumerable();
                    foreach (var t in oldtokens)
                    {
                        t.TokenStatus = TokenStatus.InValid;
                    }
                }
                order.OrderStatus = OrderStatus.Completed;
                ViewData["Response"] = "Your order was processed successfully, thank you.";
                SendEmail(order);
            }
            else if (reason_code == "481")
            {

                PaymentType paymentType = PaymentType.Tokenization;
                var paymentsettings = _context.PaymentSettings.Where(x => x.PaymentType == paymentType).FirstOrDefault();
                if (!string.IsNullOrEmpty(payment_token))
                {

                    var dbtoken = _context.UserPaymentTokens.Where(x => x.Token == payment_token && x.TokenStatus == TokenStatus.Valid).FirstOrDefault();
                    if (dbtoken == null)
                    {
                        dbtoken = new UserPaymentToken { Token = payment_token, TokenStatus = TokenStatus.Valid, PaymentSetting = paymentsettings, User = order.Users };
                        _context.UserPaymentTokens.Add(dbtoken);
                    }
                    var oldtokens = _context.UserPaymentTokens.Where(x => x.Token != payment_token && x.User == order.Users && x.TokenStatus == TokenStatus.Valid).AsEnumerable();
                    foreach (var t in oldtokens)
                    {
                        t.TokenStatus = TokenStatus.InValid;
                    }
                }
                order.OrderStatus = OrderStatus.Completed;
                ViewData["Response"] = "Your order was processed successfully, thank you.";
                SendEmail(order);
            }
            else
            {
                ViewData["PaymentTitle"] = "Payment Error";
                string responseMsg = "";
                switch (reason_code)
                {
                    case "102":
                        responseMsg = "An Error has occured please try again later.";
                        break;
                    case "104":
                        responseMsg = "An Error has occured please try again later.";
                        break;
                    case "110":
                        order.OrderStatus = OrderStatus.OnHold;
                        responseMsg = "The transaction was approved, but an error has occured, however a representative will contact you the soonest possible. Thank you.";
                        break;
                    case "200":
                        order.OrderStatus = OrderStatus.OnHold;
                        responseMsg = "The transaction was approved, but an error has occured, however a representative will contact you the soonest possible. Thank you.";
                        break;
                    case "201":
                        order.OrderStatus = OrderStatus.OnHold;
                        responseMsg = "The transaction was approved, but an error has occured, however a representative will contact you the soonest possible. Thank you.";
                        break;
                    case "202":
                        responseMsg = "Sorry, Your card was expired.";
                        break;
                    case "203":
                        responseMsg = "Insufficient funds in the account.";
                        break;
                    case "204":
                        responseMsg = "Stolen or lost card.";
                        break;
                    case "205":
                        responseMsg = " Issuing bank unavailable.";
                        break;
                    case "207":
                        responseMsg = "Inactive card or card not authorized for card-not-present transactions.";
                        break;
                    case "208":
                        responseMsg = "Inactive card or card not authorized for card-not-present transactions.";
                        break;
                    case "210":
                        responseMsg = "The card has reached the credit limit.";
                        break;
                    case "211":
                        responseMsg = "Invalid CVN.";
                        break;
                    case "221":
                        responseMsg = "An Error has occured please try again later.";
                        break;
                    case "222":
                        responseMsg = " Account frozen.";
                        break;
                    case "230":
                        order.OrderStatus = OrderStatus.OnHold;
                        responseMsg = "The transaction was approved, but an error has occured, however a representative will contact you the soonest possible. Thank you.";
                        break;
                    case "231":

                        responseMsg = "Invalid account number.";
                        break;
                    case "232":
                        responseMsg = "The card type is not accepted by the payment processor.";
                        break;
                    case "233":
                        order.OrderStatus = OrderStatus.OnHold;
                        responseMsg = "The transaction was approved, but an error has occured, however a representative will contact you the soonest possible. Thank you.";
                        break;
                    case "234":
                        responseMsg = "An Error has occured please try again later.";
                        break;
                    case "236":
                        responseMsg = "An Error has occured please try again later";
                        break;
                    case "240":
                        responseMsg = "The card type sent is invalid or does not correlate with the credit card number.";
                        break;
                    case "475":
                        responseMsg = "An Error has occured please try again later.";
                        break;
                    case "476":
                        responseMsg = "An Error has occured please try again later.";
                        break;
                    case "520":
                        order.OrderStatus = OrderStatus.OnHold;
                        responseMsg = "The transaction was approved, but an error has occured, however a representative will contact you the soonest possible. Thank you.";
                        break;
                    default:
                        responseMsg = "An Error has occured please try again later.";
                        break;
                }
                ViewData["Response"] = responseMsg;
            }
            var basketitems = _context.BasketItems.Where(x => x.Order.Id == order.Id).Include(x => x.Offers).Include(x => x.Offers.User).ToList();
            _context.SaveChanges();
            return View(basketitems);
        }
        async void SendEmail(Order order)
        {
            StreamReader sr = new StreamReader(_hostingEnvironment.WebRootPath + "/emailtemplate/OrderConfirmed.html");
            string temp = sr.ReadToEnd();
            var basketitems = _context.BasketItems.Where(x => x.Order.Id == order.Id).Include(x => x.Offers).Include(x => x.Offers.User).ToList();
            string ordertable = "";
            foreach (var item in basketitems)
            {
                ordertable += @"<tr class=""m_-5965336264313205829order_item"">
                                                                                                        <td class=""m_-5965336264313205829td"" style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word"">
                                                                                                            " + item.Offers.Title + @"<ul class=""m_-5965336264313205829wc-item-meta"" style=""font-size:small;margin:1em 0 0;padding:0;list-style:none"">
                                                                                                                <li style=""margin:0.5em 0 0;padding:0"">
                                                                                                                    <strong class=""m_-5965336264313205829wc-item-meta-label"" style=""float:left;margin-right:.25em;clear:both"">Sold By:</strong>" + (item.Offers.User != null ? item.Offers.User.UserName : "PLAN MY") + @"</p>
                                                                                                                </li>
                                                                                                            </ul>
                                                                                                        </td>
                                                                                                        <td class=""m_-5965336264313205829td"" style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif"">
                                                                                                            " + item.Quantity + @"
                                                                                                        </td>
                                                                                                        <td class=""m_-5965336264313205829td"" style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif"">
                                                                                                            <span class=""m_-5965336264313205829woocommerce-Price-amount m_-5965336264313205829amount""><span class=""m_-5965336264313205829woocommerce-Price-currencySymbol"">$</span>" + item.TotalPrice + @"</span>
                                                                                                        </td>
                                                                                                    </tr>";
            }

            var user = _context.Users.Where(x => x.Id == order.UsersId).FirstOrDefault();
            temp = temp.Replace("$username$", (user != null ? user.UserName : "Customer")).Replace("$referencenumber$", order.ReferenceNumber).Replace("$date$", order.TransactionDate.ToString()).Replace("$order$", ordertable).Replace("$subtotal$", order.Total.ToString()).Replace("$total$", order.Total.ToString());
            sr.Close();
            await _emailSender.SendEmailAsync(
                order.Users.Email,
                "Order confirmed from planmy",
                temp);
        }
    }
}