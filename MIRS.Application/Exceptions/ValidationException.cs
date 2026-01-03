namespace MIRS.Application.Exceptions;

public sealed class ValidationException : ApplicationException
{
    public IReadOnlyCollection<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors)
        : base("Validation failed.")
    {
        Errors = errors.ToArray();
    }

    public ValidationException(string error)
        : this(new[] { error })
    {
    }
}