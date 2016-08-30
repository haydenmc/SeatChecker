using System;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace SeatChecker
{
    /// <summary>
    /// SeatChecker main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Configuration instance used to access config values
        /// </summary>
        private static IConfigurationRoot Configuration;

        public static void Main(string[] args)
        {
            // Load configuration from json files
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.default.json")
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            // Load some config values
            int checkIntervalSeconds = int.Parse(Configuration["SeatChecker:CheckIntervalSeconds"]);
            var crnList = Configuration.GetSection("SeatChecker:Crns").GetChildren().Select(s => s.Value);

            // Check seats in a loop
            var seatChecker = new SeatChecker(Configuration["SeatChecker:MyPurdueUser"], Configuration["SeatChecker:MyPurduePass"]);
            while (true)
            {
                Console.WriteLine(DateTimeOffset.Now.ToString());
                foreach (var crn in crnList)
                {
                    var remainingSeats = seatChecker.CheckRemainingSeats(Configuration["SeatChecker:TermCode"], crn).Result;
                    Console.WriteLine(
                        String.Format(
                            "\t{0}: {1} seats available.",
                            crn,
                            remainingSeats
                        )
                    );
                }
                Thread.Sleep(checkIntervalSeconds * 1000);
            }
        }
    }
}
