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
    public class AuthorsService : IAuthorsService
    {
        #region CONSTRUCTOR AND DEPENDENCY INYECTION
        public IAuthorsDac IAuthorsDac { get; set; }
        public AuthorsService(IAuthorsDac IAuthorsDac)
        {
            this.IAuthorsDac = IAuthorsDac;
        }
        #endregion

        #region METHODS
        private static bool ValidationInsert(Authors author, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (string.IsNullOrEmpty(author.Name_author))
                    throw new Exception("Verify Name_author");

                if (string.IsNullOrEmpty(author.Last_name_author))
                    throw new Exception("Verify Last_name_author");

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel InsertAuthors(Authors author)
        {
            RestResponseModel response = new();
            try
            {
                if (!ValidationInsert(author, out string rpta))
                    throw new Exception(rpta);

                rpta = this.IAuthorsDac.InsertAuthor(author);

                if (!rpta.Equals("OK"))
                    throw new Exception($"Error inserting author | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(author);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel SearchAuthors(SearchBindingModel search)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(search.Type_search))
                    throw new Exception("Verify Type_search");

                if (string.IsNullOrEmpty(search.Value_search))
                    throw new Exception("Verify Value_search");

                string rpta =
                    this.IAuthorsDac.SearchAuthors(search.Type_search,
                    search.Value_search, out DataTable dtAuthors);

                List<Authors> authors = new();

                if (dtAuthors == null)
                {
                    if (rpta.Equals("OK"))
                    {
                        authors.Add(new Authors()
                        {
                            Name_author = "Find empty"
                        });

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(authors);
                        return response;
                    }
                    else
                        throw new Exception($"Error | {rpta}");
                }

                authors = (from DataRow row in dtAuthors.Rows
                           select new Authors(row)).ToList();

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(authors);
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
