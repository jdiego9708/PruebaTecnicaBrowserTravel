using SISBrowserTravelBooks.Entities.Helpers;
using System.Data;

namespace SISBrowserTravelBooks.Entities.Models
{
    public class Editorials
    {
        public Editorials()
        {
            this.Name_editorial = string.Empty;
            this.Campus_editorial = string.Empty;
        }
        public Editorials(DataRow row)
        {
            this.Id_editorial = ConvertValueHelper.ConvertIntValue(row["Id_editorial"]);
            this.Name_editorial = ConvertValueHelper.ConvertStringValue(row["Name_editorial"]);
            this.Campus_editorial = ConvertValueHelper.ConvertStringValue(row["Campus_editorial"]);
        }
        public int Id_editorial { get; set; }
        public string Name_editorial { get; set; }
        public string Campus_editorial { get; set; }
    }
}
