namespace SocialPulse.Helpers
{
    public static class FormFileExtensions
    {
        public static byte[] ToByteArray(this IFormFile file)
        {
            if (file == null) return Array.Empty<byte>();

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
