using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISBrowserTravelBooks.Entities.Models;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;
using SISBrowserTravelBooks.Services.Interfaces;
using System;

namespace SISBrowserTravelBooks.Controllers
{

    [Route("api/")]
    [ApiController]
    public class AuthorsController : Controller
    {
        private readonly ILogger<AuthorsController> logger;
        private IAuthorsService IAuthorsService { get; set; }
        public AuthorsController(ILogger<AuthorsController> logger,
            IAuthorsService IAuthorsService)
        {
            this.logger = logger;
            this.IAuthorsService = IAuthorsService;
        }

        [Authorize]
        [HttpPost]
        [Route("InsertAuthors")]
        public IActionResult InsertAuthors(JObject authorsJson)
        {
            try
            {
                logger.LogInformation("Start InsertAuthors(");

                if (authorsJson == null)
                    throw new Exception("InsertAuthors info empty");

                Authors authorModel = authorsJson.ToObject<Authors>();

                if (authorModel == null)
                {
                    logger.LogInformation("Not Information of authorModel");
                    throw new Exception("Not Information of authorModel");
                }
                else
                {
                    RestResponseModel rpta = this.IAuthorsService.InsertAuthors(authorModel);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"InsertAuthors successfull");
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
                logger.LogError("Error in controller InsertAuthors", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SearchAuthors")]
        public IActionResult SearchAuthors(JObject searchJson)
        {
            try
            {
                logger.LogInformation("Start SearchAuthors");

                if (searchJson == null)
                    throw new Exception("SearchAuthors info empty");

                SearchBindingModel searchModel = searchJson.ToObject<SearchBindingModel>();

                if (searchModel == null)
                {
                    logger.LogInformation("Not Information of searchModel");
                    throw new Exception("Not Information of searchModel");
                }
                else
                {
                    RestResponseModel rpta = this.IAuthorsService.SearchAuthors(searchModel);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"SearchAuthors successfull");
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
                logger.LogError("Error in controller SearchAuthors", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
