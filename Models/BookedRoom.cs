using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealRehearsalSpace.Models
{
    public class BookedRoom
    {
        [Key]
        public int BookedRoomId { get; set; }

        [Required]
        [Display(Name = "Room Booked")]
        public int RoomId {get; set;}

        public Room Room { get; set; }

        [Required]
        [Display(Name = "Time Booked")]
        public int TimeTableId { get; set; }

        public TimeTable TimeTable { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Date Booked")]
        public string BookDate { get; set; }
    }
}
