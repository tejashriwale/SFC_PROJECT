using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariDropdownRepository : IGET_VyapariDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariDropdown>> getasync(GET_VyapariDropdownDomain input)
        {
            List<GET_VyapariDropdown> list = new List<GET_VyapariDropdown>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariDropdown]( @p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariDropdown
                        {
                           VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                           
                            RATE = reader.GetDouble("RATE"),
                            VYAPARI = reader.GetString("VYAPARI")
                        });
                    }
                }
            }
            return list;
        }
    }
}
