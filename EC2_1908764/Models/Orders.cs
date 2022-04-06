using System;
using System.ComponentModel.DataAnnotations;

namespace EC2_1908764.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public int SKU { get; set; }

        [Required(ErrorMessage ="Please enter name of receiver.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter shipping address.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter country.")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter phone number.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ItemPic { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOrderd { get; set; }
    }
}
