using LibraryCore.EntityLayer.Concrete;

namespace LibraryCore.PresentationLayer.Models
{
    public class TypeModel
    {
        public EntityLayer.Concrete.Type Type { get; set; }
        public List<EntityLayer.Concrete.Type> Types { get; set; }
        public List<TypeWithBook> TypeWithBooks { get; set; }

    }
}
