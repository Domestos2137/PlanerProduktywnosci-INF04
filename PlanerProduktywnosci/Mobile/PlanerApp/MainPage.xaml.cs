using PlanerApp.Services;

namespace PlanerApp;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;

    public MainPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        SetLoadingState(true);

        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Błąd", "Wprowadź login i hasło.", "OK");
            SetLoadingState(false);
            return;
        }

        try
        {
            bool isSuccess = await _apiService.LoginAsync(email, password);

            if (isSuccess)
            {
                await DisplayAlert("Sukces", "Zalogowano pomyślnie!", "OK");
            }
            else
            {
                await DisplayAlert("Błąd logowania", "Nieprawidłowe dane lub serwer nie odpowiada.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Błąd krytyczny", $"Coś poszło nie tak: {ex.Message}", "OK");
        }
        finally
        {
            SetLoadingState(false);
        }
    }

    private void SetLoadingState(bool isLoading)
    {
        LoginButton.IsEnabled = !isLoading;
        LoadingSpinner.IsVisible = isLoading;

        LoginButton.IsVisible = !isLoading;
    }
}