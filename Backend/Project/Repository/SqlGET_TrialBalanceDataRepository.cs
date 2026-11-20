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
                commmand.CommandText = "SELECT * FROM [GET_TrialBalanceData](@p0 ,@p1 ,@p2 ,@p3 ,@p4 )";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.OrganizationID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.DivisionID) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.SDATE) ?? ""); commmand.Parameters.Add(commmand_p2); }
                { var commmand_p3 = commmand.CreateParameter(); commmand_p3.ParameterName = "@p3"; commmand_p3.Value = (object)(Convert.ToString(input.EDATE) ?? ""); commmand.Parameters.Add(commmand_p3); }
                { var commmand_p4 = commmand.CreateParameter(); commmand_p4.ParameterName = "@p4"; commmand_p4.Value = (object)(Convert.ToString(input.LEVEL) ?? ""); commmand.Parameters.Add(commmand_p4); }
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
