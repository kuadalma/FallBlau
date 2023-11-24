using app.models;
using app.services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace app.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly AuthService authService;

        public RegisterViewModel(AuthService authService)
        {
            this.authService = authService;
        }

        [ObservableProperty]
        private string id = "test";

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string userLogin;

        [ObservableProperty]
        private string userPassword;

        [ObservableProperty]
        private string error;

        [RelayCommand]
        private async void Register()
        {
            //authService.Login();
            //await Shell.Current.GoToAsync($"//{nameof(LoginPage)}?User={Id}");
            if (!string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(UserPassword) && !string.IsNullOrWhiteSpace(UserName))
            {
                string loginUrl = $"https://localhost:5001/api/User/{UserName},{UserLogin},{UserPassword}";

                try
                {
                    using HttpClient client = new();
                    HttpResponseMessage response = await client.PostAsync(loginUrl,null);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<User>(responseBody);

                        if (user != null)
                        {
                            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                        }
                        else
                        {
                            Error = "user == null";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
                }
            }
            else
            {
                Error = "Puste pole";
                await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
            }
        }

        [RelayCommand]
        private async void Login()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}?User={Id}");
        }
    }
}
