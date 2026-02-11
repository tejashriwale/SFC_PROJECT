using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_EMPLOYEERepository : IGET_EMPLOYEERepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_EMPLOYEERepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_EMPLOYEE>> getasync(GET_EMPLOYEEDomain input)
        {
            List<GET_EMPLOYEE> list = new List<GET_EMPLOYEE>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_EMPLOYEE]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_EMPLOYEE
                        {
                            EMPID = reader.GetString("EMPID"),
                            EMPNAME = reader.GetString("EMPNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
