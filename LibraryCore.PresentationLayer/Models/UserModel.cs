using LibraryCore.EntityLayer.Concrete;

namespace LibraryCore.PresentationLayer.Models
{
    public class UserModel  //kullanıcıişlemlerinde - kullanıcının ödünç aldıgı kitaplarda kullanılan model
    {
        public List<User> Users { get; set; }
        public User User { get; set; }
        public List<Position> Positions { get; set; }
        public BorrowedBook BorrowedBook { get; set; }
    }
}
