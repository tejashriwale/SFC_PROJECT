using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRMEmployeesNameRepository : IGET_HRMEmployeesNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HRMEmployeesNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_HRMEmployeesName>> getasync(GET_HRMEmployeesNameDomain input)
        {
            List<GET_HRMEmployeesName> list = new List<GET_HRMEmployeesName> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_HRMEmployeesName]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_HRMEmployeesName
                        {
                            EMPID = reader.GetString("EMPID"),
                            EMPNAME = reader.GetString("EMPNAME")
                        });
                    }
                }
            }
            return list;
        }
    }
}
