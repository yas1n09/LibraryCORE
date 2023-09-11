using LibraryCore.BusinessLayer.Results;

namespace LibraryCore.PresentationLayer.Models
{
    public class ErrorViewModel : Result    //validation hata mesajlarýný fýrlatmak için sýnýf
    {
        public ErrorViewModel(bool success, string message) : base(success, message)
        {
        }

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}