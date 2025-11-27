namespace Coink.Presentation.Common.Errors;

public class ErrorResponse
{
    public int Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? TraceId { get; set; }
    public List<ValidationError>? Errors { get; set; }
}
public class ValidationError
{
    public string Field { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}
