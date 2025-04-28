namespace Chater.App.Services;

public interface IServiceResultFactory
{
    ServiceResult Success();
    ServiceResult<T> Success<T>(T data);
    ServiceResult Failure(string error, int statusCode);
    ServiceResult<T> Failure<T>(string error, int statusCode);
}
