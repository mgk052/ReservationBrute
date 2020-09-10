using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationBrute.Areas.Identity.Data;
using ReservationBrute.Data;

[assembly: HostingStartup(typeof(ReservationBrute.Areas.Identity.IdentityHostingStartup))]
namespace ReservationBrute.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ReservationBruteContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ReservationBruteAdmin")));

                services.AddDefaultIdentity<ReservationBruteUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ReservationBruteContext>();
            });
        }
    }
}