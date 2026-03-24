using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dululu.DataServices;
using dululu.Models;
using dululu.Views;
using MvvmHelpers;
using System.Windows.Input;
namespace dululu.ViewModels
{
    public partial class BooklistHomePageViewmodel : AddBookBaseViewModel
    {
        
        [ObservableProperty]
        private bool _gridVisibility;
        private readonly LBookService bookService;
        public ObservableRangeCollection<Book> Books { get; set; } = new();
        public string Image { get; set; }

        

        public BooklistHomePageViewmodel(LBookService bookService)
        {
            this.bookService = bookService;
            Title = "DULULU Book List";
           
        }

        

        [RelayCommand]
        private async Task LoadBookFromDatabase()
        {
            GridVisibility = false;
            await Task.Delay(1000);
            var results = await bookService.GetBooksAsync();
            if (results.Count > 0)
            {
                Books.Clear();

                foreach (var book in results)
                {
                    string subString = book.Description.Length > 30 ? book.Description.Substring(0, 30) + "..." : book.Description;
                    Books.Add(new Book() { Id = book.Id, Title = book.Title, Description = subString, Image = book.Image });
                }
            }
            GridVisibility = true;
        }

        [RelayCommand]
        private async Task NavigateToAddBookPage() => await Shell.Current.GoToAsync(nameof(AddOrUpdateBookPage), true);

        [RelayCommand]
        private async Task NavigateToDetails(Book bookModel)
        {
            if (bookModel is null) return;

            var desc = await bookService.GetBookAsync(bookModel.Id);

            

            var navigationParameter = new Dictionary<string, object>();
            navigationParameter.Add("ViewBookDetails", desc);
            await Shell.Current.GoToAsync(nameof(BookDetailsPage), navigationParameter);
            
        }

        [RelayCommand]
        private async Task DeleteBookData(Book bookToBeDeleted)
        {
            bool answer = await Shell.Current.DisplayAlert("Confirm Delete?", $"Are you sure you wanna delete {bookToBeDeleted.Title}?", "Yes", "No");
            if (answer)
            {
                var result = await bookService.DeleteBookAsync(bookToBeDeleted);
                MakeToast(result.Message);
                await LoadBookFromDatabase();
            }
        }

        [RelayCommand]
        private async Task UpdateBookData(Book bookToUpdated)
        {
            bool answer = await Shell.Current.DisplayAlert("Confirm Update?", $"Are you sure you wanna update: {bookToUpdated.Title} ?", "Yes", "No");
            if (answer)
            {
                var navigationParameter = new Dictionary<string, object>();
                navigationParameter.Add("UpdateBookData", bookToUpdated);
                await Shell.Current.GoToAsync(nameof(AddOrUpdateBookPage), navigationParameter);
            }
        }

        private async Task LoadBooksByCategoryAsync(BookCategory category)
        {
            // โหลดหนังสือจากฐานข้อมูลตามประเภทที่เลือก
            var results = await bookService.GetBooksByCategoryAsync(category);
            // โค้ดอื่น ๆ เพื่อแสดงรายการหนังสือ
        }

        private static async void MakeToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 15;
            var toast = Toast.Make(message, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }

        public async Task SearchBooks(string searchText)
        {
            // ถ้าช่องค้นหาว่างเปล่า ก็โหลดหนังสือทั้งหมดอีกครั้ง
            if (string.IsNullOrWhiteSpace(searchText))
            {
                await LoadBookFromDatabase();
                return;
            }

            // ทำการค้นหาโดยใช้ข้อความที่ป้อนเข้ามา
            var searchResults = Books.Where(book => book.Title.ToLower().Contains(searchText.ToLower()) || book.Description.ToLower().Contains(searchText.ToLower())).ToList();

            // ตรวจสอบว่ามีผลลัพธ์หรือไม่และดำเนินการต่อตามความเหมาะสม
            if (searchResults.Any())
            {
                // แสดงรายการหนังสือที่ค้นพบ
                Books.ReplaceRange(searchResults);
            }
            else
            {
                // ถ้าไม่พบผลลัพธ์ โปรดโหลดหนังสือทั้งหมด
                await LoadBookFromDatabase();
            }
        }


    }
}
