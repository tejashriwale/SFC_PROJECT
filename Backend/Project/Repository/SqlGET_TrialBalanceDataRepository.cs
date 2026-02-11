using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_TrialBalanceDataRepository : IGET_TrialBalanceDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TrialBalanceDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TrialBalanceData>> getasync(GET_TrialBalanceDataDomain input)
        {
            List<GET_TrialBalanceData> list = new List<GET_TrialBalanceData>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT * FROM [GET_TrialBalanceData]('" + input.OrganizationID + "' ,'" + input.DivisionID + "' ,'" + input.SDATE + "' ,'" + input.EDATE + "' ,'" + input.LEVEL + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TrialBalanceData
                        {
                            DRAMT = reader.GetDecimal("DRAMT"),
                            DRACC = reader.GetString("DRACC"),
                            CRAMT = reader.GetDecimal("CRAMT"),
                            CRACC = reader.GetString("CRACC"),
                            DRACCTM = reader.GetString("DRACCTM"),
                            CRACCTM = reader.GetString("CRACCTM"),
                            CRACCGID = reader.GetString("CRACCGID"),
                            DRACCGID = reader.GetString("DRACCGID"),
                           
                        });
                    }
                }
                return list;
            }
        }
    }
}
