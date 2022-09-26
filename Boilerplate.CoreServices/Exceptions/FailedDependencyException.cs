namespace Boilerplate.CoreServices.Exceptions;

using System;

public class FailedDependencyException : Exception
{
    public FailedDependencyException(string message) : base(message)
    {
    }
}