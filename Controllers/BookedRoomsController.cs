using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RealRehearsalSpace.Data;
using RealRehearsalSpace.Models;
using RealRehearsalSpace.Models.ViewModels;

namespace RealRehearsalSpace.Controllers
{
    [Authorize]
    public class BookedRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /* Represents user data */
        private readonly UserManager<ApplicationUser> _userManager;

        /* Retrieves the data for the current user from _userManager */
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private readonly IConfiguration _config;

        public BookedRoomsController(ApplicationDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: BookedRooms
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var applicationDbContext = _context.BookedRooms.Include(b => b.Room).Include(b => b.TimeTable).Where(u => u.UserId == user.Id);
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
                .Include(b => b.TimeTable)
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
            ViewData["TimeTableId"] = new SelectList(_context.TimeTables, "TimeTableId", "BookDate");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: BookedRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookedRoomId,RoomId,TimeTableId,UserId,BookDate")] BookedRoom bookedRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookedRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name", bookedRoom.RoomId);
            ViewData["TimeTableId"] = new SelectList(_context.TimeTables, "TimeTableId", "BookDate", bookedRoom.TimeTableId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", bookedRoom.UserId);
            return View(bookedRoom);
        }

        [Authorize]
        //Custom Create method called AddToBookedRooms
        public async Task<IActionResult> AddToBookedRooms(int id, TimeTable timeTable)
        {
            Room roomToAdd = await _context.Rooms.SingleOrDefaultAsync(r => r.RoomId == id);

            var user = await GetCurrentUserAsync();

            BookedRoom currentBookedRoom = new BookedRoom();
            currentBookedRoom.RoomId = roomToAdd.RoomId;
            currentBookedRoom.BookDate = DateTime.Today.ToShortDateString();
            currentBookedRoom.TimeTableId = timeTable.TimeTableId;
            currentBookedRoom.UserId = user.Id.ToString();
            _context.Add(currentBookedRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "BookedRooms");
        }

        // POST: BookedRooms/Edit/5
        // Custom Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookedRoomViewModel viewModel)
        {

            ModelState.Remove("bookedRoom.User");
            ModelState.Remove("bookedRoom.UserId");
            ModelState.Remove("bookedRoom.BookDate");
            ModelState.Remove("timeTable.BookTime");

            ModelState.Remove("UserId");
            ModelState.Remove("BookDate");

            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                BookedRoom reassignedBookedRoom = await _context.BookedRooms
                .FirstOrDefaultAsync(m => m.BookedRoomId == id);
                reassignedBookedRoom.TimeTableId = viewModel.timeTable.TimeTableId;
                reassignedBookedRoom.BookedRoomId = id;
                reassignedBookedRoom.UserId = user.Id;
                _context.Update(reassignedBookedRoom);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: BookedRooms/Edit/5
        // Semi-Custom Edit GET
        public async Task<IActionResult> Edit(int? id)
        {

            string roomsql = $@"
            SELECT
                br.BookedRoomId,
                br.RoomId,
				br.TimeTableId,
				br.UserId,
				br.BookDate
            FROM BookedRooms br
            WHERE br.BookedRoomId = {id}
            ";

            if (id == null)
            {
                return NotFound();
            }

            using (IDbConnection conn = Connection)
            {
                BookedRoom bookedRoom = await conn.QueryFirstAsync<BookedRoom>(roomsql);
                EditBookedRoomViewModel model = new EditBookedRoomViewModel(_config, bookedRoom);
                return View(model);
            }
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
                .Include(b => b.TimeTable)
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