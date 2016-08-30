using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace SeatChecker
{
    public class Program
    {
        private static IConfigurationRoot Configuration;
        public static void Main(string[] args)
        {
            // Load configuration
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.default.json")
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            // Check seats!
            int checkIntervalSeconds = int.Parse(Configuration["SeatChecker:CheckIntervalSeconds"]);
            var crnList = Configuration.GetSection("SeatChecker:Crns").GetChildren().Select(s => s.Value);
            var seatChecker = new SeatChecker(Configuration["SeatChecker:MyPurdueUser"], Configuration["SeatChecker:MyPurduePass"]);
            while (true)
            {
                Console.WriteLine(DateTimeOffset.Now.ToString());
                foreach (var crn in crnList)
                {
                    Console.WriteLine(
                        String.Format(
                            "\t{0}: {1} seats available.",
                            crn,
                            seatChecker.CheckRemainingSeats(Configuration["SeatChecker:TermCode"], crn).Result
                        )
                    );
                }
                Thread.Sleep(checkIntervalSeconds * 1000);
            }
        }
    }
}
