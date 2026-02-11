using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_BalanceshitDataRepository : IGET_BalanceshitDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_BalanceshitDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_BalanceshitData>> getasync(GET_BalanceshitDataDomain input)
        {
            List<GET_BalanceshitData> result = new List<GET_BalanceshitData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_BalanceshitData]('" + input.OrganizationID + "' , '" + input.DivisionID + "' , '" + input.SDATE + "' , '" + input.EDATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_BalanceshitData
                        {
                            AMT = reader.GetDouble("AMT"),
                            ACCAID = reader.GetString("ACCAID"),
                            DEPTID = reader.GetString("DEPTID"),
                            ACCOUNTNAME = reader.GetString("ACCOUNTNAME"),
                            SGRPID = reader.GetString("SGRPID"),
                            SUBGROUP = reader.GetString("SUBGROUP"),
                            MGRPID = reader.GetString("MGRPID"),
                            MAINGROUP = reader.GetString("MAINGROUP"),
                            CRDR = reader.GetString("CRDR")
       

                        });
                    }
                }
            }
            return result;
        }
    }
}
