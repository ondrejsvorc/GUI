namespace TodoListWinForms;

partial class Form1
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    // Declare controls matching the XAML names.
    private System.Windows.Forms.DataGridView dataGridTasks;
    private System.Windows.Forms.TextBox textBoxTask;
    private System.Windows.Forms.ComboBox comboBoxTaskType;
    private System.Windows.Forms.CheckBox checkBoxIsDone;
    private System.Windows.Forms.Button btnAddTask;
    private System.Windows.Forms.Button btnUpdateTask;
    private System.Windows.Forms.Button btnDeleteTask;
    private System.Windows.Forms.Button btnSaveTasks;
    private System.Windows.Forms.Button btnLoadTasks;
    private System.Windows.Forms.Panel panelInput;
    private System.Windows.Forms.Panel panelCRUD;
    private System.Windows.Forms.Panel panelFileOps;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support – do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        dataGridTasks = new DataGridView();
        textBoxTask = new TextBox();
        comboBoxTaskType = new ComboBox();
        checkBoxIsDone = new CheckBox();
        btnAddTask = new Button();
        btnUpdateTask = new Button();
        btnDeleteTask = new Button();
        btnSaveTasks = new Button();
        btnLoadTasks = new Button();
        panelInput = new Panel();
        panelCRUD = new Panel();
        panelFileOps = new Panel();
        ((System.ComponentModel.ISupportInitialize)dataGridTasks).BeginInit();
        panelInput.SuspendLayout();
        panelCRUD.SuspendLayout();
        panelFileOps.SuspendLayout();
        SuspendLayout();
        // 
        // dataGridTasks
        // 
        dataGridTasks.BackgroundColor = Color.FromArgb(45, 45, 48);
        dataGridTasks.AutoGenerateColumns = false;
        // Title Column
        var titleCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        titleCol.HeaderText = "Title";
        titleCol.DataPropertyName = "Title";
        titleCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        // Type Column
        var typeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        typeCol.HeaderText = "Type";
        typeCol.DataPropertyName = "Type";
        typeCol.Width = 150;
        // Done Column
        var doneCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        doneCol.HeaderText = "Done";
        doneCol.DataPropertyName = "IsDone";
        doneCol.Width = 100;
        dataGridTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { titleCol, typeCol, doneCol });
        dataGridTasks.Location = new Point(10, 10);
        dataGridTasks.Name = "dataGridTasks";
        dataGridTasks.ReadOnly = true;
        dataGridTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridTasks.Size = new Size(680, 381);
        dataGridTasks.TabIndex = 0;
        dataGridTasks.SelectionChanged += DataGridTasks_SelectionChanged;
        // 
        // textBoxTask
        // 
        textBoxTask.BackColor = Color.FromArgb(45, 45, 48);
        textBoxTask.BorderStyle = BorderStyle.FixedSingle;
        textBoxTask.ForeColor = Color.FromArgb(204, 204, 204);
        textBoxTask.Location = new Point(109, 10);
        textBoxTask.Name = "textBoxTask";
        textBoxTask.Size = new Size(200, 32);
        textBoxTask.TabIndex = 0;
        // 
        // comboBoxTaskType
        // 
        comboBoxTaskType.BackColor = Color.FromArgb(204, 204, 204);
        comboBoxTaskType.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBoxTaskType.ForeColor = Color.FromArgb(45, 45, 48);
        comboBoxTaskType.Location = new Point(319, 10);
        comboBoxTaskType.Name = "comboBoxTaskType";
        comboBoxTaskType.Size = new Size(150, 33);
        comboBoxTaskType.TabIndex = 1;
        // 
        // checkBoxIsDone
        // 
        checkBoxIsDone.BackColor = Color.Transparent;
        checkBoxIsDone.ForeColor = Color.FromArgb(204, 204, 204);
        checkBoxIsDone.Location = new Point(479, 10);
        checkBoxIsDone.Name = "checkBoxIsDone";
        checkBoxIsDone.Size = new Size(100, 32);
        checkBoxIsDone.TabIndex = 2;
        checkBoxIsDone.Text = "Done";
        checkBoxIsDone.UseVisualStyleBackColor = false;
        // 
        // btnAddTask
        // 
        btnAddTask.BackColor = Color.FromArgb(62, 62, 66);
        btnAddTask.Cursor = Cursors.Hand;
        btnAddTask.FlatAppearance.BorderColor = Color.FromArgb(86, 86, 86);
        btnAddTask.FlatStyle = FlatStyle.Flat;
        btnAddTask.Font = new Font("Segoe UI", 16F);
        btnAddTask.ForeColor = Color.FromArgb(204, 204, 204);
        btnAddTask.Location = new Point(79, 5);
        btnAddTask.Name = "btnAddTask";
        btnAddTask.Size = new Size(166, 50);
        btnAddTask.TabIndex = 0;
        btnAddTask.Text = "Add New Task";
        btnAddTask.UseVisualStyleBackColor = false;
        btnAddTask.Click += AddTask_Click;
        // 
        // btnUpdateTask
        // 
        btnUpdateTask.BackColor = Color.FromArgb(62, 62, 66);
        btnUpdateTask.Cursor = Cursors.Hand;
        btnUpdateTask.FlatAppearance.BorderColor = Color.FromArgb(86, 86, 86);
        btnUpdateTask.FlatStyle = FlatStyle.Flat;
        btnUpdateTask.Font = new Font("Segoe UI", 16F);
        btnUpdateTask.ForeColor = Color.FromArgb(204, 204, 204);
        btnUpdateTask.Location = new Point(253, 5);
        btnUpdateTask.Name = "btnUpdateTask";
        btnUpdateTask.Size = new Size(166, 50);
        btnUpdateTask.TabIndex = 1;
        btnUpdateTask.Text = "Update Task";
        btnUpdateTask.UseVisualStyleBackColor = false;
        btnUpdateTask.Click += UpdateTask_Click;
        // 
        // btnDeleteTask
        // 
        btnDeleteTask.BackColor = Color.FromArgb(62, 62, 66);
        btnDeleteTask.Cursor = Cursors.Hand;
        btnDeleteTask.FlatAppearance.BorderColor = Color.FromArgb(86, 86, 86);
        btnDeleteTask.FlatStyle = FlatStyle.Flat;
        btnDeleteTask.Font = new Font("Segoe UI", 16F);
        btnDeleteTask.ForeColor = Color.FromArgb(204, 204, 204);
        btnDeleteTask.Location = new Point(427, 5);
        btnDeleteTask.Name = "btnDeleteTask";
        btnDeleteTask.Size = new Size(166, 50);
        btnDeleteTask.TabIndex = 2;
        btnDeleteTask.Text = "Delete Task";
        btnDeleteTask.UseVisualStyleBackColor = false;
        btnDeleteTask.Click += DeleteTask_Click;
        // 
        // btnSaveTasks
        // 
        btnSaveTasks.BackColor = Color.FromArgb(62, 62, 66);
        btnSaveTasks.Cursor = Cursors.Hand;
        btnSaveTasks.FlatAppearance.BorderColor = Color.FromArgb(86, 86, 86);
        btnSaveTasks.FlatStyle = FlatStyle.Flat;
        btnSaveTasks.Font = new Font("Segoe UI", 16F);
        btnSaveTasks.ForeColor = Color.FromArgb(204, 204, 204);
        btnSaveTasks.Location = new Point(156, 5);
        btnSaveTasks.Name = "btnSaveTasks";
        btnSaveTasks.Size = new Size(166, 50);
        btnSaveTasks.TabIndex = 0;
        btnSaveTasks.Text = "Save to File";
        btnSaveTasks.UseVisualStyleBackColor = false;
        btnSaveTasks.Click += SaveTasks_Click;
        // 
        // btnLoadTasks
        // 
        btnLoadTasks.BackColor = Color.FromArgb(62, 62, 66);
        btnLoadTasks.Cursor = Cursors.Hand;
        btnLoadTasks.FlatAppearance.BorderColor = Color.FromArgb(86, 86, 86);
        btnLoadTasks.FlatStyle = FlatStyle.Flat;
        btnLoadTasks.Font = new Font("Segoe UI", 16F);
        btnLoadTasks.ForeColor = Color.FromArgb(204, 204, 204);
        btnLoadTasks.Location = new Point(330, 5);
        btnLoadTasks.Name = "btnLoadTasks";
        btnLoadTasks.Size = new Size(166, 50);
        btnLoadTasks.TabIndex = 1;
        btnLoadTasks.Text = "Load from File";
        btnLoadTasks.UseVisualStyleBackColor = false;
        btnLoadTasks.Click += LoadTasks_Click;
        // 
        // panelInput
        // 
        panelInput.Controls.Add(textBoxTask);
        panelInput.Controls.Add(comboBoxTaskType);
        panelInput.Controls.Add(checkBoxIsDone);
        panelInput.Location = new Point(10, 397);
        panelInput.Name = "panelInput";
        panelInput.Size = new Size(680, 50);
        panelInput.TabIndex = 1;
        // 
        // panelCRUD
        // 
        panelCRUD.Controls.Add(btnAddTask);
        panelCRUD.Controls.Add(btnUpdateTask);
        panelCRUD.Controls.Add(btnDeleteTask);
        panelCRUD.Location = new Point(10, 457);
        panelCRUD.Name = "panelCRUD";
        panelCRUD.Size = new Size(680, 60);
        panelCRUD.TabIndex = 2;
        // 
        // panelFileOps
        // 
        panelFileOps.Controls.Add(btnSaveTasks);
        panelFileOps.Controls.Add(btnLoadTasks);
        panelFileOps.Location = new Point(10, 527);
        panelFileOps.Name = "panelFileOps";
        panelFileOps.Size = new Size(680, 60);
        panelFileOps.TabIndex = 3;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(11F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(30, 30, 30);
        ClientSize = new Size(700, 600);
        Controls.Add(dataGridTasks);
        Controls.Add(panelInput);
        Controls.Add(panelCRUD);
        Controls.Add(panelFileOps);
        Font = new Font("Segoe UI", 14F);
        ForeColor = Color.FromArgb(204, 204, 204);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "TODO List";
        ((System.ComponentModel.ISupportInitialize)dataGridTasks).EndInit();
        panelInput.ResumeLayout(false);
        panelInput.PerformLayout();
        panelCRUD.ResumeLayout(false);
        panelFileOps.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
}