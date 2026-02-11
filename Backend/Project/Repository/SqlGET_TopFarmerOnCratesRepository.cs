using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TopFarmerOnCratesRepository : IGET_TopFarmerOnCratesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TopFarmerOnCratesRepository(ApplicationDbContext dbContext)
        {
          this.dbContext = dbContext;
        }
        public async Task<List<GET_TopFarmerOnCrates>> getasync(GET_TopFarmerOnCratesDomain input)
        {
            List<GET_TopFarmerOnCrates> result = new List<GET_TopFarmerOnCrates>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_TopFarmerOnCrates]('" + input.DPKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "', '" + input.FROMDATE + "', '" + input.TODATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_TopFarmerOnCrates
                        {
                            DPKID = reader.GetString("DPKID"),
                            MAID = reader.GetString("MAID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            UID = reader.GetString("UID"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_TopFarmerOnCrates>> getkeyword(GET_TopFarmerOnCratesDomain input)
        {
            List<GET_TopFarmerOnCrates> result = new List<GET_TopFarmerOnCrates>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_TopFarmerOnCrates]('" + input.DPKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "', '" + input.FROMDATE + "', '" + input.TODATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_TopFarmerOnCrates
                        {
                            DPKID = reader.GetString("DPKID"),
                            MAID = reader.GetString("MAID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            UID = reader.GetString("UID"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
