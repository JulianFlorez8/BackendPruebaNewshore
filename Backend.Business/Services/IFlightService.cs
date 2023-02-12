using Backend.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Business.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAll();

    }
}
