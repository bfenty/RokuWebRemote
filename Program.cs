using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting Program..."); // Log indicating the start of the program

        try
        {
            List<RokuDevice> devices = await RokuDiscovery.DiscoveryAsync(); // Discover Roku devices asynchronously
            foreach (var device in devices)
            {
                device.PrintInfo(); // Print information for each discovered device
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during discovery: {ex.Message}"); // Log any errors that occur during discovery
        }
    }
}
