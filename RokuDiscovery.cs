using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class RokuDevice
{
    public string Name { get; set; }
    public string IPAddress { get; set; }
    public RokuDevice(string name, string location)
    {
        Name = name;
        IPAddress = location;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Roku: {Name} at {IPAddress}");
    }
}

public static class RokuDiscovery
{
public static async Task<List<RokuDevice>> DiscoveryAsync()
{
    var devices = new List<RokuDevice>();

    using var udpClient = new UdpClient();

    Console.WriteLine("Adding known devices");
    devices.Add(new RokuDevice("Office", "http://192.168.0.207:8060/"));

    Console.WriteLine("Starting SSDP discovery");

    //SSDP discovery message
    string request = 
        "M-SEARCH * HTTP/1.1\r\n" +
        "Host: 239.255.255.250:1900\r\n" +
        "Man: \"ssdp:discover\"\r\n" +
        "ST: roku:ecp\r\n" +
        "MX: 3\r\n\r\n";

    byte[] requestBytes = Encoding.ASCII.GetBytes(request);
    var multicastEndPoint = new IPEndPoint(IPAddress.Parse("239.255.255.250"),1900);

    // await udpClient.SendAsync(requestBytes,requestBytes.Length, multicastEndPoint);

    // Console.WriteLine("SSDP discovery message sent");

    // //set timeout
    // udpClient.Client.ReceiveTimeout = 10000;

    // try
    // {
    //     while(true)
    //     {
    //         Console.WriteLine("Waiting for response...");
    //         var result = await udpClient.ReceiveAsync();
    //         string response = Encoding.ASCII.GetString(result.Buffer);
    //         Console.WriteLine($"Received response from {result.RemoteEndPoint.Address}");
            
    //         string location = ParseLocationFromResponse(response);
    //         if (!string.IsNullOrEmpty(location))
    //         {
    //             Console.WriteLine($"Found device at {location}");
    //             string name = await GetDeviceNameAsync(location);
    //             devices.Add(new RokuDevice(name, location));
    //         }
    //     }
    // }
    // catch (SocketException)
    // {
    //     Console.WriteLine("Timeout reached, stopping SSDP discovery");
    //     // Timeout reached, stop listening for responses
    // }

    return devices;
}

    private static string ParseLocationFromResponse(string response)
    {
        foreach (var line in response.Split("\r\n"))
        {
            if (line.StartsWith("LOCATION:",StringComparison.OrdinalIgnoreCase))
            {
                return line.Substring(9).Trim();
            }
        }
        return null;
    }

    private static async Task<string> GetDeviceNameAsync(string locationUrl)
    {
        try
        {
            using var client = new HttpClient();
            var xml = await client.GetStringAsync(locationUrl);
            var start = xml.IndexOf("<friendlyName>");
            var end = xml.IndexOf("</friendlyName>");

            if (start != -1 && end != -1)
            {
                start += "<friendlyName>".Length;
                return xml.Substring(start, end - start).Trim();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching device name: {ex.Message}");
        }
        return "Unknown Roku Device";
    }
}
