using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CheckVehicalExistRepository : IGET_CheckVehicalExistRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CheckVehicalExistRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CheckVehicalExist>> getasync(GET_CheckVehicalExistDomain input)
        {
            List<GET_CheckVehicalExist> list = new List<GET_CheckVehicalExist>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CheckVehicalExist]('" + input.VNO + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CheckVehicalExist
                        {
                            VNO = reader.GetString("VNO"),
                            VDAID = reader.GetString("VDAID"),
                          

                        });
                    }
                }
            }
            return list;
        }
    }
}
