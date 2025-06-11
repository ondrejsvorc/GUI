using System.Windows;
using System.Windows.Controls;
using TodoList.Core;

namespace TodoListWpf;

/// <summary>
/// Interakční logika pro MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
{
    private readonly ITaskService _taskService = new TaskService();

    public MainWindow()
    {
        InitializeComponent();

        // Nastavení datových zdrojů.
        dataGridTasks.ItemsSource = _taskService.Tasks;
        comboBoxTaskType.ItemsSource = Enum.GetValues(typeof(TaskType));

        // Nastavení výchozí hodnoty pro ComboBox.
        comboBoxTaskType.SelectedItem = TaskType.Other;
    }

    /// <summary>
    /// Přidá nový úkol, pokud má vyplněný název.
    /// </summary>
    private void AddTask_Click(object sender, RoutedEventArgs e)
    {
        string title = textBoxTask.Text.Trim();
        TaskType type = (TaskType)comboBoxTaskType.SelectedItem;
        bool isDone = checkBoxIsDone.IsChecked == true;
        DateTime? deadline = datePickerDeadline.SelectedDate;

        if (deadline is null)
        {
            MessageBox.Show("Please select a deadline.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        OperationResult result = _taskService.AddTask(title, type, isDone, (DateTime)deadline);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        textBoxTask.Clear();
    }

    /// <summary>
    /// Aktualizuje vybraný úkol s novým textem z textového pole.
    /// </summary>
    private void UpdateTask_Click(object sender, RoutedEventArgs e)
    {
        if (dataGridTasks.SelectedItem is not TaskItem task)
        {
            MessageBox.Show("Select task to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        string title = textBoxTask.Text.Trim();
        TaskType type = (TaskType)comboBoxTaskType.SelectedItem;
        bool isDone = checkBoxIsDone.IsChecked == true;
        DateTime? deadline = datePickerDeadline.SelectedDate;

        if (deadline is null)
        {
            MessageBox.Show("Please select a deadline.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        OperationResult result = _taskService.UpdateTask(task, title, type, isDone, (DateTime)deadline);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        textBoxTask.Clear();
    }

    /// <summary>
    /// Odstraní vybraný úkol.
    /// </summary>
    private void DeleteTask_Click(object sender, RoutedEventArgs e)
    {
        if (dataGridTasks.SelectedItem is not TaskItem task)
        {
            MessageBox.Show("Select task to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        OperationResult result = _taskService.DeleteTask(task);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        textBoxTask.Clear();
    }

    /// <summary>
    /// Zobrazí vybraný úkol ve vstupních prvcích při změně výběru v DataGridu.
    /// </summary>
    private void DataGridTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dataGridTasks.SelectedItem is null)
        {
            return;
        }

        TaskItem task = (TaskItem)dataGridTasks.SelectedItem;

        textBoxTask.Text = task.Title;
        comboBoxTaskType.SelectedItem = task.Type;
        checkBoxIsDone.IsChecked = task.IsDone;
        datePickerDeadline.SelectedDate = task.Deadline;
    }

    /// <summary>
    /// Uloží úkoly do souboru.
    /// </summary>
    private void SaveTasks_Click(object sender, RoutedEventArgs e)
    {
        OperationResult result = _taskService.SaveTasks();
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    /// <summary>
    /// Načte úkoly ze souboru.
    /// </summary>
    private void LoadTasks_Click(object sender, RoutedEventArgs e)
    {
        OperationResult result = _taskService.LoadTasks();
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}