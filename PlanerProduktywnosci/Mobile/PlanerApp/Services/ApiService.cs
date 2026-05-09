using System.Net.Http.Json;
using PlanerApp.Models;

namespace PlanerApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    private readonly string _baseUrl = "http://localhost:5290/api";

    public ApiService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
        _httpClient = new HttpClient(handler);
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/auth/login", new
            {
                Username = username,
                Password = password
            });

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<TodoTask>> GetTasksAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<TodoTask>>($"{_baseUrl}/tasks") ?? new();
        }
        catch
        {
            return new List<TodoTask>();
        }
    }
}