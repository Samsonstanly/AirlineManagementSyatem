using AirlineManagementSyatem.Repository;
using AirlineManagementSyatem.Service;
using AirlineManagementSyatem.Utilities;
using AirlineManagementSyatem.Models;

namespace AirlineManagementSyatem
{
    public class Program
    {
        static async Task Main()
        {
            // Dependency injection
            IAdminService adminService = new AdminService(new AdminRepository());

            IFlightService flightService = new FlightService(new FlightRepository());

            // Maximum three tries
            const int maxAttempts = 3;
            int attempts = 0;
            Admin admin = null;

            // LOGIN WITH RETRY
            while (attempts < maxAttempts)
            {
                Console.Write("Username: ");
                string user = Console.ReadLine();

                // Validate username
                if (!InputValidator.IsValidUserName(user, out string userError))
                {
                    Console.WriteLine(userError);
                    attempts++;
                    continue;
                }

                Console.Write("Password: ");
                string pass = InputValidator.ReadPassword();

                // Validate password
                if (!InputValidator.IsValidPassword(pass, out string passError))
                {
                    Console.WriteLine(passError);
                    attempts++;
                    continue;
                }

                // Check login from database
                admin = await adminService.LoginAsync(user, pass);

                if (admin != null)
                {
                    break; 
                }
                Console.WriteLine("Invalid login. Try again.");
                attempts++;
            }

            // Exits if login fails
            if (admin == null)
            {
                Console.WriteLine("Too many failed attempts. Exiting...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Welcome {admin.UserName}");

            // DASHBOARD MENU
            while (true)
            {
                Console.WriteLine("\n===== DASHBOARD =====");
                Console.WriteLine("1. List Flights");
                Console.WriteLine("2. Search Flight");
                Console.WriteLine("3. Add Flight");
                Console.WriteLine("4. Update Flight");
                Console.WriteLine("5. Exit");
                Console.Write("Choose option: ");

                if (!int.TryParse(Console.ReadLine(), out int ch))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (ch)
                {
                    case 1:
                        await ListFlights(flightService);
                        break;

                    case 2:
                        await SearchFlight(flightService);
                        break;

                    case 3:
                        await AddFlight(flightService);
                        break;

                    case 4:
                        await UpdateFlight(flightService);
                        break;

                    case 5:
                        Console.WriteLine("Goodbye");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // List flights
        static async Task ListFlights(IFlightService flightService)
        {
            var flights = await flightService.GetAllFlightsAsync();

            if (flights.Count == 0)
            {
                Console.WriteLine("No flights found.");
                return;
            }

            Console.WriteLine("\n--- Flight List ---");

            int i = 1;
            flights.ForEach(f => Console.WriteLine($"{i++}. {f.FlightId} | {f.FlightName} | {f.DepartureAirport} -> {f.ArrivalAirport}"));
            Console.ReadKey();
            Console.Clear();
        }
        

        // Search flight
        static async Task SearchFlight(IFlightService flightService)
        {
            var flights = await flightService.GetAllFlightsAsync();

            if (flights.Count == 0)
            {
                Console.WriteLine("No flights available.");
                return;
            }

            Console.WriteLine("\nSelect Flight:");

            // Show flights from DB
            for (int i = 0; i < flights.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {flights[i].FlightName} ({flights[i].DepartureAirport} -> {flights[i].ArrivalAirport})");
            }

            // Validate selection
            if (!int.TryParse(Console.ReadLine(), out int choice) ||
                choice < 1 || choice > flights.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var selected = flights[choice - 1];

            // Display flight details
            Console.WriteLine("\nFlight Details:");
            Console.WriteLine($"ID: {selected.FlightId}");
            Console.WriteLine($"Route: {selected.DepartureAirport} -> {selected.ArrivalAirport}");
            Console.WriteLine($"Date: {selected.DepartureDate:d}");

            Console.ReadKey();
            Console.Clear();
        }

        // Add flight
        static async Task AddFlight(IFlightService flightService)
        {
            Console.WriteLine("\n--- Add Flight ---");

            // Get airlines from DB
            var flightDetails = await flightService.GetFlightDetailsAsync();

            Console.WriteLine("\nSelect Flight:");

            for (int i = 0; i < flightDetails.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {flightDetails[i].FlightName}");
            }

            if (!int.TryParse(Console.ReadLine(), out int pick) ||
                pick < 1 || pick > flightDetails.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            int fdId = flightDetails[pick - 1].FlightDetailsId;


            // Airport select
            Console.WriteLine("\nSelect Departure Airport:");
            Console.WriteLine("1. Indira Gandhi International Airport");
            Console.WriteLine("2. Chhatrapati Shivaji Airport");
            Console.WriteLine("3. Heathrow Airport");


            if (!int.TryParse(Console.ReadLine(), out int dep) || dep < 1 || dep > 3)
                return;

            Console.WriteLine("\nSelect Arrival Airport:");
            Console.WriteLine("1. Indira Gandhi International Airport");
            Console.WriteLine("2. Chhatrapati Shivaji Airport");
            Console.WriteLine("3. Heathrow Airport");
            ;

            if (!int.TryParse(Console.ReadLine(), out int arr) || arr < 1 || arr > 3 || arr == dep)
            {
                Console.WriteLine("Invalid airport selection.");
                return;
            }

            // Time giving
            Console.WriteLine("\nEnter Departure Time (HH:mm):");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan depTime))
            {
                Console.WriteLine("Invalid time format.");
                return;
            }

            Console.WriteLine("Enter Arrival Time (HH:mm):");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan arrTime))
            {
                Console.WriteLine("Invalid time format.");
                return;
            }

            // Save to dtabase
            await flightService.AddFlightAsync(fdId, dep, DateTime.Today,depTime,arr, DateTime.Today,arrTime);

            Console.WriteLine(" Flight added!");

            Console.ReadKey();
            Console.Clear();
        }

        // Update flight
        static async Task UpdateFlight(IFlightService flightService)
        {
            var flights = await flightService.GetAllFlightsAsync();

            if (flights.Count == 0)
                return;

            Console.WriteLine("\nSelect Flight to Update:");

            for (int i = 0; i < flights.Count; i++)
                Console.WriteLine($"{i + 1}. {flights[i].FlightName}");

            if (!int.TryParse(Console.ReadLine(), out int pick))
                return;

            int flightId = flights[pick - 1].FlightId;

            // Airport selection
            Console.WriteLine("New Departure Airport (1-3):");
            int dep = int.Parse(Console.ReadLine());

            Console.WriteLine("New Arrival Airport (1-3):");
            int arr = int.Parse(Console.ReadLine());

            // Update DB
            await flightService.UpdateFlightAsync(flightId, 1, dep, DateTime.Today,new TimeSpan(12, 0, 0),arr, DateTime.Today,new TimeSpan(16, 0, 0));

            Console.WriteLine("Flight updated!");

            Console.ReadKey();
            Console.Clear();
        }
    }
}
