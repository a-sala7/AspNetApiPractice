namespace AspNetApiPractice.Services.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string resourceName, object id) : 
        base(FormatMessage(resourceName, id)){}

    private static string FormatMessage(string resourceName, object id)
    {
        return $"{resourceName} with ID {id.ToString()} not found.";
    }
}