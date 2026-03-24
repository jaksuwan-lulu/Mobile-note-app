using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dululu.Models;


namespace dululu.ViewModels
{
    [QueryProperty(nameof(BookModel), "ViewBookDetails")]
    public partial class BookDetailsPageViewmodel : AddBookBaseViewModel
    {
        private BookCategory _selectedCategory;
        public BookCategory SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }

        public object NavigateToDetailsCommand { get; internal set; }

        [ObservableProperty]
        private Book _bookModel;


        public BookDetailsPageViewmodel()
        {
            // ดึงค่า SelectedCategory จาก ViewModel ของหน้าก่อนหน้านี้
            MessagingCenter.Subscribe<AddOrUpdateBookPageViewmodel, BookCategory>(this, "SelectedCategoryMessage", (sender, category) =>
            {
                SelectedCategory = category;
            });
        }        

        // ไม่ลืมใช้ Unsubscribe จาก MessagingCenter เมื่อไม่ได้ใช้งานอีกต่อไป
        ~BookDetailsPageViewmodel()
        {
            MessagingCenter.Unsubscribe<AddOrUpdateBookPageViewmodel, BookCategory>(this, "SelectedCategoryMessage");
        }

    }
}