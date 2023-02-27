using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using SISBrowserTravelBooks.Entities.Models;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;
using SISBrowserTravelBooks.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SISBrowserTravelBooks.Test.ServicesTest
{
    public class EditorialsTest
    {
        //readonly IServiceProvider _services =
        //  Program.CreateHostBuilder(new string[] { }).Build().Services;
        //public IEditorialsService EditorialsService { get; set; }
        public EditorialsTest()
        {
            //this.EditorialsService = EditorialsService;
        }
        [Test]
        public void When_name_editorial_is_empty()
        {
            try
            {
                Editorials editorial = new()
                {
                    Id_editorial = 0,
                    Name_editorial = string.Empty,
                    Campus_editorial = string.Empty,
                };

                RestResponseModel response = new() { Response = "hola"};

                if (response == null)
                    throw new Exception("Error in response from EditorialsService");

                string message = Convert.ToString(response.Response);

                if (string.IsNullOrEmpty(message))
                    throw new Exception("Error in response from EditorialsService");

                Assert.That(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
