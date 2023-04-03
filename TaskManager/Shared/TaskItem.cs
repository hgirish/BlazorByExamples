/* Shared classes can be referenced by both the Client and Server */

namespace TaskManager.Shared;

public class TaskItem
{
    public int TaskItemId { get; set; }
    public string? TaskName { get; set; }
    public bool IsComplete { get; set; }
}