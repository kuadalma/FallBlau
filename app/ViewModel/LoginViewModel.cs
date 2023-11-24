using app.models;
using app.services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace app.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService authService;
        
        public LoginViewModel(AuthService authService)
        {
            this.authService = authService;
        }

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string userLogin;

        [ObservableProperty]
        private string userPassword;

        [ObservableProperty]
        private string error;

        [RelayCommand]
        private async void Login()
        {
            //authService.Login();
            //await Shell.Current.GoToAsync($"//{nameof(MainPage)}?User={Id}");
            if (!string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(UserPassword))
            {
                string loginUrl = $"https://localhost:5001/api/User/{UserLogin},{UserPassword}";

                try
                {
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                    // Pass the handler to httpclient(from you are calling api)
                    HttpClient client = new HttpClient(clientHandler);
                    HttpResponseMessage response = await client.GetAsync(loginUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var user = JsonConvert.DeserializeObject<User>(responseBody);

                        if (user != null)
                        {
                            Id = user.id;
                            authService.Login();
                            await Shell.Current.GoToAsync($"//{nameof(MainPage)}?User={Id}");
                        }
                        else
                        {
                            Error = "user == null";
                        }
                    }
                    else
                    {
                        Error = $"HTTP error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                Error = "Puste pole";
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }

        }


        [RelayCommand]
        private async void Register()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
    }
}
