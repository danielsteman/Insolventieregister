using System;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Instantiate the service client
        var client = new InsolvencyService.CIRSoapClient(InsolvencyService.CIRSoapClient.EndpointConfiguration.CIRSoap);

        try
        {
            var date = DateTime.Now;  // Example date, replace with actual date
            var court = "exampleCourt";  // Replace with actual court
            var pubType = new InsolvencyService.ArrayOfString();  // Initialize with new ArrayOfString

            // Invoke the service method
            var response = await client.searchByDateAsync(date, court, pubType);

            // Handle the response
            // Access response data from 'response.Body'
            Console.WriteLine("Response received: " + response.Body.ToString());
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            // Close the client to free up resources
            client.Close();
        }
    }
}
