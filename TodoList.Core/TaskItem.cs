namespace TodoList.Core;

/// <summary>
/// Represents a task.
/// </summary>
/// <param name="Id">Task identifier.</param>
/// <param name="Title">Task title.</param>
/// <param name="Type">Task type.</param>
/// <param name="IsDone">Task completion status.</param>
public record TaskItem(Guid Id, string Title, TaskType Type = TaskType.Other, bool IsDone = false);

/// <summary>
/// Represents the task type.
/// </summary>
public enum TaskType { Work = 0, University = 1, Personal = 2, Other = 3 }
