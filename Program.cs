using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting Program...");

        RokuDevice study = new RokuDevice("Study", "http://192.168.0.207:8060/");
        RokuDevice lovingroom = new RokuDevice("Living Room", "http://192.168.0.235:8060/");

        study.PrintInfo();
        lovingroom.PrintInfo();

        Console.WriteLine("DONE!");

        var devices = await RokuDiscovery.DiscoveryAsync();
        foreach (var device in devices)
        {
            device.PrintInfo();
        }
    }
}