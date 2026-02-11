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
                command.CommandText = "SELECT Top 20 * FROM [GET_Expense]('" + input.EXPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                command.CommandText = "SELECT * FROM [GET_Expense]('" + input.EXPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
