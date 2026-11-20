using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_GenralAccountRepository : IGET_GenralAccountRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GenralAccountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GenralAccount>> getasync(GET_GenralAccountDomain input)
        {
            List<GET_GenralAccount> result = new List<GET_GenralAccount>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GenralAccount]( @p0 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GenralAccount
                        {
                            ACCAID = reader.GetString("ACCAID"),
                            ACCTM = reader.GetString("ACCTM"),
                          

                        });
                    }
                }
            }
            return result;
        }
    }
}
