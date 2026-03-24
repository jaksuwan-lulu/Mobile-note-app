using dululu.Models;
using dululu.ViewModels;

namespace dululu.Views;

public partial class AddOrUpdateBookPage : ContentPage
{
	public AddOrUpdateBookPage(AddOrUpdateBookPageViewmodel addOrUpdateBookPageViewmodel)
	{
		InitializeComponent();
		BindingContext = addOrUpdateBookPageViewmodel;
	}   
}