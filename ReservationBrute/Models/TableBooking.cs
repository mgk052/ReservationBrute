using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationBrute.Models
{
    public class TableBooking
    {
        public int id { get; set; }
        public string location { get; set; }
        public int booked_seats { get; set; }
        public int booking_no { get; set; }
        public DateTime date_time { get; set; }
    }
}
