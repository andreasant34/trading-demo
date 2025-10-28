using Trading.Core.Models;

namespace Trading.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a request is invalid
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(ErrorCode exceptionCode)
        {
            ErrorCode = exceptionCode;
        }

        public ErrorCode ErrorCode { get; }
    }
}
