### GUI

# WPF + WinForms TODO List

Tento projekt představuje TODO List, vyvinutý pomocí WPF a WinForms.
Budeme demonstrovat proces tvorby této aplikace krok za krokem.


## Požadavky před zahájením práce

Než začnete s vývojem, ujistěte se, že máte nainstalovány následující nástroje:

1. **Visual Studio**

Pokud jej nemáte, stáhněte si ho z oficiálních stránek: https://visualstudio.microsoft.com/cs/downloads/

Při instalaci vyberte "Vývoj desktopových aplikací pomocí .NET".

![Visual Studio Setup](images/image_2.png)

2. **.NET 8**

Podpora nejnovější verze .NET je vyžadována pro běh projektu.


## Stažení projektu

Celý náš hotový projekt si můžete stáhnout z GitHubu:

1. Otevřete repozitář.
2. Klikněte na Code → Download ZIP.

![Visual Studio Setup](images/image_1.png)

3. Rozbalte soubor a otevřete projekt ve Visual Studiu.


## Funkce

### Přidávání úkolů
### Nastavení typu úkolu (pracovní, univerzitní, osobní, jiný)
### Nastavení stavu úkolu (dokončený/nedokončený)
### Odebírání úkolů
### Úprava úkolů
### ukládání úkolů do souboru (JSON)
### Načítání úkolů ze souboru (JSON)

#Tvorba DoToList ve WPF
##Návrh rozhraní v XAML
###Window
Tvoří základní prostor, na který budeme postupně přídavat naše další prvky (vnořovat). Jde o základ, bez kterého by se desktopová aplikace nezobrazila. "x:Class" zde se nachazi  odkaz na třídu, se kterou je xaml soubor spojený, v našem případe jde o třídu MainWindow ve jmenném prostoru TodoListWpf. "xmlns, xmlns: x"– inforamce o xaml schéma. Url odkayz poskytují zdroj definic tagů. Bez nich náš program XAML tagy nerozezná. URL odkazy se mohou měnit v případě užití jiných knihoven. "title" – nadpis aplikace. "height, width" – šířka a výška okna. "Background" - Barva základního okna v hexadecimálním zápisu. "FontFamily" - font písma na základním okně a všech jeho prvcích. "FontSize" - velikost písma/fontu. "ForeGround" - barva textu písma v hexadecimáním zápisu. "HorizontalAlignment" - zarovnání okna aplikace na uživatelské obrazovce.
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
###Grid
Grid je neviditelná mřížka, která zabírá celý prostor okna a pomáhá nam pozicovat prvky. "Grid Margin" - odsazení mřížky, a tudíž i prvků, od okrajů okna. "Grid.RowDefinitions" - párovy tag, slouží k přímému pozicování prvků. Jelikož naše okno chceme rozdělit na tři horizontální části, tak potřebujeme nadefinovat tři řádky, ve kterých budou naše prvky. 1. RowDefinition - "Height" = *, znamená, že chceme aby výška řádku zabírala 1/n prostoru okna, kde n = počet řádku, v našem případě jedna třetina protoru. 2. a 3. řádek, "Height" = Auto - výška řádku se automaticky přizpůsobí podle obsahu. 
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
###DataGrid


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
