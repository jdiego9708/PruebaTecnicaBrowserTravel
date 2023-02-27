using SISBrowserTravelBooks.Entities.ModelsConfiguration;

namespace SISBrowserTravelBooks.Services.Interfaces
{
    public interface IUsersService
    {
        RestResponseModel ProcessLogin(LoginModel loginModel);
    }
}
