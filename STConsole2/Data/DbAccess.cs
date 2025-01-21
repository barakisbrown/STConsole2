namespace STConsole2.Data;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using STConsole2.Model;

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

    internal bool InsertReading(Reading reading)
    {
        bool success = false;
        var sqlCmd = "Insert INTO READING (Amount,Added) VALUES (@Amount,@Added);";
        using var conn = new SqlConnection(connectionString);
        if (conn.State != System.Data.ConnectionState.Open)
        {
            using var cmd = new SqlCommand(sqlCmd);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Amount", (Int16)reading.Amount);
            cmd.Parameters.AddWithValue("@Added", reading.Added.ToDateTime(TimeOnly.MinValue));
            cmd.Connection = conn;
            conn.Open();
            success = cmd.ExecuteNonQuery() == 1;
        }

        return success;
    }

    private string? GetConnectionString => Configuration.GetConnectionString(connectionName);
}
