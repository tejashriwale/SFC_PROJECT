using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RECEIPTMasterAUCTIONRepository : IGET_RECEIPTMasterAUCTIONRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RECEIPTMasterAUCTIONRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_RECEIPTMasterAUCTION>> getasync(GET_RECEIPTMasterAUCTIONDomain input)
        {
            List<GET_RECEIPTMasterAUCTION> list = new List<GET_RECEIPTMasterAUCTION>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RECEIPTMasterAUCTION]('" + input.BAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.APKID + "' , '" + input.DEPTID + "' , '" + input.DATE + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMasterAUCTION
                        {
                            APKID = reader.GetString("APKID"),
                            BAID = reader.GetString("BAID"),
                            BILLNO = reader.GetString("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            STATUS = reader.GetString("STATUS"),
                            MAID = reader.GetString("MAID"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
