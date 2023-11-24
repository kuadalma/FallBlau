using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using app.models;
using app.services;

namespace app.ViewModel
{
    [QueryProperty("User", "User")]
    public partial class MainViewModel : ObservableObject
    {
        private readonly AuthService authService;
        public MainViewModel(AuthService authService)
        {
            Items = new ObservableCollection<TaskModel>();
            this.authService = authService;
        }

        
        [ObservableProperty]
        string user;

        [ObservableProperty]
        ObservableCollection<TaskModel> items;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string desc;

        [ObservableProperty]
        private DateTime setDate;

        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrEmpty(Name))
                return;

            TaskModel task = new TaskModel(Name, SetDate.ToString("d"), Desc);
            Items.Add(task);
            Name = string.Empty;
            Desc = string.Empty;
            SetDate = DateTime.Today;
        }

        [RelayCommand]
        private void Remove(TaskModel task)
        {
            if (Items.Contains(task))
                Items.Remove(task);
        }

        [RelayCommand]
        private void Complete(TaskModel task)
        {
            if (Items.Contains(task))
                Items.Remove(task);
        }

        [RelayCommand]
        async Task Tap(TaskModel task)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Name={task.Name}&Desc={task.Desc}&CreateDate={task.CreateDate}&SetDate={task.SetDate}");
        }

        [RelayCommand]
        async Task LogOut()
        {
            authService.Logout();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
