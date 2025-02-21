using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TodoListWpf;

/// <summary>
/// Interakční logika pro MainWindow.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Kolekce úkolů, která automaticky aktualizuje UI při změně.
    /// </summary>
    private readonly ObservableCollection<TodoItem> Tasks = [];

    public MainWindow()
    {
        InitializeComponent();

        // Nastavení datového zdroje ListBoxu
        listBoxTasks.ItemsSource = Tasks;
    }

    /// <summary>
    /// Přidá nový úkol do kolekce, pokud není text prázdný.
    /// </summary>
    private void AddTask_Click(object sender, RoutedEventArgs e)
    {

    }

    /// <summary>
    /// Aktualizuje vybraný úkol s novým textem z textového pole.
    /// </summary>
    private void UpdateTask_Click(object sender, RoutedEventArgs e)
    {

    }

    /// <summary>
    /// Odstraní vybraný úkol z kolekce.
    /// </summary>
    private void DeleteTask_Click(object sender, RoutedEventArgs e)
    {

    }

    /// <summary>
    /// Při změně výběru v ListBoxu zobrazí vybraný úkol v textovém poli.
    /// </summary>
    private void ListBoxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    /// <summary>
    /// Uloží úkoly do souboru.
    /// </summary>
    private void SaveTasks_Click(object sender, RoutedEventArgs e)
    {

    }

    /// <summary>
    /// Načtění úkolů ze souboru.
    /// </summary>
    private void LoadTasks_Click(object sender, RoutedEventArgs e)
    {

    }
}