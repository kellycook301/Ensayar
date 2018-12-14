using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using RealRehearsalSpace.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace RealRehearsalSpace.Models.ViewModels
{
    public class CreateBookedRoomViewModel
    {
        public List<SelectListItem> TimeTables { get; set; }
        public TimeTable timeTable { get; set; }
        public Room room { get; set; }

        public CreateBookedRoomViewModel(){ }

        public CreateBookedRoomViewModel(IConfiguration config, Room currentRoom)
        {
            room = currentRoom;
            using (IDbConnection conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                var times = conn.Query<TimeTable>(@"
                    SELECT TimeTableId, BookTime FROM TimeTables;
                ").ToList();
                var bookedRoomTimes = conn.Query<BookedRoom>($@"
                    SELECT TimeTableId FROM BookedRooms WHERE RoomId = {room.RoomId};
                ").ToList();

                foreach (BookedRoom br in bookedRoomTimes)
                {
                    times.Remove(br.TimeTable);
                };
                TimeTables = times.AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.BookTime,
                    Value = li.TimeTableId.ToString()
                }).ToList();
                ;
            }
            TimeTables.Insert(0, new SelectListItem
            {
                Text = "Choose A Time To Book...",
                Value = "0"
            });
        }
    }
}
