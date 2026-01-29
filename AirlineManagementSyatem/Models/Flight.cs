using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string FlightName { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime ArrivedDate { get; set; }
        public TimeSpan ArrivedTime { get; set; }
    }
}
