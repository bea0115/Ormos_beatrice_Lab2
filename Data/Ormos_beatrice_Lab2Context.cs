using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ormos_beatrice_Lab2.Models;

namespace Ormos_beatrice_Lab2.Data
{
    public class Ormos_beatrice_Lab2Context : DbContext
    {
        public Ormos_beatrice_Lab2Context (DbContextOptions<Ormos_beatrice_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Ormos_beatrice_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Ormos_beatrice_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    }
}
