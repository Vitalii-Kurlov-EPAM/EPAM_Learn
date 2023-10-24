namespace Module_02.Task_02.CatalogService.WebApi.Models;

public sealed class ErrorModelResponse
{
    public bool IsError { get; }
    public string Type { get; }
    public Dictionary<string, string[]> Errors { get; }

    public ErrorModelResponse(string type, IEnumerable<KeyValuePair<string, string[]>> errors = null)
    {
        IsError = true;
        Type = type;
        Errors = new Dictionary<string, string[]>(errors ?? Array.Empty<KeyValuePair<string, string[]>>());
    }

    public ErrorModelResponse(string type, params string[] errors)
        : this(type, errors?.Length > 0 ? new[] { new KeyValuePair<string, string[]>(string.Empty, errors) } : null)
    {
    }
}