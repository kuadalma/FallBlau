using app.services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        private string id = "test";

        [ObservableProperty]
        private string userLogin;

        [ObservableProperty]
        private string userPassword;

        [RelayCommand]
        private async void Login()
        {
            authService.Login();
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}?User={Id}");
        }

        [RelayCommand]
        private async void Register()
        {
            authService.Login();
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}?User={Id}");
        }
    }
}
