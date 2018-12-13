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

        public CreateBookedRoomViewModel(IConfiguration config)
        {
            using (IDbConnection conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                TimeTables = conn.Query<TimeTable>(@"
                    SELECT TimeTableId, BookTime FROM TimeTables;
                ")
                .AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.BookTime,
                    Value = li.TimeTableId.ToString()
                }).ToList();
                ;
            }
            TimeTables.Insert(0, new SelectListItem
            {
                Text = "Choose time...",
                Value = "0"
            });
        }
    }
}
