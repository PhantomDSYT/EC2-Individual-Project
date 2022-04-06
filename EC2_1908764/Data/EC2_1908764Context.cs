using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EC2_1908764.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EC2_1908764.Data
{
    public class EC2_1908764Context : IdentityDbContext<ApplicationUser>
    {
        public EC2_1908764Context (DbContextOptions<EC2_1908764Context> options)
            : base(options)
        {
        }

        public DbSet<EC2_1908764.Models.Phones> Phones { get; set; }
        public DbSet<EC2_1908764.Models.Orders> Orders { get; set; }
    }
}
