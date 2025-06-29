namespace PRN232Project
{
    [Serializable]
    public class ProblemException : Exception
    {
        public string Title { get; }
        public int StatusCode { get; }
        public override string Message { get; }

        public ProblemException(string title, int statusCode, string message) : base(message)
        {
            Title = title;
            StatusCode = statusCode;
            Message = message;
        }

        public ProblemException(string title, int statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            Title = title;
            StatusCode = statusCode;
            Message = message;
        }

        // Convenience methods for common HTTP status codes
        public static ProblemException BadRequest(string message) =>
            new("Bad Request", 400, message);

        public static ProblemException NotFound(string message) =>
            new("Not Found", 404, message);

        public static ProblemException InternalServerError(string message) =>
            new("Internal Server Error", 500, message);

        public static ProblemException InternalServerError(string message, Exception innerException) =>
            new("Internal Server Error", 500, message, innerException);
    }
}
