using System;
using Api.Core.Shared.ErrorCodes;

namespace Api.Core.Shared.DomainExceptions
{
    public class DomainException: Exception
    {
        private readonly string code;

        public DomainException()
        {
        }

        public DomainException(string message, string errorCode)
            : base(message)
        {
            this.code = errorCode;
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
