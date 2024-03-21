using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

public class Program
{
    public static async Task Main(string[] args)
    {
        var client = new InsolvencyService.CIRSoapClient(InsolvencyService.CIRSoapClient.EndpointConfiguration.CIRSoap);

        try
        {
            var date = DateTime.Now;  // Example date, replace with actual date
            var court = "exampleCourt";  // Replace with actual court
            var pubType = new InsolvencyService.ArrayOfString
            {
                "PublicationType1", // Add example publication types
                "PublicationType2"
            };  // Initialize with new ArrayOfString

            using (new OperationContextScope(client.InnerChannel))
            {
                var actionHeader = MessageHeader.CreateHeader("Action", "http://schemas.xmlsoap.org/ws/2004/08/addressing", "http://www.rechtspraak.nl/namespaces/cir01/searchByDate");
                OperationContext.Current.OutgoingMessageHeaders.Add(actionHeader);

                // Add custom headers for username and password
                MessageHeader<string> usernameHeader = new MessageHeader<string>("username");
                MessageHeader<string> passwordHeader = new MessageHeader<string>("password");

                // Create header untyped
                var untypedUsernameHeader = usernameHeader.GetUntypedHeader("Username", "");
                var untypedPasswordHeader = passwordHeader.GetUntypedHeader("Password", "");

                // Add headers to the outgoing request
                OperationContext.Current.OutgoingMessageHeaders.Add(untypedUsernameHeader);
                OperationContext.Current.OutgoingMessageHeaders.Add(untypedPasswordHeader);

                // Invoke the service method
                var response = await client.searchByDateAsync(date, court, pubType);

                // Handle the response
                // Access response data from 'response.Body'
                Console.WriteLine("Response received: " + response.Body.ToString());
            }
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
