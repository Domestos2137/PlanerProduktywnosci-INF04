using PlanerApp.Models;
using PlanerApp.Services;

namespace PlanerApp;

public partial class TodayPage : ContentPage
{
    private readonly ApiService _apiService = new ApiService();

    public TodayPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadTasks();
    }

    private async Task LoadTasks()
    {
        var tasks = await _apiService.GetTasksAsync();

        TasksListView.ItemsSource = tasks;
        CountLabel.Text = tasks.Count(t => !t.IsDone).ToString();
    }

    private async void OnRefreshRequested(object sender, EventArgs e)
    {
        await LoadTasks();
        RefreshTasks.IsRefreshing = false;
    }

    private async void OnTaskStatusChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = (CheckBox)sender;
        var task = (TodoTask)checkbox.BindingContext;

        if (task != null)
        {
            bool success = await _apiService.UpdateTaskAsync(task);

            if (!success)
            {
                await DisplayAlert("B³¹d", "Nie uda³o siê zaktualizowaæ zadania", "OK");
            }

            var tasks = (List<TodoTask>)TasksListView.ItemsSource;
            CountLabel.Text = tasks.Count(t => !t.IsDone).ToString();
        }
    }
    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTaskPage());
    }
}