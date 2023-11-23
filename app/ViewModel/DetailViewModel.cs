using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace app.ViewModel
{
    [QueryProperty("Name", "Name")]
    [QueryProperty("Desc", "Desc")]
    [QueryProperty("CreateDate", "CreateDate")]
    [QueryProperty("SetDate", "SetDate")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        string name;

        [ObservableProperty]
        string desc;

        [ObservableProperty]
        string createDate;

        [ObservableProperty]
        string setDate;

        [RelayCommand]
        static async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
