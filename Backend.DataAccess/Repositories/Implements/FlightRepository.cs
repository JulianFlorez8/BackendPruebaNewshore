
using Backend.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using Backend.DataAccess.Models;

namespace Backend.DataAccess.Repositories.Implements
{
    
    public class FlightRepository : IFlightRepository
    {
        private readonly BackContext backContext;
        HttpClient client = new HttpClient();
        public FlightRepository(BackContext backContext)
        {
            this.backContext = backContext;
            
        }
        public class Result
        {
            public string departureStation { get; set; }
            public string arrivalStation { get; set; }
            public string flightCarrier { get; set; }
            public string flightNumber { get; set; }
            public double price { get; set; }
        }

        /**
         * Metodo el cual consume la API de vuelos y me los obtiene todos Problema 2 y lo devuelve en forma del 
         * modelo definido Problema 1
         * @param ninguno
         * @returns lista de vuelos
         **/
        public async Task<IEnumerable<Flight>> GetAll()
        {
            //return await backContext.Set<Flight>().ToListAsync();
            List<Flight> result = new List<Flight>();
            List<Flight> preResult = new List<Flight>();
            List<Result> resultInitial = new List<Result>(); 
            client.BaseAddress= new Uri("https://recruiting-api.newshore.es/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));

            HttpResponseMessage response =  client.GetAsync("flights/2").Result;
            System.Console.WriteLine(response.IsSuccessStatusCode);
            System.Console.WriteLine(response.Content);
            if (response.IsSuccessStatusCode)
            {
                resultInitial =  response.Content.ReadAsAsync<List<Result>>().Result;
                var va = 0;
                foreach( var flight in resultInitial)
                {
                    Flight newFlight= new Flight();
                    Transport newTransport= new Transport();
                    newTransport.FlightCarrier= flight.flightCarrier;
                    newTransport.FlightNumber = flight.flightNumber;
                    newFlight.Transport = newTransport;
                    newFlight.Origin = flight.departureStation;
                    newFlight.Destination = flight.arrivalStation;
                    newFlight.Price = flight.price;
                    preResult.Insert(va, newFlight);
                    va++;
                }
                IList<Flight> iPreResult= preResult;
                return iPreResult;
            }
            else
            {
                var resultado = response.Content.ReadAsStringAsync().Result;
                throw new Exception(resultado);
            }
            throw new Exception("Error");

        }

    }
}
