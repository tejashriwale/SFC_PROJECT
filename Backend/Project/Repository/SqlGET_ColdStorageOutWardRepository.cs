using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ColdStorageOutWardRepository : IGET_ColdStorageOutWardRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ColdStorageOutWardRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ColdStorageOutWard>> getasync(GET_ColdStorageOutWardDomain input)
        {
            List<GET_ColdStorageOutWard> list = new List<GET_ColdStorageOutWard>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ColdStorageOutWard](@p0 ,  @p1   ,@p2 , @p3)ORDER BY ENDDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CSOWID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ColdStorageOutWard
                        {
                            CSOWID = reader.GetString("CSOWID"),
                            LOTNO = reader.GetString("LOTNO"),
                            CNAME = reader.GetString("CNAME"),
                            THIRDPARTYNAME = reader.GetString("THIRDPARTYNAME"),
                            WEIGHTINKG = reader.GetDouble("WEIGHTINKG"),
                            ENDDATE = reader.GetString("ENDDATE"),
                            DAYINSLAB = reader.GetInt32("DAYINSLAB"),
                            RATE = reader.GetDouble("RATE"),
                            OUTWARDWEIGHT = reader.GetDouble("OUTWARDWEIGHT"),
                            REMAININGWEIGHT = reader.GetDouble("REMAININGWEIGHT"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                            STARTDATE = reader.GetString("STARTDATE"),
                            CSID = reader.GetString("CSID"),
                            DAYINSLABT = reader.GetInt32("DAYINSLABT"),
                            RN = reader.GetInt64("RN"),
                            ITEM = reader.GetString("ITEM"),
                            NARRATION = reader.GetString("NARRATION"),



                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_ColdStorageOutWard>> keyword(GET_ColdStorageOutWardDomain input)
        {
            List<GET_ColdStorageOutWard> list = new List<GET_ColdStorageOutWard>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ColdStorageOutWard](@p0 ,  @p1   ,@p2 , @p3)ORDER BY ENDDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CSOWID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ColdStorageOutWard
                        {
                            CSOWID = reader.GetString("CSOWID"),
                            LOTNO = reader.GetString("LOTNO"),
                            CNAME = reader.GetString("CNAME"),
                            THIRDPARTYNAME = reader.GetString("THIRDPARTYNAME"),
                            WEIGHTINKG = reader.GetDouble("WEIGHTINKG"),
                            ENDDATE = reader.GetString("ENDDATE"),
                            DAYINSLAB = reader.GetInt32("DAYINSLAB"),
                            RATE = reader.GetDouble("RATE"),
                            OUTWARDWEIGHT = reader.GetDouble("OUTWARDWEIGHT"),
                            REMAININGWEIGHT = reader.GetDouble("REMAININGWEIGHT"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                            STARTDATE = reader.GetString("STARTDATE"),
                            CSID = reader.GetString("CSID"),
                            DAYINSLABT = reader.GetInt32("DAYINSLABT"),
                            RN = reader.GetInt64("RN"),
                            ITEM = reader.GetString("ITEM"),
                            NARRATION = reader.GetString("NARRATION"),



                        });
                    }
                }
            }
            return list;
        }
    }
}
