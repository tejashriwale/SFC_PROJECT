using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_LedgerDataRepository : IGET_LedgerDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_LedgerDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_LedgerData>> getasync(GET_LedgerDataDomain input)
        {
            List<GET_LedgerData> result = new List<GET_LedgerData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_LedgerData]('" + input.OrganizationID + "'  ,'" + input.DivisionID + "'  , '" + input.ACCAID + "' , '" + input.SDATE + "' , '" + input.EDATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_LedgerData
                        {
                            DATE		 = reader.GetDateTime("DATE"),
                            DRAMT	 = reader.GetDouble("DRAMT"),
                            CRAMT	 = reader.GetDouble("CRAMT"),
                            DRCR		 = reader.GetString("DRCR"),
                            ACCAID	 = reader.GetString("ACCAID"),
                            GLRPID	 = reader.GetString("GLRPID"),
                            MGRPID	 = reader.GetString("MGRPID"),
                            SGRPID	 = reader.GetString("SGRPID"),
                            GENRALLAGER	 = reader.GetString("GENRALLAGER"),
                            SUBLAGER		 = reader.GetString("SUBLAGER"),
                            LAGER = reader.GetString("LAGER"),
                            NARR = reader.GetString("NARR")



                        });
                    }
                }
            }
            return result;
        }
    }
}
