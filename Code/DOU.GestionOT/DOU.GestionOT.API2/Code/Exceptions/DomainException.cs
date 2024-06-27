using System.Runtime.Serialization;

namespace DOU.GestionOT.API2.Code.Exceptions
{
    /// <summary>
    /// Domain exception it's a controlled exception its purpose is to format and notify the user.
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">Exception's message.</param>
        public DomainException(string message)
           : base(message)
        {
        }
#if NET8_0_OR_GREATER
        [Obsolete(message: "Legacy serialization support APIs are obsolete", DiagnosticId = "SYSLIB0051")] // add this attribute to the serialization ctor
#endif
        protected DomainException(SerializationInfo info, StreamingContext context) // Noncompliant: serialization constructor is not `protected`
        : base(info, context)
        {
            // ...
        }
    }
}
