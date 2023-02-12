using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Models
{
    [Table("Flight", Schema = "dbo")]
    public class Flight
    {
        [ForeignKey("Transport")]
        public int TransportID { get; set; }
        public Transport Transport { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }


    }
}
