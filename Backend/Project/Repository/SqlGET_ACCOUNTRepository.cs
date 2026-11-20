using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ACCOUNTRepository : IGET_ACCOUNTRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ACCOUNTRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ACCOUNT>> getasync(GET_ACCOUNTDomain input)
        {
            List<GET_ACCOUNT> list = new List<GET_ACCOUNT>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ACCOUNT](@p0 , @p1 , @p2)ORDER BY ODATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.ACCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_ACCOUNT
                        {
                            ACCAID = reader.GetString("ACCAID"),
                            ACCGID = reader.GetString("ACCGID"),
                            MGRPID = reader.GetString("MGRPID"),
                            SGRPID = reader.GetString("SGRPID"),
                            GLRPID = reader.GetString("GLRPID"),
                            SGLRPID = reader.GetString("SGLRPID"),
                            LevelID = reader.GetString("LevelID"),
                            ACCTM = reader.GetString("ACCTM"),
                            ACCTE = reader.GetString("ACCTE"),
                            TRNModeID = reader.GetString("TRNModeID"),
                            RELID = reader.GetString("RELID"),
                            MAINGROUP = reader.GetString("MAINGROUP"),
                            SUBGROUP = reader.GetString("SUBGROUP"),
                            GENERALACC = reader.GetString("GENERALACC"),
                            DATE = reader.GetString("DATE"),
                            ODATE = reader.GetDateTime("ODATE")
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_ACCOUNT>> getkeyword(GET_ACCOUNTDomain input)
        {
            List<GET_ACCOUNT> list = new List<GET_ACCOUNT>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ACCOUNT](@p0 , @p1,@p2)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.ACCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ACCOUNT
                        {
                            ACCAID = reader.GetString("ACCAID"),
                            ACCGID = reader.GetString("ACCGID"),
                            MGRPID = reader.GetString("MGRPID"),
                            SGRPID = reader.GetString("SGRPID"),
                            GLRPID = reader.GetString("GLRPID"),
                            SGLRPID = reader.GetString("SGLRPID"),
                            LevelID = reader.GetString("LevelID"),
                            ACCTM = reader.GetString("ACCTM"),
                            ACCTE = reader.GetString("ACCTE"),
                            TRNModeID = reader.GetString("TRNModeID"),
                            RELID = reader.GetString("RELID"),
                            MAINGROUP = reader.GetString("MAINGROUP"),
                            SUBGROUP = reader.GetString("SUBGROUP"),
                            GENERALACC = reader.GetString("GENERALACC"),
                            DATE = reader.GetString("DATE"),
                            ODATE = reader.GetDateTime("ODATE")
                        });
                    }
                }
            }
            return list;
        }
    }
}
