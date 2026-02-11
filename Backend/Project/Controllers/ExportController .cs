using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("api/export")]
public class ExportController : ControllerBase
{
    private readonly IConfiguration _config;

    public ExportController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("excel")]
    public async Task<IActionResult> ExportToExcel([FromBody] ExportRequest request)
    {
        if (string.IsNullOrEmpty(request.QueryType) || string.IsNullOrEmpty(request.QueryName))
            return BadRequest("QueryType (SP/FUNCTION) आणि QueryName लागेल!");

        // **Data Fetch करतो**
        var dataTable = await GetData(request.QueryType, request.QueryName);
        if (dataTable.Rows.Count == 0)
            return NotFound("No Data Found!");

        // **Excel File बनवतो**
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Data");

            // **Header टाकतो**
            for (int i = 0; i < dataTable.Columns.Count; i++)
                worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;

            // **Data टाकतो**
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cell(row + 2, col + 1).Value = dataTable.Rows[row][col]?.ToString();
                }
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.QueryName}.xlsx");
            }
        }
    }

    private async Task<DataTable> GetData(string queryType, string queryName)
    {
        DataTable dt = new DataTable();
        using (var conn = new SqlConnection(_config.GetConnectionString("shadimuharathConStr")))
        {
            await conn.OpenAsync();
            string sqlQuery = "";

            if (queryType.ToUpper() == "SP")
            {
                // If it's a Stored Procedure, we'll execute it directly
                sqlQuery = $"EXEC {queryName}";
            }
            else if (queryType.ToUpper() == "FUNCTION")
            {
                // If it's a Function, we will query it using SELECT *
                sqlQuery = $"SELECT * FROM {queryName}()";
            }
            else
            {
                throw new Exception("Invalid QueryType! Use 'SP' or 'FUNCTION'.");
            }

            using (var cmd = new SqlCommand(sqlQuery, conn))
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
        }
        return dt;
    }
}



    public class ExportRequest
{
    public string QueryType { get; set; } // "SP" किंवा "FUNCTION"
    public string QueryName { get; set; } // Procedure किंवा Function चे नाव
}
