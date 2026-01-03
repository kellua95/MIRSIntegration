namespace MIRS.Api.Errors;

public class ApiValidationErrorResponse : ApiResponse
{
    public IEnumerable<string> Errors { get; init; }

    public ApiValidationErrorResponse(IEnumerable<string> errors)
        : base(400, "Validation failed")
    {
        Errors = errors;
    }
}