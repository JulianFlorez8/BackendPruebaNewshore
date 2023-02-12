using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Business.Services.Implements
{
    public class FlightService : IFlightService
    {
        private FlightRepository flightRepository;
        public FlightService(FlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }
        public async Task<IEnumerable<Flight>> GetAll()
        {
            return (IEnumerable < Flight >) await flightRepository.GetAll();
        }
    }
}
