using dululu.Models;
using System.Collections.ObjectModel;

namespace dululu.DataServices
{
    public interface LBookService
    {
        Task<ServiceResponse> AddOrUpdateBookAsync(Book book);
        Task<ServiceResponse> DeleteBookAsync(Book book);
        Task<List<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task<List<Book>> GetBookAsync(string selectedCategory);
        Task<List<Book>> GetBooksByCategoryAsync(BookCategory category);

    }
}
