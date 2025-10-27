namespace Trading.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a request is invalid
    /// </summary>
    internal class BadRequestException:Exception
    {
        public BadRequestException(ExceptionCode exceptionCode)
        {
            ExceptionCode = exceptionCode;
        }

        public ExceptionCode ExceptionCode { get; }
    }
}
