using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;
using SISBrowserTravelBooks.Services.Interfaces;
using System;

namespace SISBrowserTravelBooks.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> logger;
        private IUsersService IUsersService { get; set; }
        public UsersController(ILogger<UsersController> logger,
            IUsersService IUsersService)
        {
            this.logger = logger;
            this.IUsersService = IUsersService;
        }

        [HttpPost]
        [Route("ProcessLogin")]
        public IActionResult ProcessLogin(JObject loginJson)
        {
            try
            {
                logger.LogInformation("Start ProcessLogin");

                if (loginJson == null)
                    throw new Exception("ProcessLogin info empty");

                LoginModel loginModel = loginJson.ToObject<LoginModel>();

                if (loginModel == null)
                {
                    logger.LogInformation("Not Information of loginModel");
                    throw new Exception("Not Information of loginModel");
                }
                else
                {
                    RestResponseModel rpta = this.IUsersService.ProcessLogin(loginModel);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"ProcessLogin successfull");
                        return Ok(rpta.Response);
                    }
                    else
                    {
                        return BadRequest(rpta.Response);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error in controller ProcessLogin", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
