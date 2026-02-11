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
                command.CommandText = "SELECT Top 20 * FROM [GET_AUCTIONSHED]('" + input.APKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.DATE + "' , '" + input.USERID + "')order by ISCOMPLETED, UORDER ,ODATE desc";
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
                command.CommandText = "SELECT * FROM [GET_AUCTIONSHED]('" + input.APKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '"+ input.DATE + "' , '"+input.USERID+ "')order by ISCOMPLETED, UORDER ,ODATE desc";
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
