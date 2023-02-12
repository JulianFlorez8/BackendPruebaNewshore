using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Business.Models
{
  
    [Table("Transport", Schema = "dbo")]
    public class Transport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransportID { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
        

    }
}
