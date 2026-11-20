using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_ProductDNameRepository : IGET_ProductDNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ProductDNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ProductDName>> getasync(GET_ProductDNameDomain input)
        {
            List<GET_ProductDName> list = new List<GET_ProductDName>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_ProductDName]( @p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ProductDName
                        {

                            PAID = reader.GetString("PAID"),
                            PNAME = reader.GetString("PNAME"),
                            RATE = reader.GetString("RATE"),
                            PSUBCATEGORY = reader.GetString("PSUBCATEGORY"),
                            SIZE = reader.GetString("SIZE"),
                            RANGE = reader.GetString("RANGE")



                        });
                    }
                }
            }
            return list;
        }
    }
}
