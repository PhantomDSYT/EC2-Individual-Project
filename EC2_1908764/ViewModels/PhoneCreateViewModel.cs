using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1908764.ViewModels
{
    public class PhoneCreateViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter brand name."), MaxLength(15)]
        public string Brand { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter model."), MaxLength(30)]
        public String Model { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter manufactured date.")]
        [Display(Name = "Release Date")]
        public DateTime ManufactureDate { get; set; }

        [Required(ErrorMessage = "Enter the quantity.")]
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter price.")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public IFormFile Photo { get; set; }
    }
}
