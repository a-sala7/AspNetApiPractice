namespace AspNetApiPractice.ViewModels.Shared;
public class ValidationErrors
{
    public IEnumerable<string> Errors { get; }
    public ValidationErrors(string error)
    {
        this.Errors = new string[] {error};
    }
    public ValidationErrors(IEnumerable<string> errors)
    {
        this.Errors = errors;
    }
}