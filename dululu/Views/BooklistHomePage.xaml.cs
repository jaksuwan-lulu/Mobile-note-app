using dululu.ViewModels;
namespace dululu.Views;

public partial class BooklistHomePage : ContentPage
{
    private readonly BooklistHomePageViewmodel booklistHomePageViewModel;

    public BooklistHomePage(BooklistHomePageViewmodel booklistHomePageViewModel)
	{
		InitializeComponent();
        BindingContext = booklistHomePageViewModel;
        this.booklistHomePageViewModel = booklistHomePageViewModel;
    }

    protected override void OnAppearing()
    {
        booklistHomePageViewModel.LoadBookFromDatabaseCommand.Execute(this);
    }

    private async void OnSearchCompleted(object sender, EventArgs e)
    {
        string searchText = searchEntry.Text;

        // ทำการค้นหา
        await ((BooklistHomePageViewmodel)BindingContext).SearchBooks(searchText);
    }

}