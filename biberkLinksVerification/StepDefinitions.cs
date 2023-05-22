using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace biberkLinksVerification
{
    public class LinkVerifier
    {
        private HttpClient httpClient;

        public LinkVerifier()
        {
            httpClient = new HttpClient();
        }

        public async Task VerifyLink(string state)
        {
            Setup();

            HttpResponseMessage response_ = await httpClient.GetAsync($"https://www.biberk.com/policyholders/resources/employer-posting-notices/");

            // Check the response status code
            if (!response_.IsSuccessStatusCode)
            {
                string errorMessage = "Expected to see an error message that the page cannot be found";
                Console.WriteLine(errorMessage);
            }
            else
            {
                string message = "Landed On the Mainpage";
                Console.WriteLine(message);
            }

            // Send an HTTP GET request to the page URL
            HttpResponseMessage response__ = await httpClient.GetAsync($"https://www.biberk.com/policyholders/resources/employer-posting-notices/{state}");

            // Get the response content
            string responseContent = await response__.Content.ReadAsStringAsync();

            // Check if the server message is present in the response content
            if (!responseContent.Contains(state))
            {
                string serverMessage = $"Expected to see the server message: {state}";
                Console.WriteLine(serverMessage);
            }

            // Send an HTTP GET request to the dropdown option URL
            HttpResponseMessage response___ = await httpClient.GetAsync($"https://www.biberk.com/policyholders/resources/employer-posting-notices/{state}/{state}");

            // Check the response status code
            if (response___.IsSuccessStatusCode)
            {
                Console.WriteLine($"The link for dropdown option {state} is not broken.");
            }
            else
            {
                string errorMessage = $"The link for dropdown option {state} is broken.";
                Console.WriteLine(errorMessage);
            }

            TearDown();
        }

        public void Setup()
        {
            httpClient = new HttpClient();
        }

        public void TearDown()
        {
            httpClient.Dispose();
        }
    }

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var verifier = new LinkVerifier();

            List<string> states = new List<string>
            {
                "Delaware",
                "Illinois",
                "Indiana",
                "Kentucky",
                "New Jersey",
                "New Mexico",
                "Vermont",

                // Add more states as needed
            };

            foreach (string state in states)
            {
                await verifier.VerifyLink(state);
            }
        }
    }
}
