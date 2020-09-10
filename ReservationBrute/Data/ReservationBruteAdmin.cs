using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationBrute.Models;

namespace ReservationBrute.Data
{
    public class ReservationBruteAdmin : DbContext
    {
        public ReservationBruteAdmin (DbContextOptions<ReservationBruteAdmin> options)
            : base(options)
        {
        }

        public DbSet<ReservationBrute.Models.Reservations> Reservations { get; set; }
    }
}
