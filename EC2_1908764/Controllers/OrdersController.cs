using EC2_1908764.Data;
using EC2_1908764.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EC2_1908764.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrdersController : Controller
    {
        private readonly EC2_1908764Context _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public OrdersController(EC2_1908764Context context,
                                IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int? id)
        {
            if(id == null)
                return NotFound();

            var phone = await _context.Phones.FindAsync(id);

            Orders order = new Orders
            {
                Item = phone.Brand + " " + phone.Model,
                Price = phone.Price,
                ItemPic = phone.Image,
                SKU = phone.ID
            };

            if(phone == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Orders order)
        {
            if(ModelState.IsValid)
            {
                Orders orders = new Orders
                {
                    SKU = order.SKU,
                    Name = order.Name,
                    Address = order.Address,
                    Country = order.Country,
                    PhoneNumber = order.PhoneNumber,
                    Item = order.Item,
                    Quantity = order.Quantity,
                    ItemPic = order.ItemPic,
                    Price = order.Price,
                    DateOrderd = DateTime.Today
                };
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success");
            }

            return RedirectToAction("Checkout",order.SKU);
        }

        public IActionResult Success()
        {
            return View();
        }
    }

}
