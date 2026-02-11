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
                command.CommandText = "SELECT * FROM [GET_WAREHOUSENAME]('"+input.COMPANYID+"' , '"+input.DEPTID+"')";

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
