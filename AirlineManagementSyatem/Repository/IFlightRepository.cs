using AirlineManagementSyatem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Repository
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int flightId);

        Task AddFlightAsync(
            int flightDetailsId,
            int departureAirport,
            DateTime departureDate,
            TimeSpan departureTime,
            int arrivedAirport,
            DateTime arrivedDate,
            TimeSpan arrivedTime);

        Task UpdateFlightAsync(
            int flightId,
            int flightDetailsId,
            int departureAirport,
            DateTime departureDate,
            TimeSpan departureTime,
            int arrivedAirport,
            DateTime arrivedDate,
            TimeSpan arrivedTime);

        Task<List<FlightDetails>> GetFlightDetailsAsync();

    }
}
