using PlanerApp.Models;
using PlanerApp.Services;

namespace PlanerApp;

public partial class AddTaskPage : ContentPage
{
    private readonly ApiService _apiService = new ApiService();

    public AddTaskPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlert("B³¹d", "Tytu³ jest wymagany!", "OK");
            return;
        }

        var newTask = new TodoTask
        {
            Title = TitleEntry.Text,
            Description = DescriptionEditor.Text,
            Category = CategoryEntry.Text,
            DueDate = DueDatePicker.Date,
            Status = "Nowe"
        };

        bool success = await _apiService.AddTaskAsync(newTask);

        if (success)
        {
            await Navigation.PopAsync(); 
        }
        else
        {
            await DisplayAlert("B³¹d", "Nie uda³o siê zapisaæ zadania", "OK");
        }
    }
}