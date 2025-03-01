﻿using System.Collections.ObjectModel;
using System.Text.Json;

namespace TodoListWinForms;

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

        if (Tasks.Any(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && x.Type == type))
        {
            return OperationResult.Failure("Task with same title and task type already exists.");
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

        if (task.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && task.Type == type && task.IsDone == isDone)
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

        Tasks[index] = new TaskItem(task.Id, title, type, isDone);
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
public record OperationResult
{
    public bool IsSuccess { get; }

    public string? ErrorMessage { get; }

    /// <summary>
    /// Privátní konstruktor zajišťuje, že v programu existují pouze objekty vytvořené v rámci této třídy.
    /// Aktuálně existují pouze objekty vytvořené statickými metodami .Success a .Failure, které nelze dále upravovat volajícím kódem.
    /// </summary>
    /// <param name="isSuccess">Nastaveno na true v případě úspěchu, na false v případě neúspěchu.</param>
    /// <param name="errorMessage">Nastaveno na null v případě úspěchu, jinak obsahuje popis chyby.</param>
    private OperationResult(bool isSuccess, string? errorMessage) => (IsSuccess, ErrorMessage) = (isSuccess, errorMessage);

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