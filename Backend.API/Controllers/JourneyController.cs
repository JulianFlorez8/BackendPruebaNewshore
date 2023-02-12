using Backend.Business.Services.Implements;
using Backend.DataAccess.Data;
using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories.Implements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using static Backend.API.Controllers.JourneyController;

namespace Backend.API.Controllers
{
    
    [Route("api/Journey")]
    public class JourneyController : ApiController
    {
        
        private readonly JourneyService journeyService = new JourneyService(new JourneyRepository(BackContext.Create()));
        private readonly FlightService flightService = new FlightService(new FlightRepository(BackContext.Create()));
        private double totalPrice = 0.0;


        [HttpPost]
        [Route("api/Journey/calculate")]
        public async Task<IHttpActionResult> Post(InputParameters inputParameters)
        {
            var flights = await flightService.GetAll();
            Journey journey = new Journey();
            journey.Origin = inputParameters.Origin;
            journey.Destination = inputParameters.Destination;
            journey.Price = totalPrice;
            List<Flight> result = new List<Flight>();
            SearchFlightsRecursive(journey.Origin, journey.Destination, result, 0, flights, inputParameters);
            journey.Flights = result;
            return Ok(journey);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetJourney(InputParameters inputParameters)
        {
            var flights = await flightService.GetAll();
            Journey journey = new Journey
            {
                Origin = inputParameters.Origin,
                Destination = inputParameters.Destination
            };
            List<Flight> result = new List<Flight>();
            SearchFlightsRecursive(journey.Origin, journey.Destination, result, 0, flights, inputParameters);
            journey.Price = totalPrice;
            journey.Flights= result;
            return Ok(journey);
        }


        public bool SearchFlightsRecursive(string origin, string destination, List<Flight> result, int numberOfFlights, IEnumerable<Flight> flights, InputParameters inputParameters)
        {
            if (numberOfFlights >= 3 || numberOfFlights >= inputParameters.MaxFlights)
            {
                return false;
            }

            var filteredflights = flights.Where(f => f.Origin == origin);

            foreach (var flight in filteredflights) {

                result.Add(flight);
                if (flight.Destination == destination)
                {
                    return true;
                }

                if (SearchFlightsRecursive(flight.Destination, destination, result, numberOfFlights + 1, flights, inputParameters))
                {
                    return true;
                }
                result.Remove(flight);
            }

            return false;
        }


        public class InputParameters
        {
            public string Origin { get; set; }
            public string Destination { get; set; }
            public int MaxFlights { get; set; }
        }

  
    }
}
