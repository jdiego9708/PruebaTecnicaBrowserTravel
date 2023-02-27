using SISBrowserTravelBooks.Entities.Models;
using System.Data;

namespace SISBrowserTravelBooks.DataAccess.Interfaces
{
    public interface IEditorialsDac
    {
        string InsertEditorial(Editorials editorial);
        string UpdateEditorial(Editorials editorial);
        string SearchEditorials(string type_search, string value_search,
            out DataTable dt);
    }
}
