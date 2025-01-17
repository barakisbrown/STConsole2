namespace STConsole2.View;

using STConsole2.Data;
using STConsole2.Model;
using Spectre.Console;

internal static class Menu
{
    private const string X = "Welcome to Sugar Tracker. Blood Sugar Tracker Application";
    private const string V = "Please Select (1-7) OR 0 to exit. :>";
    private static readonly string MenuInputString = V;
    private static readonly string AppNameString = X;
    private static readonly string menu = """
                            
                                    
                                    Main Menu
                                    
                                    What would you like to do?
                                    ---------------------------------------
                                    Type 0 to Close Sugar Tracker App.
                                    Type 1 to View All Readings.
                                    Type 2 to Add Reading
                                    Type 3 to Delete Reading
                                    Type 4 to Update Reading
                                    Type 5 to Show Lifetime Report
                                    Type 6 to Show 30/60/90 Day Report
                                    Type 7 to write reading to a csv file
                                    --------------------------------------
                                    
                            """;
    private static readonly string ValidErrorMessage = "[bold]Must be between 0 and 7[/]";


    internal static int GetMenuAndSelection()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        // DISPLAY APP NAME
        AnsiConsole.WriteLine(AppNameString);
        // DISPLAY MENU
        AnsiConsole.WriteLine(menu);
        var selection = AnsiConsole.Prompt(
            new TextPrompt<int>(MenuInputString)
            .Validate((n) => n switch
            {
                > 7 => ValidationResult.Error(ValidErrorMessage),
                < 0 => ValidationResult.Error(ValidErrorMessage),
                >= 0 => ValidationResult.Success()
            }));


        return selection;
    }

    internal static void Add() { }
    internal static void Delete() { }
    internal static void Update() { }
}
