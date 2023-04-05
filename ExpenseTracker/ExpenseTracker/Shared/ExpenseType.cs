/* Shared classes can be referenced by both the Client and Server */
namespace ExpenseTracker.Shared;

public class ExpenseType
{
    public int Id { get; set; }
    public string? Type { get; set; }
}
