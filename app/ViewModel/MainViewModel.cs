using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace app.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Items = new ObservableCollection<string>();
        }

        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        private string text;

        [RelayCommand]
        void Add()
        {
            if(string.IsNullOrEmpty(Text))
                return;
            Items.Add(Text);
            Text = string.Empty;
        }
    }
}
