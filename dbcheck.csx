using System;
using System.Data;
using Microsoft.Data.SqlClient;

var connectionString = "Server=ADSVR\\SQLEXPRESS01;Database=InventoryManagement;User ID=sa;Password=123456;TrustServerCertificate=True;";

try
{
    using var conn = new SqlConnection(connectionString);
    conn.Open();
    Console.WriteLine("=== DATABASE CONNECTION SUCCESSFUL ===");
    Console.WriteLine($"Database: {conn.Database}");
    Console.WriteLine($"Server: {conn.DataSource}");
    Console.WriteLine();

    // Get all tables
    Console.WriteLine("========== ALL TABLES ==========");
    using (var cmd = new SqlCommand(@"
        SELECT TABLE_SCHEMA, TABLE_NAME
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_TYPE = 'BASE TABLE'
        ORDER BY TABLE_SCHEMA, TABLE_NAME", conn))
    {
        using var reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            Console.WriteLine($"  {count}. [{reader["TABLE_SCHEMA"]}].[{reader["TABLE_NAME"]}]");
        }
        Console.WriteLine($"\nTotal Tables: {count}");
    }

    Console.WriteLine();

    // Get all stored procedures
    Console.WriteLine("========== ALL STORED PROCEDURES ==========");
    using (var cmd = new SqlCommand(@"
        SELECT ROUTINE_SCHEMA, ROUTINE_NAME, CREATED, LAST_ALTERED
        FROM INFORMATION_SCHEMA.ROUTINES
        WHERE ROUTINE_TYPE = 'PROCEDURE'
        ORDER BY ROUTINE_NAME", conn))
    {
        using var reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            Console.WriteLine($"  {count}. [{reader["ROUTINE_SCHEMA"]}].[{reader["ROUTINE_NAME"]}] (Created: {reader["CREATED"]}, Modified: {reader["LAST_ALTERED"]})");
        }
        Console.WriteLine($"\nTotal Stored Procedures: {count}");
    }

    Console.WriteLine();

    // Get all functions
    Console.WriteLine("========== ALL FUNCTIONS ==========");
    using (var cmd = new SqlCommand(@"
        SELECT ROUTINE_SCHEMA, ROUTINE_NAME, ROUTINE_TYPE, DATA_TYPE, CREATED, LAST_ALTERED
        FROM INFORMATION_SCHEMA.ROUTINES
        WHERE ROUTINE_TYPE = 'FUNCTION'
        ORDER BY ROUTINE_NAME", conn))
    {
        using var reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            Console.WriteLine($"  {count}. [{reader["ROUTINE_SCHEMA"]}].[{reader["ROUTINE_NAME"]}] (Type: {reader["DATA_TYPE"]}, Created: {reader["CREATED"]}, Modified: {reader["LAST_ALTERED"]})");
        }
        Console.WriteLine($"\nTotal Functions: {count}");
    }

    Console.WriteLine();

    // Get all views
    Console.WriteLine("========== ALL VIEWS ==========");
    using (var cmd = new SqlCommand(@"
        SELECT TABLE_SCHEMA, TABLE_NAME
        FROM INFORMATION_SCHEMA.VIEWS
        ORDER BY TABLE_NAME", conn))
    {
        using var reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            Console.WriteLine($"  {count}. [{reader["TABLE_SCHEMA"]}].[{reader["TABLE_NAME"]}]");
        }
        Console.WriteLine($"\nTotal Views: {count}");
    }

    Console.WriteLine();

    // Get all triggers
    Console.WriteLine("========== ALL TRIGGERS ==========");
    using (var cmd = new SqlCommand(@"
        SELECT name, parent_id, OBJECT_NAME(parent_id) AS TableName, type_desc, create_date, modify_date
        FROM sys.triggers
        WHERE is_ms_shipped = 0
        ORDER BY name", conn))
    {
        using var reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            Console.WriteLine($"  {count}. {reader["name"]} (Table: {reader["TableName"]}, Type: {reader["type_desc"]})");
        }
        Console.WriteLine($"\nTotal Triggers: {count}");
    }

    conn.Close();
    Console.WriteLine("\n=== DONE ===");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
    Console.WriteLine($"Stack: {ex.StackTrace}");
}
