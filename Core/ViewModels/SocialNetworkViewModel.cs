namespace SocialPulse.Core.ViewModels
{
    public class SocialNetworkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string BaseDomain { get; set; } = null!;
        public byte[] Icon { get; set; } = null!;

        public string IconBase64 => $"data:image/jpeg;base64,{Convert.ToBase64String(Icon)}";

    }
}
