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

        public CreateBookedRoomViewModel() { }

        public CreateBookedRoomViewModel(IConfiguration config, Room currentRoom)
        {
            room = currentRoom;
            using (IDbConnection conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                var times = conn.Query<TimeTable>($@"
                    Select tt.TimeTableId, tt.BookTime FROM TimeTables tt
                    LEFT JOIN BookedRooms br ON tt.TimeTableId = br.TimeTableId AND br.RoomId = {room.RoomId}
                    WHERE br.BookedRoomId is NULL
                    ORDER BY tt.TimeTableId;

                ").ToList();
                TimeTables = times
                .Select(li => new SelectListItem
                {
                    Text = li.BookTime,
                    Value = li.TimeTableId.ToString()
                }).ToList();
            }
            TimeTables.Insert(0, new SelectListItem
            {
                Text = "Choose A Time To Book...",
                Value = "0"
            });
        }
    }
}
