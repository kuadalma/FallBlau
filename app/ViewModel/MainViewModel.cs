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
            AddToBaseAsync(Name, Desc, DateTime.Today.ToString("d"), SetDate.ToString("d"), User);
            Name = string.Empty;
            Desc = string.Empty;
            SetDate = DateTime.Today;
        }

        [RelayCommand]
        private void Remove(TaskModel task)
        {
            if (Items.Contains(task))
            {
                Items.Remove(task);
                RemoveFromBaseAsync(1);
            }
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
        //notwork HELP!!!
        public async void LoadItemsAsync()
        {
            try
            {
                Items.Clear();

                string apiUrl = $"https://localhost:5001/api/Task/{User}";
                HttpClientHandler clientHandler = new() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } };
                HttpClient client = new(clientHandler);
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var fetchedItems = JsonConvert.DeserializeObject<List<TaskModel>>(responseBody);

                    foreach (var item in fetchedItems)
                    {
                        Items.Add(item);
                    }
                }
                else
                {
                    Error = $"Failed to fetch items. Status code: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                Error = $"Error loading items: {ex.Message}";
            }
        }
        private async void AddToBaseAsync(string name, string desc, string createDate, string setDate, string userID)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(desc) && !string.IsNullOrWhiteSpace(createDate) && !string.IsNullOrWhiteSpace(setDate) && !string.IsNullOrWhiteSpace(userID))
            {
                try
                {
                    string apiUrl = $"https://localhost:5001/api/Task/{name},{desc},{createDate},{setDate},{userID}";
                    HttpClientHandler clientHandler = new() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } };
                    HttpClient client = new(clientHandler);
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);
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
        private async void RemoveFromBaseAsync(int taskid)
        {
            if (taskid != null)
            {
                try
                {
                    string apiUrl = $"https://localhost:5001/api/Task/{taskid}";
                    HttpClientHandler clientHandler = new() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } };
                    HttpClient client = new(clientHandler);
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);
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
        private async void CheackedTackAsync(string name, string desc, string createDate, string setDate, string userID)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(desc) && !string.IsNullOrWhiteSpace(createDate) && !string.IsNullOrWhiteSpace(setDate) && !string.IsNullOrWhiteSpace(userID))
            {
                try
                {
                    string apiUrl = $"https://localhost:5001/api/Task/{name},{desc},{createDate},{setDate},{userID}";
                    HttpClientHandler clientHandler = new() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } };
                    HttpClient client = new(clientHandler);
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);
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
        } //TODO
    }
}