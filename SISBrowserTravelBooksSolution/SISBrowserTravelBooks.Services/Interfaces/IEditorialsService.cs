using SISBrowserTravelBooks.Entities.Models;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;

namespace SISBrowserTravelBooks.Services.Interfaces
{
    public interface IEditorialsService
    {
        RestResponseModel InsertEditorial(Editorials editorial);
        RestResponseModel SearchEditorials(SearchBindingModel search);
    }
}
