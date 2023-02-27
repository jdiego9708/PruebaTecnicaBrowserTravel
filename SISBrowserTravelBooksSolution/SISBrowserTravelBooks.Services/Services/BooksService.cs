using Newtonsoft.Json;
using SISBrowserTravelBooks.DataAccess.Interfaces;
using SISBrowserTravelBooks.Entities.Models;
using SISBrowserTravelBooks.Entities.Models.BindingModels;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;
using SISBrowserTravelBooks.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SISBrowserTravelBooks.Services.Services
{
    public class BooksService : IBooksService
    {
        #region CONSTRUCTOR AND DEPENDENCY INYECTION
        public IBooksDac IBooksDac { get; set; }
        public IAuthorsDac IAuthorsDac { get; set; }
        public BooksService(IBooksDac IBooksDac,
            IAuthorsDac IAuthorsDac)
        {
            this.IBooksDac = IBooksDac;
            this.IAuthorsDac = IAuthorsDac;
        }
        #endregion

        #region METHODS
        private static bool ValidationInsert(Books book, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (book.Id_editorial == 0)
                    throw new Exception("Verify Id_editorial");

                if (string.IsNullOrEmpty(book.Tittle_book))
                    throw new Exception("Verify Tittle_book");

                if (string.IsNullOrEmpty(book.Synopsis_book))
                    throw new Exception("Verify Synopsis_book");

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel InsertBook(InsertBookBindingModel insertbook)
        {
            RestResponseModel response = new();
            try
            {
                if (!ValidationInsert(insertbook, out string rpta))
                    throw new Exception(rpta);

                rpta = this.IBooksDac.InsertBook(insertbook);

                if (!rpta.Equals("OK"))
                    throw new Exception($"Error inserting book | {rpta}");

                rpta = this.IAuthorsDac.InsertAuthorBook(new Authors_books()
                {
                    Id_author = insertbook.Id_author,
                    Id_book = insertbook.Id_book,
                });

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(insertbook);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel SearchBooks(SearchBindingModel search)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(search.Type_search))
                    throw new Exception("Verify Type_search");

                if (string.IsNullOrEmpty(search.Value_search))
                    throw new Exception("Verify Value_search");

                string rpta =
                    this.IBooksDac.SearchBooks(search.Type_search,
                    search.Value_search, out DataTable dtBooks);

                List<Books> books = new();

                if (dtBooks == null)
                {
                    if (rpta.Equals("OK"))
                    {
                        books.Add(new Books()
                        {
                            Tittle_book = "Find empty"
                        });

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(books);
                        return response;
                    }
                    else
                        throw new Exception($"Error | {rpta}");
                }

                books = (from DataRow row in dtBooks.Rows
                           select new Books(row)).ToList();

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(books);
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
