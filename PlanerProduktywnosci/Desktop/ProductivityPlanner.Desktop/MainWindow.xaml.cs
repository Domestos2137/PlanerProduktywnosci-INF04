using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

    var json = JsonConvert.SerializeObject(newTask);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    try
    {
        // Upewnij się, że token jest w nagłówku (jeśli go tam jeszcze nie ma)
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

        var response = await _client.PostAsync("http://localhost:5290/api/tasks", content);

        if (response.IsSuccessStatusCode)
        {
            txtNewTaskTitle.Clear(); // Czyścimy pole
            await LoadTasksFromApi(); // Odświeżamy listę, żeby zobaczyć nowe zadanie
        }
        else
        {
            MessageBox.Show("Błąd podczas dodawania: " + response.ReasonPhrase);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Błąd połączenia: " + ex.Message);
    }
}