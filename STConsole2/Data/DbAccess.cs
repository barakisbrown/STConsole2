namespace STConsole2.Data;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Spectre.Console;


internal class DbAccess
{
    private readonly string connectionName = "DefaultConnection";
    private IConfiguration Configuration { get; }
    private string connectionString { get; set; }

    internal DbAccess()
    {
       Configuration =  new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        connectionString = GetConnectionString;
    }

    public void DbSetup()
    {
        AnsiConsole.Markup("[underline red]Welcome[/] [white]to Sugar Tracker Console[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.Markup("[red]Opening Connection[/]");
        AnsiConsole.WriteLine();
        using var conn = new SqlConnection(connectionString);

        if (conn.State != System.Data.ConnectionState.Open)
        {
            
            conn.Open();
        }

        AnsiConsole.Markup("[red]Connected[/]");
    }

    private string? GetConnectionString => Configuration.GetConnectionString(connectionName);
}
