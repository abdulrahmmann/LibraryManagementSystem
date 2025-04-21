using System.Net;

namespace LibraryManagementSystem.Application.Common
{
    public class AuthResponse
    {
        public string Token { get; set; }

        public bool Result { get; set; }

        public string Message { get; set; } = string.Empty;

        public HttpStatusCode HttpStatusCode { get; set; }

        public DateTime Timestamp { get; set; }

        public List<string> Errors { get; set; } = [];

        public AuthResponse(string token, bool result, string message, HttpStatusCode httpStatusCode)
        {
            Token = token;
            Result = result;
            Message = message;
            HttpStatusCode = httpStatusCode;
            Timestamp = DateTime.UtcNow;
        }
    }
}
