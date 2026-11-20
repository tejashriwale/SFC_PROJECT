using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VyapariBharanaDataRepository : IGET_VyapariBharanaDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariBharanaDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariBharanaData>> getasync(GET_VyapariBharanaDataDomain input)
        {
            List<GET_VyapariBharanaData> result = new List<GET_VyapariBharanaData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariBharanaData](@p0,@p1 )ORDER BY TDATE ASC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.TRNDATE) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariBharanaData
                        {
                            SLAB_RANGE = reader.GetString("SLAB_RANGE"),
                            BPAMT = reader.GetDecimal("BPAMT"),
                            VAPASI_AMT = reader.GetDecimal("VAPASI_AMT"),
                            TOTAL = reader.GetDecimal("TOTAL"),
                            TRNDT = reader.GetString("TRNDT"),
                            VDAID = reader.GetString("VDAID"),
                            FDUEDT = reader.GetString("FDUEDT"),
                            VPAID = reader.GetString("VPAID"),
                            TID = reader.GetString("TID"),
                            STID = reader.GetString("STID"),
                            TDATE = reader.GetDateTime("TDATE"),
                            TotalTAMT = reader.GetDecimal("TotalTAMT"),
                            GRPID = reader.GetString("GRPID"),
                            REFKEY = reader.GetString("REFKEY"),


                        });
                    }
                }
            }
            return result;
        }

    }
}
