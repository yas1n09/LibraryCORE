using LibraryCore.EntityLayer.Concrete;

namespace LibraryCore.PresentationLayer.Models
{
    public class BorrowedBookModel //ödünç alınan kitaplar ve listesi için model
    {
        public BorrowedBook BorrowedBook { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
    }
}
