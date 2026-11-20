using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGETItemDropdownRepository : IGETItemDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGETItemDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GETItemDropdown>> getasync(GETItemDropdownDomain input)
        {
            List<GETItemDropdown> list = new List<GETItemDropdown>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GETItemDropdown](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();
                
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GETItemDropdown
                        {
                            ITEMAID = reader.GetString("ITEMAID"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            ITEMNE = reader.GetString("ITEMNE"),
                        });
                    }
                }

            }
            return list;
            
        }
    }
}
