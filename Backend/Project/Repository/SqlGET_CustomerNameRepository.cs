using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CustomerNameRepository : IGET_CustomerNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CustomerNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CustomerName>> getasync(GET_CustomerNameDomain input)
        {
            List<GET_CustomerName> list = new List<GET_CustomerName>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CustomerName](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerName
                        {
                            CAID = reader.GetString("CAID"),
                            CCONTACTPERSONNAME = reader.GetString("CCONTACTPERSONNAME"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
}
