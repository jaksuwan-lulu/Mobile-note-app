using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dululu.Models;
using SQLite;

namespace dululu.DataServices
{
    public class BookService : LBookService
    {
        private SQLiteAsyncConnection BookDbConnection;

        public BookService() => SetupBookDatabase();

        private async void SetupBookDatabase()
        {
            if (BookDbConnection is null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DemobookDB.db3");
                BookDbConnection = new SQLiteAsyncConnection(dbPath);
                await BookDbConnection.CreateTableAsync<Book>();
                //await BookDbConnection.CreateTableAsync<History>();
            }
        }

        public async Task<ServiceResponse> AddOrUpdateBookAsync(Book book)
        {
            if (book is null) return ErrorMessage(-1);
            if (book.Id is 0)
            {
                int responseId = await BookDbConnection.InsertAsync(book);
                return SuccessMessage(responseId);
            }

            int updateResponseCode = await BookDbConnection.UpdateAsync(book);
            return SuccessMessage(updateResponseCode);
        }

        public async Task<ServiceResponse> DeleteBookAsync(Book book)
        {
            if (book.Id > 0)
            {
                var result = await GetBookAsync(book.Id);
                if (result is null) return ErrorMessage(book.Id);

                int responseId = await BookDbConnection?.DeleteAsync(book);
                return SuccessMessage(responseId);
            }
            return ErrorMessage(-1);
        }

        public async Task<Book> GetBookAsync(int id) => await BookDbConnection.Table<Book>().Where(_ => _.Id == id).FirstOrDefaultAsync();

        public async Task<List<Book>> GetBooksAsync() => await BookDbConnection.Table<Book>().ToListAsync();

        public async Task<List<Book>> GetBooksByCategoryAsync(BookCategory category)
        {
            return await BookDbConnection.Table<Book>().Where(b => b.Category == category.ToString()).ToListAsync();
        }

        private static ServiceResponse SuccessMessage(int responseId) => new() { Flag = true, DatabaseResponseValue = responseId, Message = "Process Completed" };
        private static ServiceResponse ErrorMessage(int responseId) => new() { Flag = true, DatabaseResponseValue = responseId, Message = "Process Failed" };

        public async Task<List<Book>> GetBookAsync(string selectedCategory)
        {
            return await BookDbConnection.Table<Book>().Where(b => b.Category == selectedCategory).ToListAsync();
        }
    }
}