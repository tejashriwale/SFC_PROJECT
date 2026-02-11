using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ExpenseCategoryRepository : IGET_ExpenseCategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ExpenseCategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ExpenseCategory>> getasync(GET_ExpenseCategoryDomain input)
        {
            List<GET_ExpenseCategory> list = new List<GET_ExpenseCategory>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ExpenseCategory]('" + input.EXPCAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ExpenseCategory
                        {
                            EXPCAID = reader.GetString("EXPCAID"),
                            EXPNAME = reader.GetString("EXPNAME"),
                            EXPDESCRIPTION = reader.GetString("EXPDESCRIPTION"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_ExpenseCategory>> getkeyword(GET_ExpenseCategoryDomain input)
        {
            List<GET_ExpenseCategory> list = new List<GET_ExpenseCategory>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ExpenseCategory]('" + input.EXPCAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ExpenseCategory
                        {
                            EXPCAID = reader.GetString("EXPCAID"),
                            EXPNAME = reader.GetString("EXPNAME"),
                            EXPDESCRIPTION = reader.GetString("EXPDESCRIPTION"),

                        });
                    }
                }
            }
            return list;
        }
           
    }
}
