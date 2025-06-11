using System.Collections.ObjectModel;
using System.Text.Json;

namespace TodoList.Core;

public class TaskService(string path = "tasks.json") : ITaskService
{
    /// <inheritdoc/>
    public ObservableCollection<TaskItem> Tasks { get; } = [];

    /// <inheritdoc/>
    public OperationResult AddTask(string title, TaskType type, bool isDone, DateTime deadline)
    {
        if (string.IsNullOrEmpty(title))
        {
            return OperationResult.Failure("Title cannot be empty.");
        }

        if (Tasks.Any(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && x.Type == type))
        {
            return OperationResult.Failure("Task with same title and task type already exists.");
        }

        TaskItem taskItem = new TaskItem(Guid.NewGuid(), title, deadline, type, isDone);
        Tasks.Add(taskItem);
        return OperationResult.Success();
    }

    /// <inheritdoc/>
    public OperationResult UpdateTask(TaskItem task, string title, TaskType type, bool isDone, DateTime deadline)
    {
        if (task is null)
        {
            return OperationResult.Failure("Task cannot be null.");
        }

        if (string.IsNullOrEmpty(title))
        {
            return OperationResult.Failure("Title cannot be empty.");
        }

        if (task.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && task.Type == type && task.IsDone == isDone && task.Deadline == deadline)
        {
            return OperationResult.Failure("Task cannot be updated as it was not modified.");
        }

        if (Tasks.Any(x => x != task && x.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && x.Type == type && x.IsDone == isDone))
        {
            return OperationResult.Failure("Task with same properties already exists.");
        }

        int index = Tasks.IndexOf(task);
        if (index == -1)
        {
            return OperationResult.Failure("Task not found.");
        }

        Tasks[index] = new TaskItem(task.Id, title, deadline, type, isDone);
        return OperationResult.Success();
    }

    /// <inheritdoc/>
    public OperationResult DeleteTask(TaskItem task)
    {
        if (task is null)
        {
            return OperationResult.Failure("Task cannot be null.");
        }

        if (!Tasks.Contains(task))
        {
            return OperationResult.Failure("Task not found.");
        }

        Tasks.Remove(task);
        return OperationResult.Success();
    }

    /// <inheritdoc/>
    public OperationResult SaveTasks()
    {
        try
        {
            string json = JsonSerializer.Serialize(Tasks, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(path, json);
        }
        catch
        {
            return OperationResult.Failure("Tasks could not be saved to file.");
        }

        return OperationResult.Success();
    }

    /// <inheritdoc/>
    public OperationResult LoadTasks()
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "[]");
        }

        string json = File.ReadAllText(path);

        ObservableCollection<TaskItem>? tasks = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(json);
        if (tasks is null)
        {
            return OperationResult.Failure("Tasks could not be loaded from file.");
        }

        Tasks.Clear();
        foreach (TaskItem task in tasks)
        {
            Tasks.Add(task);
        }

        return OperationResult.Success();
    }
}

/// <summary>
/// Represents a service that manages tasks.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Represents collection of all tasks.
    /// </summary>
    public ObservableCollection<TaskItem> Tasks { get; }

    /// <summary>
    /// Adds a new task.
    /// </summary>
    /// <param name="title">Task name.</param>
    /// <param name="type">Task type.</param>
    /// <param name="isDone">Task completion status.</param>
    /// <returns>Success or failure result.</returns>
    public OperationResult AddTask(string title, TaskType type, bool isDone, DateTime deadline);

    /// <summary>
    /// Updates an existing task.
    /// </summary>
    /// <param name="task">Task to update.</param>
    /// <param name="title">New task title.</param>
    /// <param name="type">New task type.</param>
    /// <param name="isDone">New task completion status.</param>
    /// <returns>Success or failure result.</returns>
    public OperationResult UpdateTask(TaskItem task, string title, TaskType type, bool isDone, DateTime deadline);

    /// <summary>
    /// Removes a task.
    /// </summary>
    /// <param name="task">Task to remove.</param>
    /// <returns>Success or failure result.</returns>
    public OperationResult DeleteTask(TaskItem task);

    /// <summary>
    /// Saves tasks to storage.
    /// </summary>
    /// <returns>Success or failure result.</returns>
    public OperationResult SaveTasks();

    /// <summary>
    /// Loads tasks from storage.
    /// </summary>
    /// <returns>Success or failure result.</returns>
    public OperationResult LoadTasks();
}

/// <summary>
/// Represents the result of an operation, e.g. when adding, updating, or deleting a task.
/// Contains information about whether the operation was successful, and an optional error message if the operation failed.
/// </summary>
public record OperationResult
{
    public bool IsSuccess { get; }

    public string? ErrorMessage { get; }

    /// <summary>
    /// Private constructor ensures only objects created within class exist throughout the program.
    /// Currently, only objects generated by .Success and .Failure static methods exist and cannot be further modified by the caller.
    /// </summary>
    /// <param name="isSuccess">Set to true if success, set to false if failure.</param>
    /// <param name="errorMessage">Set to null if success, set to a value if failure.</param>
    private OperationResult(bool isSuccess, string? errorMessage) => (IsSuccess, ErrorMessage) = (isSuccess, errorMessage);

    /// <summary>
    /// Creates and returns a successful operation result.
    /// </summary>
    public static OperationResult Success() => new(true, null);

    /// <summary>
    /// Creates and returns a failed operation result with the specified error message.
    /// </summary>
    /// <param name="errorMessage">Error message describing the reason for the operation's failure.</param>
    public static OperationResult Failure(string errorMessage) => new(false, errorMessage);
}