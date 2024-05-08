using System;
using System.Collections.Generic;

namespace Manager.Core.Exceptions;

public class DomainException : Exception
{
    internal List<string> _errors;
    public IReadOnlyCollection<String> Errors => _errors;
    
    public DomainException(){ }

    public DomainException(string Message, List<string> errors) : base(Message)
    {
        _errors = errors;
    }

    public DomainException(string message) : base(message)
    {
        
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}