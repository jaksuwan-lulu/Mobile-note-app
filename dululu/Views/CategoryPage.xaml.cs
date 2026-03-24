using dululu.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace dululu.Views;

public partial class CategoryPage : ContentPage
{
    public ObservableCollection<Models.Book> Books { get; set; }
    public ICommand SelectBookCommand { get; set; }
    

    private AddOrUpdateBookPage viewModel;
    private string catName;
    public CategoryPage()
	{
		InitializeComponent();
        SelectBookCommand = new Command<Models.Book>(SelectBookFromUi);
        Books = new ObservableCollection<Models.Book>();
       this.BindingContext = this;
        LoadBooks();
    }


    public async void SelectBookFromUi(Models.Book book)
    {
        if (book != null)
        {
            var navigationParameter = new Dictionary<string, object>
        {
            { "ViewBookDetails", book }             
        };
            await Shell.Current.GoToAsync(nameof(BookDetailsPage), navigationParameter);                 
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        string pageTitle = Shell.Current.CurrentItem.CurrentItem.CurrentItem.Title;

        if (pageTitle == "มังงะ")
            catName = "Manga";
        else if (pageTitle == "มังฮวา")
            catName = "Manhwa";
        else if (pageTitle == "นิยาย")
            catName = "Novel";
        else
            throw new NotSupportedException("Unsupported category title");

        LoadBooks();
    }

    private async void LoadBooks()
    {
        var bookService = new DataServices.BookService();
        var allBooks = await bookService.GetBooksAsync();
        var booksInCategory = allBooks.Where(x => x.Category == catName).ToList();

        Books.Clear(); // Clear existing books before adding new ones

        foreach (var book in booksInCategory)
        {
            Books.Add(book);
        }
    }
}