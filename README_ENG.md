# WPF + WinForms TODO List

This project represents a TODO List, developed using WPF and WinForms.

We will demonstrate the process of creating this application step by step.


## Prerequisites

Before you start development, make sure you have the following tools installed:

1. **Visual Studio 2022**

If you don’t have it, download it from the official website: https://visualstudio.microsoft.com/cs/downloads/

During installation, select "Desktop development with .NET".

![Visual Studio Setup](images/image_2.png)

2. **.NET 8**

The latest version of .NET is required for this project to run.

## Downloading the project

You can download the entire project from GitHub:

1. Open the repository.
2. Click Code → Download ZIP.

![Visual Studio Setup](images/image_1.png)

3. Extract the ZIP file and open the project in Visual Studio.


## Features

### Adding tasks
### Setting task type (Work, University, Personal, Other)
### Setting task status (Completed/Not completed)
### Removing tasks
### Editing tasks
### Saving tasks to a file (JSON)
### Loading tasks from a file (JSON)

# Creating a DoToList in WPF
## Designing the Interface in XAML
Pay attention to indentation. "<" - marks the start of a tag, "/>"" or "</" - marks the end of a tag.

### Window

The Window serves as the main container where we will gradually add and nest other elements. It is the foundation of the application—without it, the desktop application wouldn't be displayed.

- **x:Class** – This refers to the class associated with the XAML file. In our case, it points to the `MainWindow` class within the `TodoListWpf` namespace.
- **xmlns, xmlns:x** – These define the XAML schema. The URLs provide the source of tag definitions. Without them, the XAML tags wouldn't be recognized by the program. The URLs may change when using different libraries.
- **Title** – The title of the application.
- **Height, Width** – Specifies the window's height and width.
- **Background** – Defines the background color of the window in hexadecimal format.
- **FontFamily** – Specifies the font for the main window and all its elements.
- **FontSize** – Determines the font size.
- **ForeGround** – Sets the text color in hexadecimal format.
- **HorizontalAlignment** – Defines the alignment of the application window on the user's screen.
```
<Window 
    x:Class="TodoListWpf.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    Title="TODO List"
    Height="600"
    Width="700"
    Background="#1E1E1E"
    FontFamily="Segoe UI"
    FontSize="14"
    Foreground="#CCCCCC"
    HorizontalAlignment="Center"
    VerticalAlignment="Center">
```

### Grid

The **Grid** is an invisible grid that occupies the entire window space and helps us position elements.  

- **`Grid Margin`** – Defines the margin of the grid, and therefore, the spacing between elements and the window edges.  
- **`Grid.RowDefinitions`** – A paired tag used for directly positioning elements.  

Since we want to divide our window into three horizontal sections, we need to define three rows where our elements will be placed:  

1. **First RowDefinition** – `Height="*"` means that the row will take up **1/n** of the window space, where `n` is the total number of rows. In our case, it will occupy **one-third** of the window space.  
2. **Second and Third Rows** – `Height="Auto"` means that the row height will automatically adjust based on the content.
```    
    <Grid Margin="10">
        <Grid.RowDefinitions>
            <RowDefinition 
                Height="*"/>
            <RowDefinition 
                Height="Auto"/>
            <RowDefinition 
                Height="Auto"/>
        </Grid.RowDefinitions>
```

### DataGrid

The **DataGrid** is a table where we will display the list of tasks.  

- **`x:Name`** – The name of a specific element. By naming the tag, we can later access it in the logic part of the program (`MainWindow`) as a C# object.  
- **`Grid.Row`** – Specifies the row from the previous code block where the element will be located (indexing starts from `0`).  
- **`AutoGenerateColumns`** – Determines whether columns should be automatically generated based on the data collection provided. Since we only need to display specific properties of the `TaskItem` class (which we will create later), this is set to `False`.  
- **`CanUserAddRows`** – Allows users to directly add records to the table. In our case, records will be added using different methods, so this is set to `False`.  
- **`IsReadOnly`** – Defines whether users can edit the table directly. Since modifications will be handled differently, this is set to `True`.  
- **`BorderBrush`** – Sets the border color of the table.  
- **`BorderThickness`** – Defines the border width of the table.  
- **`SelectionChanged`** – Specifies a method to handle the event when a user selects a row. If no row is selected, the value is `null`. We will implement this method later.  

