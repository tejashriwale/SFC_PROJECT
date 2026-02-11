using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerCropsRepository : IGET_FarmerCropsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_FarmerCropsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerCrops>> getasync(GET_FarmerCropsDomain input)
        {
            List<GET_FarmerCrops> list = new List<GET_FarmerCrops>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerCrops]('" + input.MAID + "', '" + input.COMPANYID + "', '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerCrops
                        {
                            CROPID = reader.GetString("CROPID"),
                            CROPNAME = reader.GetString("CROPNAME")
                        });
                    }
                }
            }

            return list;
        }
    }
}
