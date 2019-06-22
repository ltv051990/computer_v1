using System;

namespace computor_v1
{
    [Serializable]
    public abstract class DomainException : Exception
    {
        public string ExceptionInfo { get; }

        protected DomainException()
        {
        }

        protected DomainException(string exception)
          : base(exception)
        {
            ExceptionInfo = exception;
        }
    }
}
