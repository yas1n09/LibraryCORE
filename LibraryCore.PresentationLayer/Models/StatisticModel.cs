namespace LibraryCore.PresentationLayer.Models
{
    public class StatisticModel //istatistik sayfasında gerekli istatistikleri saglayan model
    {
        public int NumberOfBooks { get; set; }
        public int NumberOfAuthors { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfBorrowedBooks { get; set; }
        public int NumberOfTypes { get; set; }
        public int NumberOfPositions { get; set; }
    }
}
