namespace STConsole2.Data;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using STConsole2.Model;

internal class DbAccess
{
    private readonly string connectionName = "DefaultConnection";
    private readonly string getAllSqlCmd = "SELECT * FROM READING;";
    private readonly string insertSqlCmd = "Insert INTO READING (Amount,Added) VALUES (@Amount,@Added);";
    private readonly string deleteSqlCmd = "Delete FROM READING WHERE ID = @ID;";
    private readonly string updateAmountSqlCmd = "UPDATE READING SET Amount = @Amount WHERE ID = @ID;";
    private readonly string updateDateSqlCmd = "UPDATE READING SET Added = @Added WHERE ID = @ID;";
    private readonly string updateAllSqlCmd = "UPDATE READING SET Amount = @Amount,Added = @Added WHERE ID = @ID";
    private IConfiguration Configuration { get; }
    private string ConnectionString { get; set; }

    internal DbAccess()
    {
       Configuration =  new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        ConnectionString = GetConnectionString;
    }

    public void DbSetup()
    {
        AnsiConsole.Markup("[underline red]Welcome[/] [white]to Sugar Tracker Console[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.Markup("[red]Opening Connection[/]");
        AnsiConsole.WriteLine();
        using var conn = new SqlConnection(ConnectionString);

        if (conn.State != System.Data.ConnectionState.Open)
        {
            
            conn.Open();
        }

        AnsiConsole.Markup("[red]Connected[/]");
    }

    internal List<Reading> GetAll()
    {
        var list = new List<Reading>();
        using var conn = new SqlConnection(ConnectionString);
        if (conn.State == System.Data.ConnectionState.Open)
        {
            using var cmd = new SqlCommand(getAllSqlCmd);
            cmd.Connection = conn;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Reading reading = new()
                {
                    ID = reader.GetInt32(0),
                    Amount = reader.GetInt32(1),
                    Added = DateOnly.FromDateTime(reader.GetDateTime(2))
                };
                list.Add(reading);
            }
            reader.Close();
        }

        return list;
    }

    internal bool InsertReading(Reading reading)
    {
        bool success = false;
        using var conn = new SqlConnection(ConnectionString);
        if (conn.State != System.Data.ConnectionState.Open)
        {
            using var cmd = new SqlCommand(insertSqlCmd);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Amount", (Int16)reading.Amount);
            cmd.Parameters.AddWithValue("@Added", reading.Added.ToDateTime(TimeOnly.MinValue));
            cmd.Connection = conn;
            conn.Open();
            success = cmd.ExecuteNonQuery() == 1;
        }

        return success;
    }

    internal bool DeleteSingleReading(int DeletedId)
    {
        bool success = false;
        using var conn = new SqlConnection(ConnectionString);
        if (conn.State != System.Data.ConnectionState.Open)
        {
            using var cmd = new SqlCommand(deleteSqlCmd);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", DeletedId);
            cmd.Connection = conn;
            conn.Open();
            success = cmd.ExecuteNonQuery() == 1;
        }

        return success;
    }

    internal bool UpdateAmount(int UpdatedAmount, int ID) 
    {
        bool success = false;
        using var conn = new SqlConnection(ConnectionString);
        if (conn.State != System.Data.ConnectionState.Open)
        {
            using var cmd = new SqlCommand(updateAmountSqlCmd);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Amount", UpdatedAmount);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Connection = conn;
            conn.Open();
            success = cmd.ExecuteNonQuery() == 1;
        }

        return success;
    }
    internal bool UpdateAdded(DateOnly UpdatedDate, int ID) 
    {
        bool success = false;
        using var conn = new SqlConnection(ConnectionString);
        if (conn.State != System.Data.ConnectionState.Open)
        {
            using var cmd = new SqlCommand(updateDateSqlCmd);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Added", UpdatedDate.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Connection = conn;
            conn.Open();
            success = cmd.ExecuteNonQuery() == 1;
        }

        return success;
    }
    internal bool UpdateAll(Reading Updated) 
    {
        bool success = false;
        using var conn = new SqlConnection(ConnectionString);
        if (conn.State != System.Data.ConnectionState.Open)
        {
            using var cmd = new SqlCommand(updateAllSqlCmd);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Amount", Updated.Amount);
            cmd.Parameters.AddWithValue("@Added", Updated.Added);
            cmd.Parameters.AddWithValue("@ID",Updated.ID);
            cmd.Connection = conn;
            conn.Open();
            success = cmd.ExecuteNonQuery() == 1;
        }

        return success;
    }

    List<Reading> GetDummyData()
    {
        var data = new List<Reading>();

        int[] Amounts = new[] { 80, 220, 100, 130, 150 };
        string[] Added = new[] { "01/20/2025", "01/21/2025", "01/22/2025", "01/23/2025", "01/24/2025" };
        

        for(int recordNumber = 0; recordNumber < 5; recordNumber++)
        {
            var reading = new Reading()
            {
                ID = recordNumber,
                Amount = Amounts[recordNumber],
                Added = DateOnly.Parse(Added[recordNumber])
            };

            data.Add(reading);
        }

        return data;
    }

    private string? GetConnectionString => Configuration.GetConnectionString(connectionName);
}
