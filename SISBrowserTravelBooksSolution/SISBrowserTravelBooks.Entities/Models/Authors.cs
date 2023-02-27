using SISBrowserTravelBooks.Entities.Helpers;
using System.Collections.Generic;
using System.Data;

namespace SISBrowserTravelBooks.Entities.Models
{
    public class Authors
    {
        public Authors()
        {

        }
        public Authors(DataRow row)
        {
            this.Id_author = ConvertValueHelper.ConvertIntValue(row["Id_author"]);
            this.Name_author = ConvertValueHelper.ConvertStringValue(row["Name_author"]);
            this.Last_name_author = ConvertValueHelper.ConvertStringValue(row["Last_name_author"]);
        }
        public int Id_author { get; set; }
        public string Name_author { get; set; }
        public string Last_name_author { get; set; }
        public List<Books> Books { get; set; }
    }
}
