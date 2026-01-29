using AirlineManagementSyatem.Models;
using AirlineManagementSyatem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Service
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public Task<List<Flight>> GetAllFlightsAsync()
            => _flightRepository.GetAllFlightsAsync();

        public Task<Flight> GetFlightByIdAsync(int flightId)
            => _flightRepository.GetFlightByIdAsync(flightId);

        public Task AddFlightAsync(int flightDetailsId, int depAirport,
            DateTime depDate, TimeSpan depTime,
            int arrAirport, DateTime arrDate, TimeSpan arrTime)
            => _flightRepository.AddFlightAsync(flightDetailsId, depAirport,
                                    depDate, depTime,
                                    arrAirport, arrDate, arrTime);

        public Task UpdateFlightAsync(int flightId, int flightDetailsId,
            int depAirport, DateTime depDate, TimeSpan depTime,
            int arrAirport, DateTime arrDate, TimeSpan arrTime)
            => _flightRepository.UpdateFlightAsync(flightId, flightDetailsId,
                                       depAirport, depDate, depTime,
                                       arrAirport, arrDate, arrTime);

        public Task<List<FlightDetails>> GetFlightDetailsAsync()
        {
            return _flightRepository.GetFlightDetailsAsync();
        }

    }
}
