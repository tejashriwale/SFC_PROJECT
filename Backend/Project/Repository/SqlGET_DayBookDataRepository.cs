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
                command.CommandText = "SELECT  * FROM [GET_DayBookData](@p0 , @p1, @p2 ,@p3 )ORDER BY	TRNDT ,DRCR ,MGRPID ,GRPID ,ACCID ,SerialNumber";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.OrganizationID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.SDATE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.EDATE) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DivisionID) ?? ""); command.Parameters.Add(command_p3); }
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
