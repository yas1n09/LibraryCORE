
namespace LibraryCore.PresentationLayer.Models
{
    public class ImageBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPage { get; set; }
        public IFormFile Image { get; set; }
        public int AuthorId { get; set; }
        public int TypeId { get; set; }
    }
}
