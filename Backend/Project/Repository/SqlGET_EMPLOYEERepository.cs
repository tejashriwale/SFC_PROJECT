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
                command.CommandText = "SELECT * FROM [GET_EMPLOYEE](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
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
