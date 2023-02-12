using Backend.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories
{
    public interface IJourneyRepository
    {
        Task<IEnumerable<Journey>> GetAll();
      
    }
}
