namespace LibraryCore.PresentationLayer.Models
{
    public class AuthorWithFullName //yazarın ad ve soyadını tek bir satırda getirebilme için model.
    {                               
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
