namespace LibraryCore.PresentationLayer.Models
{
    public class AuthorWithBook     //yazarların ad-soyad ve kitap sayfasıyla gösterildiği ön yüz için model
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NumberOfBook { get; set; }
    }
}
