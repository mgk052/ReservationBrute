using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationBrute.Data;
using ReservationBrute.Models;

namespace ReservationBrute.Views.TableBookings
{
    public class TableBookingsController : Controller
    {
        private readonly ReservationBruteTable _context;

        public TableBookingsController(ReservationBruteTable context)
        {
            _context = context;
        }

        // GET: TableBookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.TableBooking.ToListAsync());
        }

        // GET: TableBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableBooking = await _context.TableBooking
                .FirstOrDefaultAsync(m => m.id == id);
            if (tableBooking == null)
            {
                return NotFound();
            }

            return View(tableBooking);
        }

        // GET: TableBookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TableBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,location,booked_seats,booking_no,date_time")] TableBooking tableBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tableBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tableBooking);
        }

        // GET: TableBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableBooking = await _context.TableBooking.FindAsync(id);
            if (tableBooking == null)
            {
                return NotFound();
            }
            return View(tableBooking);
        }

        // POST: TableBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,location,booked_seats,booking_no,date_time")] TableBooking tableBooking)
        {
            if (id != tableBooking.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tableBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableBookingExists(tableBooking.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tableBooking);
        }

        // GET: TableBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableBooking = await _context.TableBooking
                .FirstOrDefaultAsync(m => m.id == id);
            if (tableBooking == null)
            {
                return NotFound();
            }

            return View(tableBooking);
        }

        // POST: TableBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tableBooking = await _context.TableBooking.FindAsync(id);
            _context.TableBooking.Remove(tableBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableBookingExists(int id)
        {
            return _context.TableBooking.Any(e => e.id == id);
        }
    }
}
