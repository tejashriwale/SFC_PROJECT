using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ExpenseRepository : IGET_ExpenseRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ExpenseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Expense>> getasync(GET_ExpenseDomain input)
        {
            List<GET_Expense> list = new List<GET_Expense>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Expense](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.EXPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Expense
                        {
                            EXPAID = reader.GetString("EXPAID"),
                            EXPCATEGORY = reader.GetString("EXPCATEGORY"),
                            EXPDATE = reader.GetString("EXPDATE"),
                            EXPAMOUNT = reader.GetDouble("EXPAMOUNT"),
                            EXPREFERENCE = reader.GetString("EXPREFERENCE"),
                            EXPENSEFOR = reader.GetString("EXPENSEFOR"),
                            EXPDESCRIPTION = reader.GetString("EXPDESCRIPTION"),
                            EXPNAME = reader.GetString("EXPNAME"),
                        });
                    }

                }
            }
            return list;
        }

        public async Task<List<GET_Expense>> getkeyword(GET_ExpenseDomain input)
        {
            List<GET_Expense> list = new List<GET_Expense>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Expense](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.EXPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Expense
                        {
                            EXPAID = reader.GetString("EXPAID"),
                            EXPCATEGORY = reader.GetString("EXPCATEGORY"),
                            EXPDATE = reader.GetString("EXPDATE"),
                            EXPAMOUNT = reader.GetDouble("EXPAMOUNT"),
                            EXPREFERENCE = reader.GetString("EXPREFERENCE"),
                            EXPENSEFOR = reader.GetString("EXPENSEFOR"),
                            EXPDESCRIPTION = reader.GetString("EXPDESCRIPTION"),
                            EXPNAME = reader.GetString("EXPNAME"),
                        });
                    }

                }
            }
            return list;
        }
            
    }
}
