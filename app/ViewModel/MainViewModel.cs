using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using app.models;

namespace app.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Items = new ObservableCollection<TaskModel>();
        }

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
        void Remove(TaskModel task)
        {
            if (Items.Contains(task))
                Items.Remove(task);
        }

        [RelayCommand]
        void Complete(TaskModel task)
        {
            if (Items.Contains(task))
                Items.Remove(task);
        }

        [RelayCommand]
        async Task Tap(TaskModel task)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Name={task.Name}&Desc={task.Desc}&CreateDate={task.CreateDate}&SetDate={task.SetDate}");
        }
    }
}
