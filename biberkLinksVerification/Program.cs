namespace biberkLinksVerification
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var testClass = new StepDefinitions();
            testClass.Setup();

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
                testClass.I_am_on_the_Biberk_website();
                testClass.I_select_the_state_of(state);
                testClass.I_click_on_the_link_for(state);
                testClass.I_should_be_taken_to_the_state_employer_posting_notices_page(state);
                testClass.I_should_see_an_error_message_that_the_page_cannot_be_found();
                testClass.I_should_see_the_server_message(state);
                await testClass.I_should_check_the_dropdown_link(state); // Invoke the async method

                // Add a delay if needed before moving to the next state
                // Thread.Sleep(milliseconds);
            }

            testClass.TearDown();
        }
    }
}
