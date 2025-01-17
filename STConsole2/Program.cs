using Spectre.Console;
using STConsole2.Data;
using STConsole2.View;

string notAvailYet = "[blink] Not Avaiable Yet[/]";
var db = new DbAccess();
db.DbSetup();

int choice = -1;
while(choice != 0)
{
    AnsiConsole.Clear();
    choice = Menu.GetMenuAndSelection();
    switch (choice)
    {
        case 0:
            break;
        case 1:
            AnsiConsole.Markup(notAvailYet);
            break;
        case 2:
            Menu.Add();
            break;
        case 3:
            Menu.Delete();
            break;
        case 4:
            Menu.Update(); break;
        case 5:
        case 6:
        case 7:
            AnsiConsole.Markup(notAvailYet);
            break;
    }
}
 