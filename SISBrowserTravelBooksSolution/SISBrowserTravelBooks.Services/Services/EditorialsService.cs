using Newtonsoft.Json;
using SISBrowserTravelBooks.DataAccess.Interfaces;
using SISBrowserTravelBooks.Entities.Models;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;
using SISBrowserTravelBooks.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SISBrowserTravelBooks.Services.Services
{
    public class EditorialsService : IEditorialsService
    {
        #region CONSTRUCTOR AND DEPENDENCY INYECTION
        public IEditorialsDac IEditorialsDac { get; set; }
        public EditorialsService(IEditorialsDac IEditorialsDac)
        {
            this.IEditorialsDac = IEditorialsDac;
        }
        #endregion

        #region METHODS
        private static bool ValidationInsert(Editorials editorial, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (string.IsNullOrEmpty(editorial.Name_editorial))
                    throw new Exception("Verify Name_editorial");

                if (string.IsNullOrEmpty(editorial.Campus_editorial))
                    throw new Exception("Verify Campus_editorial");

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel InsertEditorial(Editorials editorial)
        {
            RestResponseModel response = new();
            try
            {
                if (!ValidationInsert(editorial, out string rpta))
                    throw new Exception(rpta);

                rpta = this.IEditorialsDac.InsertEditorial(editorial);

                if (!rpta.Equals("OK"))
                    throw new Exception($"Error inserting editorial | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(editorial);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel SearchEditorials(SearchBindingModel search)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(search.Type_search))
                    throw new Exception("Verify Type_search");

                if (string.IsNullOrEmpty(search.Value_search))
                    throw new Exception("Verify Value_search");

                string rpta =
                    this.IEditorialsDac.SearchEditorials(search.Type_search,
                    search.Value_search, out DataTable dtEditorials);

                List<Editorials> editorials = new();

                if (dtEditorials == null)
                {
                    if (rpta.Equals("OK"))
                    {
                        editorials.Add(new Editorials()
                        {
                            Name_editorial = "Find empty"
                        });

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(editorials);
                        return response;
                    }
                    else
                        throw new Exception($"Error | {rpta}");
                }

                editorials = (from DataRow row in dtEditorials.Rows
                           select new Editorials(row)).ToList();

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(editorials);
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
