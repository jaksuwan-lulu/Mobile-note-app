using Xamarin.Essentials;
using dululu.ViewModels;
namespace dululu.Views;

public partial class BookDetailsPage : ContentPage
{
    public BookDetailsPage()
    {
    }

    public BookDetailsPage(BookDetailsPageViewmodel bookDetailsPageViewmodel)
	{
		InitializeComponent();
		BindingContext = bookDetailsPageViewmodel;
	}

    async void btnSharePage_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {           
            string urlToShare = "https://www.example.com/bookdetail?id=123";
            
            await Microsoft.Maui.ApplicationModel.DataTransfer.Share.RequestAsync(new Microsoft.Maui.ApplicationModel.DataTransfer.ShareTextRequest
            {
                Text = urlToShare,
                Title = "Share Book Detail"
            });
        }
        catch (Exception ex)
        {
            // ????????????????????????????????????????????????
        }
    }


}