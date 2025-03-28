using System.Net.Http;
using System.Threading.Tasks;

public class RokuClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public RokuClient(string ipAddress)
    {
        _baseUrl = $"http://{ipAddress}:8060/";
        _httpClient = new HttpClient();
    }

    public async Task SendKeyPressAsync(string key)
    {
        await _httpClient.PostAsync($"{_baseUrl}keypress/{key}", null);
    }

    public async Task LaunchAppAsync(string appId)
    {
        await _httpClient.PostAsync($"{_baseUrl}launch/{appId}", null);
    }
}
