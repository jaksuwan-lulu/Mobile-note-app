
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dululu.DataServices;
using dululu.Models;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dululu.Models
{
    public class ValidateModel
    {

        private static bool ValidateBookModel(Book validateBook)
        {
            if (validateBook.Title is null)
                Error.Add(new Error() { Property = "Title: ", Value = "Book Title cannot be empty" });

            if (validateBook.Description is null)
                Error.Add(new Error() { Property = "Description: ", Value = "Book Description cannot be empty" });

            if (validateBook.Description.Length < 20)
                Error.Add(new Error() { Property = "Description: ", Value = "Minimun length of text must be 20" });

            if (validateBook.Image is null)
                Error.Add(new Error() { Property = "Image: ", Value = "Book Image cannot be empty" });

            return true;
        }

    }
}
