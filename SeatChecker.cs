using System;
using System.Net.Http;
using System.Net.Http.Headers;
using SeatChecker.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace SeatChecker
{
    /// <summary>
    /// Class used to query Purdue.io for up-to-date CRN information
    /// </summary>
    class SeatChecker
    {
        public readonly static string ApiBaseUrl = "https://api.purdue.io";
        public readonly static string ApiCrnPath = "/Student/Crn/{0}/{1}";
        private string username;
        private string password;

        public SeatChecker(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Fetches Section object from Purdue.io API
        /// </summary>
        /// <param name="termCode">MyPurdue term code (e.g. 201710 for Fall 2016)</param>
        /// <param name="crn">MyPurdue CRN</param>
        /// <returns>Section object</returns>
        public async Task<Section> FetchSection(string termCode, string crn)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                var base64AuthString = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64AuthString);
                var request = await client.GetAsync(String.Format(ApiCrnPath, termCode, crn));
                var content = await request.Content.ReadAsStringAsync();
                if (request.IsSuccessStatusCode) {
                    var section = JsonConvert.DeserializeObject<Section>(content);
                    return section;
                }
                else
                {
                    throw new Exception("Could not retrieve section: " + await request.Content.ReadAsStringAsync());
                }
            }
        }

        /// <summary>
        /// Queries Purdue.io and returns the number of remaining seats for the given term and CRN.
        /// </summary>
        /// <param name="termCode">MyPurdue term code (e.g. 201710 for Fall 2016)</param>
        /// <param name="crn">MyPurdue CRN</param>
        /// <returns>Number of remaining seats</returns>
        public async Task<int> CheckRemainingSeats(string termCode, string crn)
        {
            var section = await FetchSection(termCode, crn);
            return section.RemainingSpace;
        }
    }
}