using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealRehearsalSpace.Models
{
    public class TimeTable
    {
        [Key]
        public int TimetableId { get; set; }

        [Required]
        public string BookTime { get; set; }
    }
}
