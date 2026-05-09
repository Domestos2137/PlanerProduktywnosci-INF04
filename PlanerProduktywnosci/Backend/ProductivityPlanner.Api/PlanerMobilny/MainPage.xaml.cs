using PlanerMobilny.Services;

namespace PlanerMobilny;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService = new ApiService();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        Loader.IsVisible = true;

        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        bool success = await _apiService.LoginAsync(email, password);

        Loader.IsVisible = false;

        if (success)
        {
            await Shell.Current.GoToAsync("//TaskListPage");
        }
        else
        {
            await DisplayAlert("Błąd", "Nieprawidłowe dane logowania", "OK");
        }
    }
}