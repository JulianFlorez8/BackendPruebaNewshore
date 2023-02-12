using Backend.DataAccess.Models;
using Backend.DataAccess.Data;
using Backend.DataAccess.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Business.Services.Implements
{
    public class JourneyService: IJourneyService
    {
        private JourneyRepository journeyRepository;
        public JourneyService(JourneyRepository journeyRepository)
        {
            this.journeyRepository = journeyRepository;
        }
        public async Task<IEnumerable<Journey>> GetAll()
        {
            return (IEnumerable<Journey>)await journeyRepository.GetAll();
        }

    }
}
