using System.ComponentModel;

namespace TodoListWinForms;

public partial class Form1 : Form
{
    private readonly ITaskService _taskService = new TaskService();

    public Form1()
    {
        InitializeComponent();

        // Nastaven� datov�ch zdroj�.
        dataGridTasks.DataSource = new BindingList<TaskItem>(_taskService.Tasks);
        comboBoxTaskType.DataSource = Enum.GetValues(typeof(TaskType));

        // Vypnut� generov�n� jin�ch ne� explicitn� definovan�ch sloupc�.
        dataGridTasks.AutoGenerateColumns = false;

        // Nastaven� v�choz� hodnoty pro ComboBox.
        comboBoxTaskType.SelectedItem = TaskType.Other;
    }

    /// <summary>
    /// P�id� nov� �kol, pokud m� vypln�n� n�zev.
    /// </summary>
    private void AddTask_Click(object sender, EventArgs e)
    {
        RefreshDataGrid();
    }

    /// <summary>
    /// Aktualizuje vybran� �kol s nov�m textem z textov�ho pole.
    /// </summary>
    private void UpdateTask_Click(object sender, EventArgs e)
    {
        RefreshDataGrid();
    }

    /// <summary>
    /// Odstran� vybran� �kol.
    /// </summary>
    private void DeleteTask_Click(object sender, EventArgs e)
    {
        RefreshDataGrid();
    }

    /// <summary>
    /// Zobraz� vybran� �kol ve vstupn�ch prvc�ch p�i zm�n� v�b�ru v DataGridu.
    /// </summary>
    private void DataGridTasks_SelectionChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Ulo�� �koly do souboru.
    /// </summary>
    private void SaveTasks_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Na�te �koly ze souboru.
    /// </summary>
    private void LoadTasks_Click(object sender, EventArgs e)
    {
        RefreshDataGrid();
    }

    /// <summary>
    /// Vynut� p�ekreslen� DataGridu.
    /// Narozd�l od WPF, kde ObservableCollection automaticky informuje o zm�n�, zde nen� podporov�na (resp. musel by se implementovat interface INotifyPropertyChanged).
    /// �e�en�m je tedy znovup�i�azen� aktualizovan�ho seznamu do DataSource, ��m� d�me sign�l o zm�n� datov�ho zdroje, a dojde tak k vykreslen�.
    /// </summary>
    private void RefreshDataGrid()
    {
        dataGridTasks.DataSource = new BindingList<TaskItem>(_taskService.Tasks);
    }
}