using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CounterNameRepository : IGET_CounterNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CounterNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CounterName>> getasync(GET_CounterNameDomain input)
        {
            List<GET_CounterName> list = new List<GET_CounterName>();
            {
                using(var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [GET_CounterName](@p0 , @p1)";
                    { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                    { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                    dbContext.Database.OpenConnection();

                    using(var reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            list.Add(new GET_CounterName
                            {
                                CAID = reader.GetString("CAID"),
                                CNAME = reader.GetString("CNAME"),
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
