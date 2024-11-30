

namespace App;


public interface IError
{
    string? Field { get; }
    string Message { get; }
}

sealed class Error : IError
{
    public string? Field { get; }
    public string Message { get; }

    private Error(string message, string? field= null)
    {
        Field = field;
        Message = message;
    }

    public static IError Create(string message, string? field)
    {
        return new Error(message, field);
    }
}