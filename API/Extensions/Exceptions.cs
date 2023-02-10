using Data.Exceptions;

namespace Football_Manager.Extensions;

public static class Exceptions
{
    public static int GetStatusCode(this Exception ex)
    {
        return ex switch
        {
            InvalidIdException => StatusCodes.Status404NotFound,
            InvalidDataException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status400BadRequest
        };
    }
}