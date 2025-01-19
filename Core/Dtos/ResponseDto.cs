namespace SocialPulse.Core.Dtos
{

    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
    public class ResponseDto<T> : ResponseDto
    {
        public T? Data { get; set; }
    }
}
