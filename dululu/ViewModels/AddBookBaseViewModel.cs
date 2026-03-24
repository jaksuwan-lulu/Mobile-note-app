using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dululu.DataServices;
using dululu.Models;
using System.Collections.ObjectModel;



namespace dululu.ViewModels
{
    public partial class AddBookBaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;
    }
}
