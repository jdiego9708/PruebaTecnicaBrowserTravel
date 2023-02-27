using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;
using SISBrowserTravelBooks.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SISBrowserTravelBooks.Services.Services
{
    public class UsersService : IUsersService
    {
        #region CONSTRUCTOR AND DEPENDENCY INYECTION
        public JwtModel Jwt { get; set; }
        public UsersService(IConfiguration Configuration)
        {
            var settings = Configuration.GetSection("Jwt");
            JwtModel jwtSecurity = settings.Get<JwtModel>();
            this.Jwt = jwtSecurity;
        }
        #endregion

        #region METHODS
        private string GetTokenLogin(string user, DateTime dateExpired)
        {
            dateExpired = DateTime.UtcNow.AddHours(+12);
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.UTF8.GetBytes(this.Jwt.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.Name, user),
                        }
                    ),
                Expires = dateExpired,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public RestResponseModel ProcessLogin(LoginModel loginModel)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(loginModel.Usuario))
                    throw new Exception("Verify Usuario");

                if (string.IsNullOrEmpty(loginModel.Password))
                    throw new Exception("Verify Password");

                if (loginModel.Usuario.Equals("browsertravelusertest"))
                {
                    if (loginModel.Password.Equals("test"))
                    {
                        string jwtToken =
                            this.GetTokenLogin(loginModel.Usuario,
                            loginModel.FechaLogin.AddDays(7));

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(
                            new 
                            { 
                                Mensaje = $"Inicio de sesión correcto, use el AccessToken " +
                                $"para hacer llamados a otros endpoints. Vencimiento {loginModel.FechaLogin.AddDays(7)}",
                                AccessToken = jwtToken,
                                loginModel.Usuario,
                            });
                        return response;
                    }
                }

                response.IsSucess = false;
                response.Response = JsonConvert.SerializeObject(
                    new
                    {
                        Mensaje = "No se pudo iniciar sesión, verifique las credenciales",
                        Usuario = loginModel.Usuario,
                    });
                return response;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        #endregion
    }
}
