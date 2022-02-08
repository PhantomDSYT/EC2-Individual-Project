﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EC2_1908764.Models;

namespace EC2_1908764.Data
{
    public class EC2_1908764Context : DbContext
    {
        public EC2_1908764Context (DbContextOptions<EC2_1908764Context> options)
            : base(options)
        {
        }

        public DbSet<EC2_1908764.Models.Phones> Phones { get; set; }
    }
}
