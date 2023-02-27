using SISBrowserTravelBooks.Entities.Models;
using System.Data;

namespace SISBrowserTravelBooks.DataAccess.Interfaces
{
    public interface IAuthorsDac
    {
        string InsertAuthorBook(Authors_books author_book);
        string InsertAuthor(Authors author);
        string UpdateAuthor(Authors author);
        string SearchAuthors(string type_search, string value_search,
            out DataTable dt);
    }
}
