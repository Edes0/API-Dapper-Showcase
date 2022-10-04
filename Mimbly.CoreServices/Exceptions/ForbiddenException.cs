namespace Mimbly.CoreServices.Exceptions;

using System;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message)
    {
    }
}