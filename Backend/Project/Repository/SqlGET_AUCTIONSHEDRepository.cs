using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_AUCTIONSHEDRepository : IGET_AUCTIONSHEDRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AUCTIONSHEDRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_AUCTIONSHED>> getasync(GET_AUCTIONSHEDDomain input)
        {
            List<GET_AUCTIONSHED> list = new List<GET_AUCTIONSHED>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_AUCTIONSHED](@p0 , @p1 , @p2 , @p3 , @p4 , @p5)order by ISCOMPLETED, UORDER ,ODATE desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.APKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
                { var command_p5 = command.CreateParameter(); command_p5.ParameterName = "@p5"; command_p5.Value = (object)(Convert.ToString(input.USERID) ?? ""); command.Parameters.Add(command_p5); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AUCTIONSHED
                        {
                            APKID = reader.GetString("APKID"),
                            TOKEN = reader.GetString("TOKEN"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            SEVA = reader.GetString("SEVA"),
                            HAMALI = reader.GetString("HAMALI"),
                            TOLAI = reader.GetString("TOLAI"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            ODATE = reader.GetDateTime("ODATE"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            FMOBILE = reader.GetString("FMOBILE"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_AUCTIONSHED>> getkeyword(GET_AUCTIONSHEDDomain input)
        {
            List<GET_AUCTIONSHED> list = new List<GET_AUCTIONSHED>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AUCTIONSHED](@p0 , @p1 , @p2 , @p3 , @p4 , @p5)order by ISCOMPLETED, UORDER ,ODATE desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.APKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
                { var command_p5 = command.CreateParameter(); command_p5.ParameterName = "@p5"; command_p5.Value = (object)(Convert.ToString(input.USERID) ?? ""); command.Parameters.Add(command_p5); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AUCTIONSHED
                        {
                            APKID = reader.GetString("APKID"),
                            TOKEN = reader.GetString("TOKEN"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            SEVA = reader.GetString("SEVA"),
                            HAMALI = reader.GetString("HAMALI"),
                            TOLAI = reader.GetString("TOLAI"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            ODATE = reader.GetDateTime("ODATE"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            FMOBILE = reader.GetString("FMOBILE"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
