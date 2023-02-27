using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISBrowserTravelBooks.Entities.Models;
using SISBrowserTravelBooks.Entities.Models.BindingModels;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;
using SISBrowserTravelBooks.Services.Interfaces;
using System;

namespace SISBrowserTravelBooks.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> logger;
        private IBooksService IBooksService { get; set; }
        public BooksController(ILogger<BooksController> logger,
            IBooksService IBooksService)
        {
            this.logger = logger;
            this.IBooksService = IBooksService;
        }

        [HttpPost]
        [Route("InsertBooks")]
        public IActionResult InsertBooks(JObject booksJson)
        {
            try
            {
                logger.LogInformation("Start InsertBooks");

                if (booksJson == null)
                    throw new Exception("InsertBooks info empty");

                InsertBookBindingModel bookModel = booksJson.ToObject<InsertBookBindingModel>();

                if (bookModel == null)
                {
                    logger.LogInformation("Not Information of BookModel");
                    throw new Exception("Not Information of BookModel");
                }
                else
                {
                    RestResponseModel rpta = this.IBooksService.InsertBook(bookModel);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"InsertBook successfull");
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
                logger.LogError("Error in controller InsertBook", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SearchBooks")]
        public IActionResult SearchBooks(JObject searchJson)
        {
            try
            {
                logger.LogInformation("Start SearchBooks");

                if (searchJson == null)
                    throw new Exception("SearchBooks info empty");

                SearchBindingModel searchModel = searchJson.ToObject<SearchBindingModel>();

                if (searchModel == null)
                {
                    logger.LogInformation("Not Information of searchModel");
                    throw new Exception("Not Information of searchModel");
                }
                else
                {
                    RestResponseModel rpta = this.IBooksService.SearchBooks(searchModel);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"SearchBooks successfull");
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
                logger.LogError("Error in controller SearchBooks", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
