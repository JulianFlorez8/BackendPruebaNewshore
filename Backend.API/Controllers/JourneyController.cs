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

        /**
        * Metodo que Post de la API que recibe los parametros y llama a las de mas funciones para
        * construir el objeto y dar respuesta a la petición
        * @param inputParameters: parametros que serán recibidos
        * @returns Task<IHttpActionResult> con el objeto en caso de ser exitoso
        */

        [HttpPost]
        [Route("api/Journey/calculate")]
        public async Task<IHttpActionResult> Post(InputParameters inputParameters)
        {
            var flights = await flightService.GetAll();
            Journey journey = new Journey
            {
                Origin = inputParameters.Origin,
                Destination = inputParameters.Destination
            };
            List<Flight> result = new List<Flight>();
            SearchFlightsRecursive(journey.Origin, journey.Destination, result, 0, flights, inputParameters);
            journey.Flights = result;
            journey.Price = CalculateTotalPrice(result);
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

        /**
        * Metodo que filtra los vuelos cuyo origin coincida con el origin entrante por parametro y despues 
        * realiza llamadas recursivas con estos, añadiendo los vuelos a result hasta encontrar un vuelo cuyo destination coincida con 
        * el destination entrante por parametro
        * @param origin: origen del viaje, destination: destino del viaje, result: lista de vuelos con la que se satisfaga el origin y destination.
        * numberOfFlights: número de vuelos en result flights: la lista de todos los vuelos.
        * inputParameters: objeto que contiene los parámetros que llegan por la solicitud.
        * @returns booleano que indica si ya se encontró una ruta con el origin y destination especificados
        */

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


        /**
       * Metodo que calcula el precio total del viaje, recorriendo cada vuelo y sumando su precio
       * @param flights: lista de vuelos
       * @returns double con el precio total
       */
        public double CalculateTotalPrice(List<Flight> flights)
        {
            double totalPrice = 0.0;
            foreach (var flight in flights)
            {
                totalPrice += flight.Price;
            }
            return totalPrice;
        }


        public class InputParameters
        {
            public string Origin { get; set; }
            public string Destination { get; set; }
            public int MaxFlights { get; set; }
        }

  
    }
}
