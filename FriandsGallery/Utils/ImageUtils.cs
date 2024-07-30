namespace FriandsGallery.Utils
{
    public static class ImageUtils
    {
        public static byte[]? ConvertFormIFormFile(IFormFile? image)
        {
            if (image == null) { return null; }
            try
            {
                using (MemoryStream stream = new())
                {
                    image.CopyTo(stream);
                    return stream.ToArray();
                }

            }

            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
