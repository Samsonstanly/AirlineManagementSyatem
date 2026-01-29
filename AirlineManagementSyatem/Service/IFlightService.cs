using AirlineManagementSyatem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Service
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int flightId);
        Task AddFlightAsync(int flightDetailsId, int depAirport,
            DateTime depDate, TimeSpan depTime,
            int arrAirport, DateTime arrDate, TimeSpan arrTime);
        Task UpdateFlightAsync(int flightId, int flightDetailsId,
            int depAirport, DateTime depDate, TimeSpan depTime,
            int arrAirport, DateTime arrDate, TimeSpan arrTime);

        Task<List<FlightDetails>> GetFlightDetailsAsync();

    }
}
