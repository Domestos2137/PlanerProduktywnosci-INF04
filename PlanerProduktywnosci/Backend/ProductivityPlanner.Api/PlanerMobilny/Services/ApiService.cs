using System.Net.Http.Json;
using PlanerMobilny.Models; // Dopasuj do swojej przestrzeni nazw

namespace PlanerMobilny.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    private readonly string _baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                                       ? "http://10.0.2.2:5000/api"
                                       : "http://localhost:5000/api";

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/auth/login", new { Email = email, Password = password });
        return response.IsSuccessStatusCode;
    }

    public async Task<List<TodoTask>> GetTasksAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TodoTask>>($"{_baseUrl}/tasks") ?? new();
    }

    public async Task<bool> UpdateTaskAsync(TodoTask task)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/tasks/{task.Id}", task);
        return response.IsSuccessStatusCode;
    }
}