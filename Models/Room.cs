using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealRehearsalSpace.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<BookedRoom> BookedRooms { get; set; }
    }
}
