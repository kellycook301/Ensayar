using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealRehearsalSpace.Models;

namespace RealRehearsalSpace.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BookedRoom> BookedRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }

        internal void Add(object thisBookedRoom)
        {
            throw new NotImplementedException();
        }
    }
}
