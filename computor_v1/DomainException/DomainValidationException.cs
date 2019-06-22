using System;

namespace computor_v1
{
    [Serializable]
    public class DomainValidationException : DomainException
    {
        public DomainValidationException()
        {
        }

        public DomainValidationException(string exception)
            : base(exception)
        {
        }
    }
}
