using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealRehearsalSpace.Data;
using RealRehearsalSpace.Models;

namespace RealRehearsalSpace.Controllers
{
    public class BookedRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookedRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookedRooms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookedRooms.Include(b => b.Room).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookedRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedRoom = await _context.BookedRooms
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookedRoomId == id);
            if (bookedRoom == null)
            {
                return NotFound();
            }

            return View(bookedRoom);
        }

        // GET: BookedRooms/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: BookedRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookedRoomId,RoomId,TimeId,UserId,BookDate")] BookedRoom bookedRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookedRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name", bookedRoom.RoomId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", bookedRoom.UserId);
            return View(bookedRoom);
        }

        // GET: BookedRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedRoom = await _context.BookedRooms.FindAsync(id);
            if (bookedRoom == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name", bookedRoom.RoomId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", bookedRoom.UserId);
            return View(bookedRoom);
        }

        // POST: BookedRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookedRoomId,RoomId,TimeId,UserId,BookDate")] BookedRoom bookedRoom)
        {
            if (id != bookedRoom.BookedRoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookedRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookedRoomExists(bookedRoom.BookedRoomId))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name", bookedRoom.RoomId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", bookedRoom.UserId);
            return View(bookedRoom);
        }

        // GET: BookedRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedRoom = await _context.BookedRooms
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookedRoomId == id);
            if (bookedRoom == null)
            {
                return NotFound();
            }

            return View(bookedRoom);
        }

        // POST: BookedRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookedRoom = await _context.BookedRooms.FindAsync(id);
            _context.BookedRooms.Remove(bookedRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookedRoomExists(int id)
        {
            return _context.BookedRooms.Any(e => e.BookedRoomId == id);
        }
    }
}
