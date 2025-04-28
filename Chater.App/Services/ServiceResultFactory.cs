namespace Chater.App.Services;

public class ServiceResultFactory : IServiceResultFactory
{
    public ServiceResult Success() => new() { IsSuccess = true };
    
    public ServiceResult<T> Success<T>(T data) => new() { 
        IsSuccess = true, 
        Data = data 
    };
    
    public ServiceResult Failure(string error, int statusCode) => new() { 
        IsSuccess = false, 
        ErrorMessage = error, 
        StatusCode = statusCode 
    };
    
    public ServiceResult<T> Failure<T>(string error, int statusCode) => new() { 
        IsSuccess = false, 
        ErrorMessage = error, 
        StatusCode = statusCode 
    };
}