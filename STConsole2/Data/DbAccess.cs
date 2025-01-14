namespace STConsole2.Data;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Spectre.Console;


internal class DbAccess
{
    private IConfiguration Configuration { get; }
    private string connectionString { get; set; }

    internal DbAccess()
    {
       Configuration =  new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        connectionString = Configuration.GetConnectionString("DefaultConnection");
    }

    public void DbTest()
    {
        AnsiConsole.Markup("[underline red]Welcome[/] [white]to Sugar Tracker Console[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.Markup("[blink red]Opening Connection[/]");
        AnsiConsole.WriteLine();
        using var conn = new SqlConnection(connectionString);

        if (conn.State != System.Data.ConnectionState.Open)
        {
            
            conn.Open();
        }

        AnsiConsole.Markup("[blink red]Connected[/]");
    }
}
