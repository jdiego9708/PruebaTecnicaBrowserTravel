using SISBrowserTravelBooks.Entities.Models;
using SISBrowserTravelBooks.Entities.Models.BindingModels;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;

namespace SISBrowserTravelBooks.Services.Interfaces
{
    public interface IBooksService
    {
        RestResponseModel InsertBook(InsertBookBindingModel book);
        RestResponseModel SearchBooks(SearchBindingModel search);
    }
}
