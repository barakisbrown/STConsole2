namespace STConsole2.View;

using STConsole2.Data;
using STConsole2.Model;
using STConsole2.Input;
using Spectre.Console;

internal static class Menu
{
    private const int SleepAmount = 1000;
    private const string X = "Welcome to Sugar Tracker. Blood Sugar Tracker Application";
    private const string V = "Please Select (1-6) OR 0 to exit. :>";
    private static readonly string MenuInputString = V;
    private static readonly string AppNameString = X;
    private static readonly string menu = """
                            
                                    
                                    Main Menu
                                    
                                    What would you like to do?
                                    ---------------------------------------
                                    Type 0 to Close Sugar Tracker App.
                                    Type 1 to View All Readings
                                    Type 2 to Add Reading
                                    Type 3 to Delete Reading
                                    Type 4 to Update Reading
                                    Type 5 to Show 30/60/90 Day Report
                                    Type 6 to write reading to a csv file
                                    --------------------------------------
                                    
                            """;
    private static readonly string MenuValidErrorMessage = "[bold]Must be between 0 and 6[/]";
    


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
                > 6 => ValidationResult.Error(MenuValidErrorMessage),
                < 0 => ValidationResult.Error(MenuValidErrorMessage),
                >= 0 => ValidationResult.Success()
            }));


        return selection;
    }

    internal static void Add() 
    {
        AnsiConsole.Clear();
        // INSERT INTO READINGS(Amount,Added)
        AnsiConsole.WriteLine("Adding a new reading to my tracker.");
        // AMOUNT
        var amount = Helper.GetAmount();
        // DATE ADDED
        var added = Helper.GetDate();       
        // ECHO THE AMOUNT AND ADDED BACK TO THE USER
        AnsiConsole.WriteLine($"You entered {amount} on the following date => {added}");
        // Confirm to insert or decline to reset and do it again
        if (Helper.YesNoPrompt())
        {
            // INSERT
            var reading = new Reading()
            {
                Amount = amount,
                Added = added
            };

            var data = new DbAccess();
            bool success = data.InsertReading(reading);
            if (success)
            {
                AnsiConsole.WriteLine("[bold]Succesfully added new reading.[/]");
                Thread.Sleep(SleepAmount);
            }
            else
            {
                AnsiConsole.WriteLine("[red]Error adding new reading.[/]");
                Thread.Sleep(SleepAmount);
            }
        }        
    }
    internal static void Delete() 
    { 
        // DELETE 1 RECORD FROM READING
    }
    
    internal static void Update() 
    { 
        // DETERMINE WHICH PART OF THE READING I NEED TO UPDATE 
        // EITHER AMOUNT OR ADDED OR BOTH
    }

    internal static void ShowAll()
    {
        int recordCount = 1;
        // SHOW ALL READINGS FROM THE DATABASE
        // ADD DATA TO TABLE
        var data = new DbAccess();
        // Actual DB Data to be displayed
        var list = data.GetAll();
        // DISPLAY TABLE
        Table table = new();
        table.Title = new TableTitle("[bold]MY GLUCOSE READING CHART[/]");
        table.AddColumn("RECORD");
        table.AddColumn("GLUCOSE READING");
        table.AddColumn("Date Entered");
        table.Border(TableBorder.Rounded);
        table.Expand();
        table.Columns[0].Centered();
        table.Columns[1].Centered();
        table.Columns[2].Centered();

        foreach(var reading in list)
        {
            table.AddRow(recordCount++.ToString(), reading.Amount.ToString(), reading.Added.ToString());
        }

        AnsiConsole.Clear();
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        // RENDER TABLE
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
        Thread.Sleep(SleepAmount * 3);
    }

    internal static void Show306090Report()
    {
        


    }
}
