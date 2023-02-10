namespace Data.Exceptions;

public class InvalidIdException : Exception
{
    public InvalidIdException()
    {
    }
    
    public InvalidIdException(string message) : base(message)
    {
        
    }
}

public class InvalidLoginException : Exception
{
    public InvalidLoginException(string message = "Invalid login details") : base(message)
    {
        
    }
}