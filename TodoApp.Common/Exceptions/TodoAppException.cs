using System;

namespace TodoApp.Common.Extceptions
{
    public class TodoAppException : Exception
    {
        public int ErrorCode { get; set; }

        public TodoAppException() : base("TodoApp Exception")
        {
        }

        public TodoAppException(string message) : base(message)
        {
        }

        public TodoAppException(int statusCode, string message) : base(message)
        {
            ErrorCode = statusCode;
        }

        public TodoAppException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TodoAppException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = statusCode;
        }
    }
}
