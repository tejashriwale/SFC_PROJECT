using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerAdvanceRepository : IGET_FarmerAdvanceRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerAdvanceRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerAdvance>> getasync(GET_FarmerAdvanceDomain input)
        {
            List<GET_FarmerAdvance> list = new List<GET_FarmerAdvance>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT   * FROM [GET_FarmerAdvance]('" + input.MAID + "' , '" + input.CROPID + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerAdvance
                        {
                            ADVAMT = reader.GetDouble("ADVAMT"),
                            TRAID = reader.GetString("TRAID"),
                            TRNDT = reader.GetString("TRNDT")

                        });
                    }
                }
            }
            return list;
        }
    }
}
