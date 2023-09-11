using LibraryCore.EntityLayer.Concrete;

namespace LibraryCore.PresentationLayer.Models
{
    public class BookModel
    {
        //kitaplarla ilgili işlem yapılacak olan sayfalarda gerekli olan ve kitap verilerini tutan model

        public List<Book> Books { get; set; }
        public Book Book { get; set; }
        public ImageBook ImageBook { get; set; }
        public List<AuthorWithFullName> Authors { get; set; }
        public List<EntityLayer.Concrete.Type> Types { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
        

    }
}
