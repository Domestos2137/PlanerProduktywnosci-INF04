using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
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
                var tasks = JsonConvert.DeserializeObject<List<TodoTask>>(response);
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
                MessageBox.Show("Tytuł zadania jest wymagany!", "Błąd walidacji");
                return;
            }

            var newTask = new TodoTask
            {
                Title = txtNewTaskTitle.Text,
                Description = txtDescription.Text,
                CreatedAt = DateTime.Now,
                DueDate = dpDueDate.SelectedDate,
                Status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Nowe",
                Priority = (cmbPriority.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Normalny",
                Category = (cmbCategory.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            try
            {
                var json = JsonConvert.SerializeObject(newTask);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("http://localhost:5290/api/tasks", content);

                if (response.IsSuccessStatusCode)
                {
                    txtNewTaskTitle.Clear();
                    txtDescription.Clear();
                    dpDueDate.SelectedDate = null;
                    await LoadTasksFromApi();
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Błąd API: {err}");
                }
            }
            catch (Exception ex) { MessageBox.Show("Błąd: " + ex.Message); }
        }

        private void dgTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTasks.SelectedItem is TodoTask selected)
            {
                txtNewTaskTitle.Text = selected.Title;
                txtDescription.Text = selected.Description;
                dpDueDate.SelectedDate = selected.DueDate;
                cmbCategory.Text = selected.Category;
                cmbStatus.Text = selected.Status;
                cmbPriority.Text = selected.Priority;
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgTasks.SelectedItem is TodoTask selected)
            {
                if (string.IsNullOrWhiteSpace(txtNewTaskTitle.Text))
                {
                    MessageBox.Show("Tytuł nie może być pusty!", "Błąd walidacji");
                    return;
                }

                selected.Title = txtNewTaskTitle.Text;
                selected.Description = txtDescription.Text;
                selected.DueDate = dpDueDate.SelectedDate;
                selected.Category = cmbCategory.Text;
                selected.Status = cmbStatus.Text;
                selected.Priority = cmbPriority.Text;

                try
                {
                    var json = JsonConvert.SerializeObject(selected);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PutAsync($"http://localhost:5290/api/tasks/{selected.Id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Zaktualizowano pomyślnie!", "Sukces");
                        await LoadTasksFromApi();
                    }
                    else
                    {
                        var err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"API odrzuciło zmiany: {err}");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Błąd: " + ex.Message); }
            }
            else
            {
                MessageBox.Show("Wybierz zadanie z listy do edycji.");
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgTasks.SelectedItem is TodoTask selected)
            {
                var result = MessageBox.Show($"Usunąć '{selected.Title}'?", "Potwierdź", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var response = await _client.DeleteAsync($"http://localhost:5290/api/tasks/{selected.Id}");
                    if (response.IsSuccessStatusCode) await LoadTasksFromApi();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtSearch.Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(dgTasks.ItemsSource);
            if (view != null)
            {
                view.Filter = item => {
                    var task = item as TodoTask;
                    return task != null && task.Title.ToLower().Contains(filter);
                };
            }
        }

        private void btnExportCSV_Click(object sender, RoutedEventArgs e)
        {
            var tasks = dgTasks.ItemsSource as List<TodoTask>;
            if (tasks == null || tasks.Count == 0) return;

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Plik CSV (*.csv)|*.csv",
                FileName = $"Zadania_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (saveDialog.ShowDialog() == true)
            {
                var csv = new StringBuilder();
                csv.AppendLine("Id;Tytul;Opis;Kategoria;Priorytet;Status;Termin");

                foreach (var t in tasks)
                {
                    csv.AppendLine($"{t.Id};{t.Title};{t.Description};{t.Category};{t.Priority};{t.Status};{t.DueDate?.ToShortDateString()}");
                }

                File.WriteAllText(saveDialog.FileName, csv.ToString(), Encoding.UTF8);
                MessageBox.Show("Wygenerowano raport CSV!", "Sukces");
            }
        }
    }

    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
    }
}