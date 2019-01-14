using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RealRehearsalSpace.Models.ViewModels
{
    public class EditBookedRoomViewModel
    {
        public List<SelectListItem> TimeTables { get; set; }
        public TimeTable timeTable { get; set; }
        public Room room { get; set; }
        public BookedRoom bookedRoom { get; set; }

        public EditBookedRoomViewModel() { }

        public EditBookedRoomViewModel(IConfiguration config, BookedRoom currentRoom)
        {
            bookedRoom = currentRoom;
            using (IDbConnection conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                var times = conn.Query<TimeTable>($@"
                    Select tt.TimeTableId, tt.BookTime FROM TimeTables tt
                    LEFT JOIN BookedRooms br ON tt.TimeTableId = br.TimeTableId AND br.RoomId = {bookedRoom.RoomId}
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
        }
    }
}
