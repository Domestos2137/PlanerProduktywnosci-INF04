using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace ProductivityPlanner.Desktop
{
    public partial class LoginWindow : Window
    {
        private readonly HttpClient _client = new HttpClient();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginData = new
            {
                Username = txtUsername.Text,
                Password = txtPassword.Password
            };

            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("http://localhost:5290/api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();

                    MainWindow mainWindow = new MainWindow(token);
                    mainWindow.Show();
                    this.Close(); 
                }
                else
                {
                    lblStatus.Text = "Błędny login lub hasło!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.Message);
            }
        }
    }
}