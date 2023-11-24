using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using app.models;
using app.services;
using Newtonsoft.Json;

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

        [ObservableProperty]
        private string error;

        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrEmpty(Name))
                return;

            TaskModel task = new TaskModel(Name, SetDate.ToString("d"), Desc);
            Items.Add(task);
            //AddToBaseAsync(Name, Desc, DateTime.Today.ToString("d"), SetDate.ToString("d"), User);
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

        private async void AddToBaseAsync(string name, string desc, string createDate, string setDate, string userID)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(desc) && !string.IsNullOrWhiteSpace(createDate) && !string.IsNullOrWhiteSpace(setDate) && !string.IsNullOrWhiteSpace(userID))
            {
                string loginUrl = $"https://localhost:5001/api/Task/{name},{desc},{createDate},{setDate},{userID}";

                try
                {
                    using HttpClient client = new();
                    HttpResponseMessage response = await client.PostAsync(loginUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var item = JsonConvert.DeserializeObject<User>(responseBody);

                        if (item != null)
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
                }
            }
            else
            {
                Error = "Puste pole";
            }
        }
    }
}
