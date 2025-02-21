namespace TodoListWpf;

/// <summary>
/// Třída reprezentující model úkolu.
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Název úkolu.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Stav úkolu.
    /// </summary>
    public bool IsDone { get; set; }

    // Přepsání metody ToString pro zobrazení v ListBoxu
    public override string ToString()
    {
        return Title;
    }
}
