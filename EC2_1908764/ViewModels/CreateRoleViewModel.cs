using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EC2_1908764.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
