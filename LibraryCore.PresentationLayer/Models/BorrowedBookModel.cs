using LibraryCore.EntityLayer.Concrete;

namespace LibraryCore.PresentationLayer.Models
{
    public class BorrowedBookModel
    {
        public BorrowedBook BorrowedBook { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
    }
}
