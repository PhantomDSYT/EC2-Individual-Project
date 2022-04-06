using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EC2_1908764.Data;
using EC2_1908764.Models;
using EC2_1908764.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace EC2_1908764.Controllers
{
    public class PhonesController : Controller
    {
        private readonly EC2_1908764Context _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public PhonesController(EC2_1908764Context context,
                                IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Phones
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Phones.ToListAsync());
        }

        // GET: Phones/Details/5
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phones = await _context.Phones
                .FirstOrDefaultAsync(m => m.ID == id);
            if (phones == null)
            {
                return NotFound();
            }

            return View(phones);
        }

        // GET: Phones/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PhoneCreateViewModel pmodel)
        {
            if (ModelState.IsValid)
            {
                string uniquefilename = ProcessUploadedFile(pmodel);

                Phones phones = new Phones
                {
                    Brand = pmodel.Brand,
                    Model = pmodel.Model,
                    ManufactureDate = pmodel.ManufactureDate,
                    Quantity = pmodel.Quantity,
                    Price = pmodel.Price,
                    Image = uniquefilename
                };
                _context.Add(phones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Phones/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phones = await _context.Phones.FindAsync(id);

            PhoneEditViewModel phoneEditViewModel = new PhoneEditViewModel
            {
                ID = phones.ID,
                Brand = phones.Brand,
                Model = phones.Model,
                ManufactureDate = phones.ManufactureDate,
                Quantity = phones.Quantity,
                Price = phones.Price,
                ExistingPhotoPath = phones.Image

            };

            if (phones == null)
            {
                return NotFound();
            }
            return View(phoneEditViewModel);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(PhoneEditViewModel pmodel)
        {
            if (!PhonesExists(pmodel.ID))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var phone = await _context.Phones.FindAsync(pmodel.ID);
                    phone.Brand = pmodel.Brand;
                    phone.Model = pmodel.Model;
                    phone.ManufactureDate = pmodel.ManufactureDate;
                    phone.Quantity = pmodel.Quantity;
                    phone.Price = pmodel.Price;
                    if(pmodel.Photo != null)
                    {
                        if(pmodel.ExistingPhotoPath != null)
                        {
                            String filepath = Path.Combine(hostingEnvironment.WebRootPath, "images", "phones", pmodel.ExistingPhotoPath);
                            System.IO.File.Delete(filepath);

                        }
                        phone.Image = ProcessUploadedFile(pmodel);
                    }
                    
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhonesExists(pmodel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string ProcessUploadedFile(PhoneCreateViewModel pmodel)
        {
            string uniquefilename = null;
            if (pmodel.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images", "phones");
                uniquefilename = Guid.NewGuid().ToString() + "_" + pmodel.Photo.FileName;
                string filepath = Path.Combine(uploadsFolder, uniquefilename);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                pmodel.Photo.CopyTo(fs);
                fs.Close();
            }

            return uniquefilename;
        }

        // GET: Phones/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phones = await _context.Phones
                .FirstOrDefaultAsync(m => m.ID == id);

            PhoneEditViewModel phoneEditViewModel = new PhoneEditViewModel
            {
                ID = phones.ID,
                Brand = phones.Brand,
                Model = phones.Model,
                ManufactureDate = phones.ManufactureDate,
                Quantity = phones.Quantity,
                Price = phones.Price,
                ExistingPhotoPath = phones.Image

            };

            if (phones == null)
            {
                return NotFound();
            }

            return View(phoneEditViewModel);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phones = await _context.Phones.FindAsync(id);

            PhoneEditViewModel phoneEditViewModel = new PhoneEditViewModel
            {
                ExistingPhotoPath = phones.Image
            };

            if (phoneEditViewModel.ExistingPhotoPath != null)
            {
                String filepath = Path.Combine(hostingEnvironment.WebRootPath, "images", "phones", phoneEditViewModel.ExistingPhotoPath);
                System.IO.File.Delete(filepath);

            }

            _context.Phones.Remove(phones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhonesExists(int id)
        {
            return _context.Phones.Any(e => e.ID == id);
        }

    }
}
