using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationBrute.Models;

namespace ReservationBrute.Data
{
    public class ReservationBruteTable : DbContext
    {
        public ReservationBruteTable (DbContextOptions<ReservationBruteTable> options)
            : base(options)
        {
        }

        public DbSet<ReservationBrute.Models.TableBooking> TableBooking { get; set; }
    }
}
