using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CategoryDropdownRepository : IGET_CategoryDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CategoryDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CategoryDropdown>> getasync(GET_CategoryDropdownDomain input)
        {
            List<GET_CategoryDropdown> list = new List<GET_CategoryDropdown>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryDropdown](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryDropdown
                        {
                            CATEGORYNAME = reader.GetString("CATEGORYNAME"),
                            CTID = reader.GetString("CTID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
