using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_DepartmentNameRepository : IGET_DepartmentNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DepartmentNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_DepartmentName>> getasync(GET_DepartmentNameDomain input)
        {
            List<GET_DepartmentName> list = new List<GET_DepartmentName>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_DepartmentName](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_DepartmentName
                        {
                            DEPTAID = reader.GetString("DEPTAID"),
                            DNAME = reader.GetString("DNAME")
                        });
                    }
                }
            }
            return list;
        }
    }
}
