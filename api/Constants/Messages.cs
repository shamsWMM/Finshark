namespace api.Constants;

public static class Messages
{
    public enum Item
    {
        Stock,
        Comment
    }

    public static object ItemNotFound(Item item, int id)
        => new { Message = $"{item} with id {id} not found." };

    public static object ItemDeleted(Item item, int id)
        => new { Message = $"{item} with id {id} deleted." };
}