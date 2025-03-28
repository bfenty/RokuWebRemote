using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting Program...");

        try
        {
            List<RokuDevice> devices = await RokuDiscovery.DiscoveryAsync();
            foreach (var device in devices)
            {
                device.PrintInfo();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during discovery: {ex.Message}");
        }
    }
}