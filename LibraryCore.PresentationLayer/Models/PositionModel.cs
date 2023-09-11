using LibraryCore.EntityLayer.Concrete;

namespace LibraryCore.PresentationLayer.Models
{
    public class PositionModel //kullanıcı pozisyonu-pozisyon sayısı ve listelemek için model
    {
        public Position Position { get; set; }
        public List<Position> Positions { get; set; }
        public List<PositionWithUser> PositionWithUsers { get; set; }
    }
}
