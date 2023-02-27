namespace SISBrowserTravelBooks.Entities.Models.BindingModels
{
    public class InsertBookBindingModel : Books
    {
        public int Id_author { get; set; }
        public Authors Author { get; set; }
    }
}
