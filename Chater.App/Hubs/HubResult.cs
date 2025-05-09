namespace Chater.Hubs;

public class HubResult<T>
{
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
}

public class HubResult
{
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsSuccess;
}