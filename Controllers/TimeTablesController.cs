using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RealRehearsalSpace.Data;
using RealRehearsalSpace.Models;
using RealRehearsalSpace.Models.ViewModels;

namespace RealRehearsalSpace.Controllers
{
    public class TimeTablesController : Controller
    {
        private readonly IConfiguration _config;

        public TimeTablesController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: TimeTables
        public async Task<ActionResult> Index()
        {
            using (IDbConnection conn = Connection)
            {

                IEnumerable<TimeTable> timeTables = await conn.QueryAsync<TimeTable>(@"
                    SELECT 
                        t.TimeTableId,
                        t.BookTime,
                    FROM TimeTable t
                ");
                return View(timeTables);
            }
        }

        // GET: TimeTables/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string sql = $@"
            SELECT
                t.TimeTableId,
                t.BookTime
            FROM TimeTables t
            WHERE t.TimeTableId = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                TimeTable timeTable = await conn.QueryFirstAsync<TimeTable>(sql);
                CreateBookedRoomViewModel model = new CreateBookedRoomViewModel(_config);
                return View(model);
            }
        }

        // GET: TimeTables/Create
        public ActionResult Create()
        {
            var model = new CreateBookedRoomViewModel(_config);
            return View(model);
        }

        // POST: TimeTables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBookedRoomViewModel model)
        {
            string sql = $@"INSERT INTO TimeTable 
            (BookTime)
            VALUES
            (
                '{model.timeTable.BookTime}'
            );";

            using (IDbConnection conn = Connection)
            {
                var newId = await conn.ExecuteAsync(sql);
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: TimeTables/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            string sql = $@"
            SELECT
                t.TimeTableId,
                t.BookTime,
            FROM TimeTable t
            WHERE t.TimeTableId = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                TimeTable timeTable = await conn.QueryFirstAsync<TimeTable>(sql);
                CreateBookedRoomViewModel model = new CreateBookedRoomViewModel(_config);
                model.timeTable = timeTable;
                return View(model);
            }
        }

        // POST: TimeTables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateBookedRoomViewModel model)
        {
            try
            {
                TimeTable timeTable = model.timeTable;

                // TODO: Add update logic here
                string sql = $@"
                    UPDATE TimeTable
                    SET BookTime = '{timeTable.BookTime}'
                    WHERE Id = {id}";

                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);
                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return BadRequest();

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeTables/Delete/5
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            string sql = $@"
            SELECT
                t.TimeTableId,
                t.BookTime,
            FROM TimeTable t
            WHERE t.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                TimeTable timeTable = await conn.QueryFirstAsync<TimeTable>(sql);
                return View(timeTable);
            }
        }

        // POST: TimeTables/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            string sql = $@"DELETE FROM TimeTable WHERE Id = {id}";

            using (IDbConnection conn = Connection)
            {
                int rowsAffected = await conn.ExecuteAsync(sql);
                if (rowsAffected > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                throw new Exception("No rows affected");
            }
        }
    }
}
