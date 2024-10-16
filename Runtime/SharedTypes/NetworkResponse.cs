using System;

[Serializable]
public class NetworkResponse<T> : NetworkResponse
{
    public T data;

    public override string ToString()
    {
        return base.ToString() +
               $"Data: {data?.ToString() ?? "null"}\n";
    }
}

[Serializable]
public class NetworkResponse
{
    public bool isSuccessful;
    public long statusCode;
    public string errorMessage;

    public override string ToString()
    {
        return $"Network Response:\n" +
               $"Is Successful: {isSuccessful}\n" +
               $"Status Code: {statusCode}\n" +
               $"Error Message: {errorMessage}\n";
    }
}
