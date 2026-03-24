using SQLite;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace dululu.Models
{
    public class Book 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }

    }
    public enum BookCategory
    {
        Novel, //0
        Manga, //1
        Manhwa //2
    }
}
