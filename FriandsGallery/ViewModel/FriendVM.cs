using System.ComponentModel.DataAnnotations;

namespace FriandsGallery.ViewModel
{
    public class FriendVM
    {
        public long Id { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "First Name should be in a range of 3 - 100")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Last Name should be in a range of 3 - 100")]
        public string LastName { get; set; } = string.Empty;

        public IFormFile? UploadedImage { get; set; }

        public byte[]? FriendImage { get; set; }
    }
}
