using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading;
using TechTalk.SpecFlow;

namespace biberkLinksVerification
{
    [Binding]
    public class StepDefinitions : Steps
    {
        private string selectedState;
        private string pageTitle;
        private string errorMessage;
        private string serverMessage;
        private HttpClient httpClient;

        [BeforeScenario]
        public void Setup()
        {
            httpClient = new HttpClient();
        }

        [AfterScenario]
        public void TearDown()
        {
            httpClient.Dispose();
        }

        [Given("I am on the Biberk website")]
        public void I_am_on_the_Biberk_website()
        {
        }

        [When("I select the state of (.*)")]
        public void I_select_the_state_of(string state)
        {
            selectedState = state;
            Thread.Sleep(3000);
        }

        [When("I click on the link for (.*)")]
        public void I_click_on_the_link_for(string state)
        {
        }

        [Then("I should be taken to the (.*) employer posting notices page")]
        public async Task I_should_be_taken_to_the_state_employer_posting_notices_page(string state)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"https://www.biberk.com/policyholders/resources/employer-posting-notices/{state}");

            pageTitle = response.RequestMessage.RequestUri.AbsoluteUri;
           Console.WriteLine(pageTitle.Contains(state).ToString(), $"Expected to be taken to the {state} employer posting notices page");
        }

        [Then("I should see an error message that the page cannot be found")]
        public async Task I_should_see_an_error_message_that_the_page_cannot_be_found()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"https://www.biberk.com/policyholders/resources/employer-posting-notices/");

            // Check the response status code
            if (!response.IsSuccessStatusCode)
            {
                errorMessage = "Expected to see an error message that the page cannot be found";
                Console.WriteLine(errorMessage);
            }
        }

        [Then("I should see the server message (.*)")]
        public async Task I_should_see_the_server_message(string message)
        {
            // Send an HTTP GET request to the page URL
            HttpResponseMessage response = await httpClient.GetAsync($"https://www.biberk.com/policyholders/resources/employer-posting-notices/{selectedState}");

            // Get the response content
            string responseContent = await response.Content.ReadAsStringAsync();

            // Check if the server message is present in the response content
            if (!responseContent.Contains(message))
            {
                serverMessage = $"Expected to see the server message: {message}";
                Console.WriteLine(serverMessage);
            }
        }

        [Then("I should check the dropdown link for (.*)")]
        public async Task I_should_check_the_dropdown_link(string option)
        {
            // Send an HTTP GET request to the dropdown option URL
            HttpResponseMessage response = await httpClient.GetAsync($"https://www.biberk.com/policyholders/resources/employer-posting-notices/{selectedState}/{option}");

            // Check the response status code
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"The link for dropdown option {option} is not broken.");
            }
            else
            {
                errorMessage = $"The link for dropdown option {option} is broken.";
                Console.WriteLine(errorMessage);
            }
        }
    }
}
