using SISBrowserTravelBooks.Entities.Helpers;
using System.Collections.Generic;
using System.Data;

namespace SISBrowserTravelBooks.Entities.Models
{
    public class Books
    {
        public Books()
        {
            this.Tittle_book = string.Empty;
            this.Synopsis_book = string.Empty;
        }
        public Books(DataRow row)
        {
            this.Id_book = ConvertValueHelper.ConvertIntValue(row["Id_book"]);
            this.Id_editorial = ConvertValueHelper.ConvertIntValue(row["Id_editorial"]);
            this.Tittle_book = ConvertValueHelper.ConvertStringValue(row["Tittle_book"]);
            this.Synopsis_book = ConvertValueHelper.ConvertStringValue(row["Synopsis_book"]);
        }
        public int Id_book { get; set; }    
        public int Id_editorial { get; set; }
        public string Tittle_book { get; set; } 
        public string Synopsis_book { get; set; }
        public List<Authors> Authors { get; set; }
    }
}
