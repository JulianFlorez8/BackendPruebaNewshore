using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Models
{
    [Table("Journey", Schema = "dbo")]
    public class Journey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JourneyId { get; set; }
        public IEnumerable<Flight> Flights { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }


    }
}
