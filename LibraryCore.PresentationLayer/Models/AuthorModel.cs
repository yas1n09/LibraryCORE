using LibraryCore.EntityLayer.Concrete;

namespace LibraryCore.PresentationLayer.Models
{
    public class AuthorModel
    {
        public Author Author { get; set; }
        public List<Author> Authors { get; set; }
        public List<AuthorWithBook> AuthorWithBooks { get; set; }

    }
}
