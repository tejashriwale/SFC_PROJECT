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
                command.CommandText = "SELECT * FROM [GET_DepartmentName]('" + input.COMPANYID + "')";
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
