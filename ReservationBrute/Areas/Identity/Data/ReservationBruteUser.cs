﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ReservationBrute.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ReservationBruteUser class
    public class ReservationBruteUser : IdentityUser
    {
        public string Role { get; internal set; }
    }
}
