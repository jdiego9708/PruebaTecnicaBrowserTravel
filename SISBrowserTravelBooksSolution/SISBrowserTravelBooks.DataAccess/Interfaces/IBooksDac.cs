using SISBrowserTravelBooks.Entities.Models;
using System.Data;

namespace SISBrowserTravelBooks.DataAccess.Interfaces
{
    public interface IBooksDac
    {
        string InsertBook(Books book);
        string UpdateBook(Books book);
        string SearchBooks(string type_search, string value_search,
            out DataTable dt);
    }
}
