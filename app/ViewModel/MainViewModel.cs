using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace app.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;
        public MainViewModel(IConnectivity connectivity)
        {
            Items = new ObservableCollection<string>();
            this.connectivity = connectivity;
        }

        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        private string text;

        [RelayCommand]
        async Task Add()
        {
            if(string.IsNullOrEmpty(Text))
                return;

            if (connectivity.NetworkAccess != NetworkAccess.Internet) 
            {
                await Shell.Current.DisplayAlert("No Internet", "You need to be connected to the internet to add items", "OK");
                return;
            }
            Items.Add(Text);
            Text = string.Empty;
        }

        [RelayCommand]
        void Remove(string s)
        {
            if (Items.Contains(s))
                Items.Remove(s);
        }

        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
        }

    }
}
