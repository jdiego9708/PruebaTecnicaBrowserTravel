using SISBrowserTravelBooks.Entities.Models;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;

namespace SISBrowserTravelBooks.Services.Interfaces
{
    public interface IAuthorsService
    {
        RestResponseModel InsertAuthors(Authors author);
        RestResponseModel SearchAuthors(SearchBindingModel search);
    }
}
