using AirlineManagementSyatem.Models;
using ClassLibraryDatabaseConnection;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly string _connectionString;

        public FlightRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Airline"].ConnectionString;
        }

        // List All Flights
        public async Task<List<Flight>> GetAllFlightsAsync()
        {
            var flights = new List<Flight>();

            await using SqlConnection connection = ConnectionManager.OpenConnection(_connectionString);

            await using SqlCommand command =
                new SqlCommand("sp_ListAllFlights", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };


            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                flights.Add(new Flight
                {
                    FlightId = Convert.ToInt32(reader["FlightId"]),
                    FlightName = reader["FlightName"].ToString(),
                    DepartureAirport = reader["DepartureAirport"].ToString(),
                    DepartureCity = reader["DepartureCity"].ToString(),
                    DepartureDate = Convert.ToDateTime(reader["DepartureDate"]),
                    DepartureTime = (TimeSpan)reader["DepartureTime"],
                    ArrivalAirport = reader["ArrivalAirport"].ToString(),
                    ArrivalCity = reader["ArrivalCity"].ToString(),
                    ArrivedDate = Convert.ToDateTime(reader["ArrivedDate"]),
                    ArrivedTime = (TimeSpan)reader["ArrivedTime"]
                });
            }

            return flights;
        }

        // Search Flight By Id
        public async Task<Flight> GetFlightByIdAsync(int flightId)
        {
            await using SqlConnection connection = ConnectionManager.OpenConnection(_connectionString);

            await using SqlCommand command = new SqlCommand("sp_SearchFlightById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

            command.Parameters.AddWithValue("@FlightId", flightId);


            await using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Flight
                {
                    FlightId = Convert.ToInt32(reader["FlightId"]),
                    FlightName = reader["FlightName"].ToString(),
                    DepartureAirport = reader["DepartureAirport"].ToString(),
                    DepartureDate = Convert.ToDateTime(reader["DepartureDate"]),
                    DepartureTime = (TimeSpan)reader["DepartureTime"],
                    ArrivalAirport = reader["ArrivalAirport"].ToString(),
                    ArrivedDate = Convert.ToDateTime(reader["ArrivedDate"]),
                    ArrivedTime = (TimeSpan)reader["ArrivedTime"]
                };
            }

            return null;
        }

        // Add Flight
        public async Task AddFlightAsync(int flightDetailsId,int departureAirport,DateTime departureDate,TimeSpan departureTime,int arrivedAirport,DateTime arrivedDate,TimeSpan arrivedTime)
        {
            await using SqlConnection connection = ConnectionManager.OpenConnection(_connectionString);

            await using SqlCommand command = new SqlCommand("sp_AddFlight", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

            command.Parameters.AddWithValue("@FlightDetailsId", flightDetailsId);
            command.Parameters.AddWithValue("@DepartureAirport", departureAirport);
            command.Parameters.Add("@DepartureDate", SqlDbType.Date).Value = departureDate;
            command.Parameters.Add("@DepartureTime", SqlDbType.Time).Value = departureTime;
            command.Parameters.AddWithValue("@ArrivedAirport", arrivedAirport);
            command.Parameters.Add("@ArrivedDate", SqlDbType.Date).Value = arrivedDate;
            command.Parameters.Add("@ArrivedTime", SqlDbType.Time).Value = arrivedTime;

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Update Flight
        public async Task UpdateFlightAsync(int flightId,int flightDetailsId,int departureAirport,DateTime departureDate,TimeSpan departureTime,int arrivedAirport,DateTime arrivedDate,TimeSpan arrivedTime)
        {
            await using SqlConnection connection = ConnectionManager.OpenConnection(_connectionString);

            await using SqlCommand command = new SqlCommand("sp_UpdateFlight", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

            command.Parameters.AddWithValue("@FlightId", flightId);
            command.Parameters.AddWithValue("@FlightDetailsId", flightDetailsId);
            command.Parameters.AddWithValue("@DepartureAirport", departureAirport);
            command.Parameters.Add("@DepartureDate", SqlDbType.Date).Value = departureDate;
            command.Parameters.Add("@DepartureTime", SqlDbType.Time).Value = departureTime;
            command.Parameters.AddWithValue("@ArrivedAirport", arrivedAirport);
            command.Parameters.Add("@ArrivedDate", SqlDbType.Date).Value = arrivedDate;
            command.Parameters.Add("@ArrivedTime", SqlDbType.Time).Value = arrivedTime;

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<FlightDetails>> GetFlightDetailsAsync()
        {
            var list = new List<FlightDetails>();

            using var conn = ConnectionManager.OpenConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetFlightDetails", conn)
            { CommandType = CommandType.StoredProcedure };

            
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new FlightDetails
                {
                    FlightDetailsId = (int)reader["FlightDetailsId"],
                    FlightName = reader["FlightName"].ToString()
                });
            }

            return list;
        }

    }
}
