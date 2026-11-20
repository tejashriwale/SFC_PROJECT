using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CategoryDropdownInvRepository : IGET_CategoryDropdownInvRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CategoryDropdownInvRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CategoryDropdownInv>> getasync(GET_CategoryDropdownInvDomain input)
        {
            List<GET_CategoryDropdownInv> list = new List<GET_CategoryDropdownInv>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryDropdownInv](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryDropdownInv
                        {
                            CTID = reader.GetString("CTID"),
                            CATEGORYNAME = reader.GetString("CATEGORYNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
    
}
