using System.ComponentModel;

namespace TodoListWinForms;

public partial class Form1 : Form
{
    private readonly ITaskService _taskService = new TaskService();

    public Form1()
    {
        InitializeComponent();

        // Nastavení datových zdrojù.
        dataGridTasks.DataSource = new BindingList<TaskItem>(_taskService.Tasks);
        comboBoxTaskType.DataSource = Enum.GetValues(typeof(TaskType));

        // Vypnutí generování jiných než explicitnì definovaných sloupcù.
        dataGridTasks.AutoGenerateColumns = false;

        // Nastavení výchozí hodnoty pro ComboBox.
        comboBoxTaskType.SelectedItem = TaskType.Other;
    }

    /// <summary>
    /// Pøidá nový úkol, pokud má vyplnìný název.
    /// </summary>
    private void AddTask_Click(object sender, EventArgs e)
    {
        RefreshDataGrid();
    }

    /// <summary>
    /// Aktualizuje vybraný úkol s novým textem z textového pole.
    /// </summary>
    private void UpdateTask_Click(object sender, EventArgs e)
    {
        RefreshDataGrid();
    }

    /// <summary>
    /// Odstraní vybraný úkol.
    /// </summary>
    private void DeleteTask_Click(object sender, EventArgs e)
    {
        RefreshDataGrid();
    }

    /// <summary>
    /// Zobrazí vybraný úkol ve vstupních prvcích pøi zmìnì výbìru v DataGridu.
    /// </summary>
    private void DataGridTasks_SelectionChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Uloží úkoly do souboru.
    /// </summary>
    private void SaveTasks_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Naète úkoly ze souboru.
    /// </summary>
    private void LoadTasks_Click(object sender, EventArgs e)
    {
        RefreshDataGrid();
    }

    /// <summary>
    /// Vynutí pøekreslení DataGridu.
    /// Narozdíl od WPF, kde ObservableCollection automaticky informuje o zmìnì, zde není podporována (resp. musel by se implementovat interface INotifyPropertyChanged).
    /// Øešením je tedy znovupøiøazení aktualizovaného seznamu do DataSource, èímž dáme signál o zmìnì datového zdroje, a dojde tak k vykreslení.
    /// </summary>
    private void RefreshDataGrid()
    {
        dataGridTasks.DataSource = new BindingList<TaskItem>(_taskService.Tasks);
    }
}