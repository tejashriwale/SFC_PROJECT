using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlWAREHOUSENAMERepository : IWAREHOUSENAMERepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlWAREHOUSENAMERepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<WAREHOUSENAME>> getasync(GET_WarehouseNameDomain input)
        {
           List<WAREHOUSENAME> list = new List<WAREHOUSENAME>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_WAREHOUSENAME](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new WAREHOUSENAME
                        {
                            WAID = reader.GetString("WAID"),
                            WNAME = reader.GetString("WNAME")
                        });

                    }
                }
            }
            return list;
        }
    }
}
