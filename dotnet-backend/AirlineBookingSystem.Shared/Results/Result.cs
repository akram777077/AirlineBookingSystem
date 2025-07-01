namespace AirlineBookingSystem.Shared.Results;


public class Result<T>
{
    public bool IsSuccess => (int)StatusCode < 400;
    public bool IsFailure => !IsSuccess;
    public T Value { get; }
    public string Error { get; }
    public ResultStatusCode StatusCode { get; }
    public Dictionary<string, object> Metadata { get; }

    protected Result(T value, string error, ResultStatusCode statusCode, Dictionary<string, object> metadata = null)
    {
        Value = value;
        Error = error;
        StatusCode = statusCode;
        Metadata = metadata ?? new Dictionary<string, object>();
    }

    // Success factory methods
    public static Result<T> Success(T value, ResultStatusCode statusCode = ResultStatusCode.Success)
        => new(value, null, statusCode);

    public static Result<T> Created(T value, string location = null)
    {
        var metadata = location != null ? new Dictionary<string, object> { ["Location"] = location } : null;
        return new(value, null, ResultStatusCode.Created, metadata);
    }

    public static Result<T> NoContent()
        => new(default!, null, ResultStatusCode.NoContent);

    // Failure factory methods
    public static Result<T> Failure(string error, ResultStatusCode statusCode = ResultStatusCode.BadRequest)
        => new(default!, error, statusCode);

    public static Result<T> NotFound(string error = "Resource not found")
        => new(default!, error, ResultStatusCode.NotFound);

    public static Result<T> Unauthorized(string error = "Unauthorized access")
        => new(default!, error, ResultStatusCode.Unauthorized);

    public static Result<T> Forbidden(string error = "Access forbidden")
        => new(default!, error, ResultStatusCode.Forbidden);

    public static Result<T> Conflict(string error = "Resource conflict")
        => new(default!, error, ResultStatusCode.Conflict);

    public static Result<T> ValidationError(string error)
        => new(default!, error, ResultStatusCode.UnprocessableEntity);

    // Utility methods
    public Result<TNew> Map<TNew>(Func<T, TNew> mapper)
    {
        if (IsFailure)
            return Result<TNew>.Failure(Error, StatusCode);
        
        return Result<TNew>.Success(mapper(Value), StatusCode);
    }

    public async Task<Result<TNew>> MapAsync<TNew>(Func<T, Task<TNew>> mapper)
    {
        if (IsFailure)
            return Result<TNew>.Failure(Error, StatusCode);
        
        var mappedValue = await mapper(Value);
        return Result<TNew>.Success(mappedValue, StatusCode);
    }
}
public class Result
{
    public bool IsSuccess => (int)StatusCode < 400;
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public ResultStatusCode StatusCode { get; }
    public Dictionary<string, object> Metadata { get; }

    protected Result(string error, ResultStatusCode statusCode, Dictionary<string, object> metadata = null)
    {
        Error = error;
        StatusCode = statusCode;
        Metadata = metadata ?? new Dictionary<string, object>();
    }

    public static Result Success(ResultStatusCode statusCode = ResultStatusCode.Success)
        => new(null, statusCode);

    public static Result Created(string location = null)
    {
        var metadata = location != null ? new Dictionary<string, object> { ["Location"] = location } : null;
        return new(null, ResultStatusCode.Created, metadata);
    }

    public static Result NoContent()
        => new(null, ResultStatusCode.NoContent);

    public static Result Failure(string error, ResultStatusCode statusCode = ResultStatusCode.BadRequest)
        => new(error, statusCode);

    public static Result NotFound(string error = "Resource not found")
        => new(error, ResultStatusCode.NotFound);

    public static Result Unauthorized(string error = "Unauthorized access")
        => new(error, ResultStatusCode.Unauthorized);

    public static Result Forbidden(string error = "Access forbidden")
        => new(error, ResultStatusCode.Forbidden);

    public static Result Conflict(string error = "Resource conflict")
        => new(error, ResultStatusCode.Conflict);

    public static Result ValidationError(string error)
        => new(error, ResultStatusCode.UnprocessableEntity);
}