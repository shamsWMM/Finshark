namespace api.Helpers;

public static class ValidationHelper
{
    public const string TextTooShort = "Text is too short.";
    public const string TextTooLong = "Text is too long.";
    public const string NumberOutOfRange = "Number is out of range.";
    public enum Item
    {
        Stock,
        Comment,
    }

    public static object UserItem(string username, string email, string token)
        => new { Username = username, Email = email, Token = token };

    public static object ItemNotFound(Item item, object identifier)
        => new { Message = $"{item} {identifier} not found." };

    public static object ItemDeleted(Item item, object identifier)
        => new { Message = $"{item} {identifier} deleted." };
  
}