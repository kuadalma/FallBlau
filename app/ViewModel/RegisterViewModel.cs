using app.services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

        [RelayCommand]
        private async void Register()
        {
            authService.Login();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}?User={Id}");
        }

        [RelayCommand]
        private async void Login()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}?User={Id}");
        }
    }
}
