using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariAppFarmerRepository : IGET_VyapariAppFarmerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariAppFarmerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariAppFarmer>> getasync(GET_VyapariAppFarmerDomain input)
        {
            List<GET_VyapariAppFarmer> result = new List<GET_VyapariAppFarmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariAppFarmer]('" + input.BAID + "', '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY ISVYAPARIVERIFIED , DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariAppFarmer
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            DATE = reader.GetString("DATE"),
                            TOKENNO = reader.GetString("TOKENNO"),
                            BDAID = reader.GetString("BDAID"),
                            APKID = reader.GetString("APKID"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),



                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_VyapariAppFarmer>> getkeyword(GET_VyapariAppFarmerDomain input)
        {
            List<GET_VyapariAppFarmer> result = new List<GET_VyapariAppFarmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariAppFarmer]('" + input.BAID + "', '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariAppFarmer
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            DATE = reader.GetString("DATE"),
                            TOKENNO = reader.GetString("TOKENNO"),
                            APKID = reader.GetString("APKID"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),

                        });
                    }
                }
            }
            return result;
        }

    }
}