```
        <DataGrid 
            x:Name="dataGridTasks"
            Grid.Row="0"
            AutoGenerateColumns="False"
            CanUserAddRows="False"
            IsReadOnly="True"
            Background="#2D2D30"
            Foreground="#CCCCCC"
            BorderBrush="#3E3E42"
            BorderThickness="1"
            SelectionChanged="DataGridTasks_SelectionChanged">
```

**DataGrid.Resources** defines where the **DataGrid** should retrieve style definitions from.  

- **`Style`** – A tag for defining styles (acts as a 'style command').  
- **`TargetType`** – Specifies what will be styled.  
- **`Setter Property, Value`** – Defines what property should be set and to what value.  
- **`Style.Triggers`** – A style trigger.  

Within the **`Style.Triggers`** tags, we can define individual triggers.  
For example, if the **`IsSelected`** property of a **DataGrid** cell is `true` (meaning the cell is selected),  
then the trigger will automatically change the **border color** to **yellow**.  
```
             <DataGrid.Resources>
                <Style TargetType="DataGridColumnHeader">
                    <Setter Property="Background" Value="#3E3E42"/>
                    <Setter Property="Foreground" Value="#CCCCCC"/>
                    <Setter Property="BorderBrush" Value="#565656"/>
                </Style>
                <Style TargetType="DataGridCell">
                    <Setter Property="Background" Value="#2D2D30"/>
                    <Setter Property="Foreground" Value="#CCCCCC"/>
                    <Setter Property="BorderBrush" Value="#565656"/>
                    <Style.Triggers>
                        <Trigger Property="IsSelected" Value="True">
                            <Setter Property="BorderBrush" Value="Yellow"/>
                        </Trigger>
                    </Style.Triggers>
                </Style>
                <Style TargetType="DataGridRow">
                    <Setter Property="Background" Value="#2D2D30"/>
                    <Setter Property="Foreground" Value="#CCCCCC"/>
                    <Setter Property="BorderBrush" Value="#565656"/>
                </Style>
                              </DataGrid.Resources>
```


 **DataGrid.Columns** defines the columns of the table.  

- **`DataGridTextColumn`** – Defines a column that contains text.  
- **`DataGridCheckBoxColumn`** – Defines a column that contains checkboxes.  
- **`Binding`** – Determines whether the table is bound to a collection of objects.  

In our case, the **DataGrid** is bound to a **task list**, which is a collection of objects of the `TaskItem` class.  
Each `TaskItem` has the following attributes:  
- **`Title (string)`**  
- **`Type (string)`**  
- **`Done (CheckBox, i.e., boolean + null)`**  

The **DataGrid** recognizes the list of `TaskItem` objects using **binding** and automatically organizes them based on the corresponding column headers and attributes.  

```
              <DataGrid.Columns>
                <!-- Sloupec pro název úkolu -->
                <DataGridTextColumn 
                    Header="Title" 
                    Binding="{Binding Title}"
                    Width="*"/>

                <!-- Sloupec pro typ úkolu -->
                <DataGridTextColumn 
                    Header="Type" 
                    Binding="{Binding Type}"
                    Width="150"/>

                <!-- Sloupec pro stav úkolu -->
                <DataGridCheckBoxColumn 
                    Header="Done" 
                    Binding="{Binding IsDone}"
                    Width="100"/>
                </DataGrid.Columns>
        </DataGrid>

```

### StackPanel

Similar to **Grid**, it is used for positioning elements. However, it is **one-dimensional**,  
aligning elements either **vertically** (one below another) or **horizontally** (side by side) using the **`Orientation`** property.  

- **`Margin`** – Defines spacing (in this order: **left, top, right, bottom**).  
- **`TextBox`** – A text input field for users.  
- **`ToolTip`** – A tooltip providing guidance to the user on how to use the text field.  
  - The tooltip appears when the user hovers over the text field and holds the mouse there for a moment (no need to click).  
