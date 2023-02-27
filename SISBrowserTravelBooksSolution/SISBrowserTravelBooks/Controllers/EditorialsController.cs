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
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class EditorialsController : Controller
    {
        private readonly ILogger<EditorialsController> logger;
        private IEditorialsService IEditorialsService { get; set; }
        public EditorialsController(ILogger<EditorialsController> logger,
            IEditorialsService IEditorialsService)
        {
            this.logger = logger;
            this.IEditorialsService = IEditorialsService;
        }

        [HttpPost]
        [Route("InsertEditorials")]
        public IActionResult InsertEditorials(JObject editorialsJson)
        {
            try
            {
                logger.LogInformation("Start InsertEditorials");

                if (editorialsJson == null)
                    throw new Exception("InsertEditorials info empty");

                Editorials editorialModel = editorialsJson.ToObject<Editorials>();

                if (editorialModel == null)
                {
                    logger.LogInformation("Not Information of editorialModel");
                    throw new Exception("Not Information of editorialModel");
                }
                else
                {
                    RestResponseModel rpta = this.IEditorialsService.InsertEditorial(editorialModel);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"InsertEditorials successfull");
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
                logger.LogError("Error in controller InsertEditorials", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SearchEditorials")]
        public IActionResult SearchEditorials(JObject searchJson)
        {
            try
            {
                logger.LogInformation("Start SearchEditorials");

                if (searchJson == null)
                    throw new Exception("SearchEditorials info empty");

                SearchBindingModel searchModel = searchJson.ToObject<SearchBindingModel>();

                if (searchModel == null)
                {
                    logger.LogInformation("Not Information of searchModel");
                    throw new Exception("Not Information of searchModel");
                }
                else
                {
                    RestResponseModel rpta = this.IEditorialsService.SearchEditorials(searchModel);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"SearchEditorials successfull");
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
                logger.LogError("Error in controller SearchEditorials", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
