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
                command.CommandText = "SELECT Top 20 * FROM [GET_ACCOUNT]('" + input.ACCAID + "' , '" + input.KEYWORD + "' , '"+input.COMPANYID+"')ORDER BY ODATE DESC";
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
                command.CommandText = "SELECT * FROM [GET_ACCOUNT]('" + input.ACCAID + "' , '" + input.KEYWORD + "','"+input.COMPANYID+"')ORDER BY DATE DESC";
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
