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
        User
    }

    public static object ItemNotFound(Item item, object identifier)
        => new { Message = $"{item} {identifier} not found." };

    public static object ItemDeleted(Item item, object identifier)
        => new { Message = $"{item} {identifier} deleted." };

    public static object ItemCreated(Item item, object identifier)
        => new { Message = $"{item} {identifier} created." };
        
}