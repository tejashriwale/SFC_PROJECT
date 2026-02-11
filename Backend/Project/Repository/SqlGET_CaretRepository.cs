using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CaretRepository : IGET_CaretRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CaretRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Caret>> getasync(GET_CaretDomain input)
        {
            List<GET_Caret> list = new List<GET_Caret>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Caret]('" + input.CAID + "' ,  '"+input.KEYWORD+"'   ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Caret
                        {
                            CAID = reader.GetString("CAID"),
                            DATE = reader.GetString("DATE"),
                            SIZE = reader.GetString("SIZE"),
                            STATUS = reader.GetString("STATUS"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Caret>> getkeyword(GET_CaretDomain input)
        {
            List<GET_Caret> list = new List<GET_Caret>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_Caret]('" + input.CAID + "' ,  '" + input.KEYWORD + "'   ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Caret
                        {
                            CAID = reader.GetString("CAID"),
                            DATE = reader.GetString("DATE"),
                            SIZE = reader.GetString("SIZE"),
                            STATUS = reader.GetString("STATUS"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
