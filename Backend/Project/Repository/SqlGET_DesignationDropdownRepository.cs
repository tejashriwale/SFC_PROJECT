using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_DesignationDropdownRepository : IGET_DesignationDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DesignationDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_DesignationDropdown>> getasync(GET_DesignationDropdownDomain input)
        {
            List<GET_DesignationDropdown> list = new List<GET_DesignationDropdown>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_DesignationDropdown](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_DesignationDropdown
                        {
                            DAID = reader.GetString("DAID"),
                            DNAME = reader.GetString("DNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
