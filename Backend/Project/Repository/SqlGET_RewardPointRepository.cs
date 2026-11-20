using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RewardPointRepository : IGET_RewardPointRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RewardPointRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RewardPoint>> getasync(GET_RewardPointDomain input)
        {
            List<GET_RewardPoint> result = new List<GET_RewardPoint>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_RewardPoint](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.RPID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_RewardPoint
                        {
                            RPID = reader.GetString("RPID"),
                            DATE = reader.GetString("DATE"),
                            PPTYPE = reader.GetString("PPTYPE"),
                            REWARDPOINTS = reader.GetString("REWARDPOINTS"),
                            STATUS = reader.GetBoolean("STATUS"),
                            UAID = reader.GetString("UAID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            ASID = reader.GetString("ASID"),
                            SYSDT = reader.GetString("SYSDT"),
                            PERSONTYPE = reader.GetString("PERSONTYPE"),

                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_RewardPoint>> getkeyword(GET_RewardPointDomain input)
        {
            List<GET_RewardPoint> result = new List<GET_RewardPoint>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_RewardPoint](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.RPID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_RewardPoint
                        {
                            RPID = reader.GetString("RPID"),
                            DATE = reader.GetString("DATE"),
                            PPTYPE = reader.GetString("PPTYPE"),
                            REWARDPOINTS = reader.GetString("REWARDPOINTS"),
                            STATUS = reader.GetBoolean("STATUS"),
                            UAID = reader.GetString("UAID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            ASID = reader.GetString("ASID"),
                            SYSDT = reader.GetString("SYSDT"),
                            PERSONTYPE = reader.GetString("PERSONTYPE"),

                        });
                    }
                }
            }
            return result;
        }
    }
}
