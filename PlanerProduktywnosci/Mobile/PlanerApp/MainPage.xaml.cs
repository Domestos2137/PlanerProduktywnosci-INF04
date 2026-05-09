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

        // Pobieramy tekst z UsernameEntry
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Błąd", "Wprowadź nazwę użytkownika i hasło.", "OK");
            SetLoadingState(false);
            return;
        }

        try
        {
            bool isSuccess = await _apiService.LoginAsync(username, password);

            if (isSuccess)
            {
                await DisplayAlert("Sukces", "Witaj w planerze!", "OK");
                Application.Current.MainPage = new NavigationPage(new TodayPage());
            }
            else
            {
                await DisplayAlert("Błąd logowania", "Niepoprawny użytkownik lub hasło.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Błąd połączenia", "Nie udało się skontaktować z API.", "OK");
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