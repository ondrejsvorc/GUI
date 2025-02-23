using System.Collections.ObjectModel;

namespace TodoListWpf;

/// <summary>
/// Služba pro správu úkolů.
/// </summary>
public class TaskService(string path = "tasks.json") : ITaskService
{
    /// <inheritdoc/>
    public ObservableCollection<TaskItem> Tasks { get; } = [];

    /// <inheritdoc/>
    public OperationResult AddTask(string title, TaskType type, bool isDone)
    {
        if (string.IsNullOrEmpty(title))
        {
            return OperationResult.Failure("Title cannot be empty.");
        }

        foreach (TaskItem task in Tasks)
        {
            if (task.Title.ToLower() == title.ToLower() && task.Type == type)
            {
                return OperationResult.Failure("Task with this title already exists.");
            }
        }

        TaskItem taskItem = new TaskItem(Guid.NewGuid(), title, type, isDone);
        Tasks.Add(taskItem);
        return OperationResult.Success();
    }

    /// <inheritdoc/>
    public OperationResult UpdateTask(TaskItem task, string title, TaskType type, bool isDone)
    {
        if (task is null)
        {
            return OperationResult.Failure("Task cannot be null.");
        }

        if (string.IsNullOrEmpty(title))
        {
            return OperationResult.Failure("Title cannot be empty.");
        }

        foreach (TaskItem taskItem in Tasks)
        {
            if (task.Title.ToLower() == title.ToLower() && task.Type == type)
            {
                return OperationResult.Failure("Task with this title already exists.");
            }
        }

        int index = Tasks.IndexOf(task);
        if (index == -1)
        {
            return OperationResult.Failure("Task not found.");
        }

        Tasks[index] = new TaskItem(task.Id, title, type, isDone);
        return OperationResult.Success();
    }

    /// <inheritdoc/>
    public OperationResult DeleteTask(TaskItem task)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public OperationResult SaveTasks()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public OperationResult LoadTasks()
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Rozhraní pro správu úkolů.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Kolekce úkolů.
    /// </summary>
    public ObservableCollection<TaskItem> Tasks { get; }

    /// <summary>
    /// Přidá nový úkol.
    /// </summary>
    public OperationResult AddTask(string title, TaskType type, bool isDone);

    /// <summary>
    /// Aktualizuje zadaný úkol.
    /// </summary>
    public OperationResult UpdateTask(TaskItem task, string title, TaskType type, bool isDone);

    /// <summary>
    /// Odstraní zadaný úkol.
    /// </summary>
    public OperationResult DeleteTask(TaskItem task);

    /// <summary>
    /// Uloží úkoly do souboru.
    /// </summary>
    public OperationResult SaveTasks();

    /// <summary>
    /// Načte úkoly ze souboru.
    /// </summary>
    public OperationResult LoadTasks();
}

/// <summary>
/// Reprezentuje výsledek operace, např. při přidávání, aktualizaci či mazání úkolu.
/// Obsahuje informaci o tom, zda operace proběhla úspěšně, a případnou chybovou zprávu, pokud došlo k selhání.
/// </summary>
/// <param name="IsSuccess">Indikuje, zda operace byla úspěšná.</param>
/// <param name="ErrorMessage">Chybová zpráva, pokud operace selhala; jinak null.</param>
public record OperationResult(bool IsSuccess, string? ErrorMessage)
{
    /// <summary>
    /// Vytvoří a vrátí úspěšný výsledek operace.
    /// </summary>
    public static OperationResult Success() => new(true, null);

    /// <summary>
    /// Vytvoří a vrátí neúspěšný výsledek operace s uvedenou chybovou zprávou.
    /// </summary>
    /// <param name="errorMessage">Text chybové zprávy popisující důvod neúspěchu operace.</param>
    public static OperationResult Failure(string errorMessage) => new(false, errorMessage);
}
