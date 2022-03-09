using Microsoft.AspNetCore.Identity;

namespace EC2_1908764.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }    
    }
}
