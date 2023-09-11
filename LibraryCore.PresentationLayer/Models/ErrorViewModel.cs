using LibraryCore.BusinessLayer.Results;

namespace LibraryCore.PresentationLayer.Models
{
    public class ErrorViewModel : Result    //validation hata mesajlar�n� f�rlatmak i�in s�n�f
    {
        public ErrorViewModel(bool success, string message) : base(success, message)
        {
        }

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}