using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers; 

namespace ProductivityPlanner.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly string _token;
        private readonly HttpClient _client = new HttpClient();

        public MainWindow(string token)
        {
            InitializeComponent();
            _token = token; 

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTasksFromApi();
        }

        private async Task LoadTasksFromApi()
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);

                var response = await _client.GetStringAsync("http://localhost:5290/api/tasks");

                var tasks = JsonConvert.DeserializeObject<List<TaskItem>>(response);

                dgTasks.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się pobrać zadań: " + ex.Message);
            }
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public bool IsCompleted { get; set; }
    }
}