- **`ComboBox`** – A dropdown list of selectable options.  
- **`ComboBoxItem`** – Items in the ComboBox can be defined directly in XAML.  
  - However, we **will not** use this method, as our options will be loaded dynamically using the `TaskType` enumeration,  
    which serves as the data source (we will implement this later).  
- **`CheckBox`** – A checkbox input.  

```
        <StackPanel 
            Grid.Row="1"
            Orientation="Vertical"
            Margin="0,10,0,0">

            <!-- Panel se vstupními prvky -->
            <StackPanel 
                Orientation="Horizontal"
                HorizontalAlignment="Center"
                Margin="0,0,0,5">

                <!-- TextBox pro název úkolu -->
                <TextBox 
                    x:Name="textBoxTask"
                    Width="200"
                    Height="25"
                    Margin="5"
                    ToolTip="Enter a new or updated task"
                    Background="#2D2D30"
                    Foreground="#CCCCCC"
                    BorderBrush="#3E3E42"
                    BorderThickness="1"/>

                <!-- ComboBox pro výběr typu úkolu -->
                <ComboBox 
                    x:Name="comboBoxTaskType"
                    Width="150"
                    Height="25"
                    Margin="5"
                    Background="#CCCCCC"
                    Foreground="#2D2D30"
                    BorderBrush="#3E3E42"
                    BorderThickness="1"
                    ToolTip="Select task type">
                </ComboBox>

                <!-- CheckBox pro nastavení stavu úkolu -->
                <CheckBox 
                    x:Name="checkBoxIsDone"
                    Content="Done"
                    VerticalAlignment="Center"
                    Margin="5"
                    Foreground="#CCCCCC"/>
            </StackPanel>

```

**Button** - a button for user interaction.  

- **`Content`** – Defines the text displayed on the button.  
- **`Click`** – Specifies the event triggered when the button is clicked.  
- **`Cursor`** – Determines the mouse cursor icon when hovering over the button.  

```
 <!-- Panel pro tlačítka (přidat/aktualizovat/smazat úkol) -->
            <StackPanel 
                Orientation="Horizontal"
                HorizontalAlignment="Center">

                <Button 
                    Content="Add New Task"
                    Click="AddTask_Click"
                    Height="50"
                    Width="150"
                    Margin="10,0"
                    FontSize="16"
                    FontWeight="SemiBold"
                    Background="#3E3E42"
                    Foreground="#CCCCCC"
                    BorderBrush="#565656"
                    Cursor="Hand"/>

                <Button 
                    Content="Update Task"
                    Click="UpdateTask_Click"
                    Height="50"
                    Width="150"
                    Margin="10,0"
                    FontSize="16"
                    FontWeight="SemiBold"
                    Background="#3E3E42"
                    Foreground="#CCCCCC"
                    BorderBrush="#565656"
                    Cursor="Hand"/>

                <Button 
                    Content="Delete Task"
                    Click="DeleteTask_Click"
                    Height="50"
                    Width="150"
                    Margin="10,0"
                    FontSize="16"
                    FontWeight="SemiBold"
                    Background="#3E3E42"
                    Foreground="#CCCCCC"
                    BorderBrush="#565656"
                    Cursor="Hand"/>
            </StackPanel>
        </StackPanel>

        <!-- Panel pro operace se soubory -->
        <StackPanel 
            Grid.Row="2"
            Orientation="Horizontal"
            HorizontalAlignment="Center"
            Margin="0,10,0,0">

            <Button 
                Content="Save to File"
                Click="SaveTasks_Click"
                Height="50"
                Width="150"
                Margin="10,0"
                FontSize="16"
                FontWeight="SemiBold"
                Background="#3E3E42"
                Foreground="#CCCCCC"
                BorderBrush="#565656"
                Cursor="Hand"/>

            <Button 
                Content="Load from File"
                Click="LoadTasks_Click"
                Height="50"
                Width="150"
                Margin="10,0"
                FontSize="16"
                FontWeight="SemiBold"
                Background="#3E3E42"
                Foreground="#CCCCCC"
                BorderBrush="#565656"
                Cursor="Hand"/>
        </StackPanel>
    </Grid>
</Window>
```

