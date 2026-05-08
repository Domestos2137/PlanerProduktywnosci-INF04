using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

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

            // Autoryzacja dla wszystkich przyszłych zapytań tego klienta
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTasksFromApi();
        }

        public async Task LoadTasksFromApi()
        {
            try
            {
                var response = await _client.GetStringAsync("http://localhost:5290/api/tasks");
                var tasks = JsonConvert.DeserializeObject<List<TaskItem>>(response);
                dgTasks.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd ładowania: " + ex.Message);
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewTaskTitle.Text))
            {
                MessageBox.Show("Wpisz tytuł zadania!");
                return;
            }

            var newTask = new
            {
                Title = txtNewTaskTitle.Text,
                Category = (cmbCategory.SelectedItem as ComboBoxItem)?.Content.ToString(),
                IsCompleted = false
            };

            try
            {
                var json = JsonConvert.SerializeObject(newTask);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("http://localhost:5290/api/tasks", content);

                if (response.IsSuccessStatusCode)
                {
                    txtNewTaskTitle.Clear();
                    await LoadTasksFromApi(); // Odśwież listę
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd dodawania: " + ex.Message);
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