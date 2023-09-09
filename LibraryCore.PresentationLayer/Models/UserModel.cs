using LibraryCore.EntityLayer.Concrete;

namespace LibraryCore.PresentationLayer.Models
{
    public class UserModel
    {
        public List<User> Users { get; set; }
        public User User { get; set; }
        public List<Position> Positions { get; set; }
        public BorrowedBook BorrowedBook { get; set; }
    }
}
