using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1908764.Models
{
    public class Phones
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter brand name."), MaxLength(15)]
        public string Brand { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter model."), MaxLength(15)]
        public String Model { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter manufactured date.")]
        [Display(Name = "Manufactured Date")]
        public DateTime ManufactureDate { get; set; }

        [Required(ErrorMessage = "Enter the quantity.")]
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter price.")]
        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}
