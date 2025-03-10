using System.ComponentModel;
using TodoList.Core;

namespace TodoListWinForms;

public partial class Form1 : Form
{
    private readonly ITaskService _taskService = new TaskService();

    public Form1()
    {
        InitializeComponent();

        // Nastavení datových zdrojù.
        dataGridTasks.DataSource = new BindingList<TaskItem>(_taskService.Tasks);
        dataGridTasks.AutoGenerateColumns = false;
        comboBoxTaskType.DataSource = Enum.GetValues(typeof(TaskType));

        // Nastavení výchozí hodnoty pro ComboBox.
        comboBoxTaskType.SelectedItem = TaskType.Other;
    }

    /// <summary>
    /// Pøidá nový úkol, pokud má vyplnìný název.
    /// </summary>
    private void AddTask_Click(object sender, EventArgs e)
    {
        string title = textBoxTask.Text.Trim();
        TaskType type = (TaskType)comboBoxTaskType.SelectedItem!;
        bool isDone = checkBoxIsDone.Checked;

        OperationResult result = _taskService.AddTask(title, type, isDone);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        RefreshDataGrid();
        textBoxTask.Clear();
    }

    /// <summary>
    /// Aktualizuje vybraný úkol s novým textem z textového pole.
    /// </summary>
    private void UpdateTask_Click(object sender, EventArgs e)
    {
        if (dataGridTasks.CurrentRow?.DataBoundItem is not TaskItem task)
        {
            MessageBox.Show("Select a task to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string title = textBoxTask.Text.Trim();
        TaskType type = (TaskType)comboBoxTaskType.SelectedItem!;
        bool isDone = checkBoxIsDone.Checked;

        OperationResult result = _taskService.UpdateTask(task, title, type, isDone);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        RefreshDataGrid();
        textBoxTask.Clear();
    }

    /// <summary>
    /// Odstraní vybraný úkol.
    /// </summary>
    private void DeleteTask_Click(object sender, EventArgs e)
    {
        if (dataGridTasks.CurrentRow?.DataBoundItem is not TaskItem task)
        {
            MessageBox.Show("Select a task to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        OperationResult result = _taskService.DeleteTask(task);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        RefreshDataGrid();
        textBoxTask.Clear();
    }

    /// <summary>
    /// Zobrazí vybraný úkol ve vstupních prvcích pøi zmìnì výbìru v DataGridu.
    /// </summary>
    private void DataGridTasks_SelectionChanged(object sender, EventArgs e)
    {
        if (dataGridTasks.CurrentRow?.Index >= _taskService.Tasks.Count)
        {
            return;
        }

        if (dataGridTasks.CurrentRow?.DataBoundItem is not TaskItem selectedTask)
        {
            return;
        }

        textBoxTask.Text = selectedTask.Title;
        comboBoxTaskType.SelectedItem = selectedTask.Type;
        checkBoxIsDone.Checked = selectedTask.IsDone;
    }

    /// <summary>
    /// Uloží úkoly do souboru.
    /// </summary>
    private void SaveTasks_Click(object sender, EventArgs e)
    {
        OperationResult result = _taskService.SaveTasks();
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
    }

    /// <summary>
    /// Naète úkoly ze souboru.
    /// </summary>
    private void LoadTasks_Click(object sender, EventArgs e)
    {
        OperationResult result = _taskService.LoadTasks();
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        RefreshDataGrid();
    }

    private void RefreshDataGrid()
    {
        dataGridTasks.DataSource = new BindingList<TaskItem>(_taskService.Tasks);
    }
}