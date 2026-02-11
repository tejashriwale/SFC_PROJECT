using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_DayBookDataRepository : IGET_DayBookDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DayBookDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_DayBookData>> getasync(GET_DayBookDataDomain input)
        {
            List<GET_DayBookData> list = new List<GET_DayBookData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_DayBookData]('" + input.OrganizationID + "' , '" + input.SDATE + "', '" + input.EDATE + "' ,'" + input.DivisionID + "' )ORDER BY	TRNDT ,DRCR ,MGRPID ,GRPID ,ACCID ,SerialNumber";
                dbContext.Database.OpenConnection();                                                                                                       		
                                                                                                                                                           		
                using (var reader = await command.ExecuteReaderAsync())                                                                                    		
                {                                                                                                                                          		
                    while (await reader.ReadAsync())                                                                                                       		
                    {
                        list.Add(new GET_DayBookData
                        {
                            ACCID = reader.GetString("ACCID"),
                            SerialNumber = reader.GetString("SerialNumber"),
                            ACCTM = reader.GetString("ACCTM"),
                            Amount = reader.GetDouble("Amount"),
                            DRCR = reader.GetString("DRCR"),
                            GRPID = reader.GetString("GRPID"),
                            GRPKEY = reader.GetString("GRPKEY"),
                            MGRPID = reader.GetString("MGRPID"),
                            Narration = reader.GetString("Narration"),
                            OID = reader.GetString("OID"),
                            ReferenceKey = reader.GetString("ReferenceKey"),
                            TRNDT = reader.GetString("TRNDT"),
                            TRNMODE = reader.GetString("TRNMODE"),
                            ReferenceName = reader.GetString("ReferenceName"),
                            MAINGROUP = reader.GetString("MAINGROUP"),



                        });
                    }
                }
            }
            return list;
        }
    }
}
