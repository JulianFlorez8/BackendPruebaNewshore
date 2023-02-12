using Backend.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Data
{
    public class BackContext : DbContext
    {
        private static BackContext backContext = null;
        public BackContext() : base("BackContext")
        {

        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Transport> Transports { get; set; }

        public static BackContext Create()
        {
            if (backContext == null)
                return new BackContext();
            return backContext;
        }


    }
}
