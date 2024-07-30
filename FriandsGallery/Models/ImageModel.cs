namespace FriandsGallery.Models
{
	public class ImageModel
	{
		public long Id { get; set; }

		public byte[] Data { get; set; }

		public FriendModel Friend { get; set; }

		public long FriendId { get; set; }
	}
}