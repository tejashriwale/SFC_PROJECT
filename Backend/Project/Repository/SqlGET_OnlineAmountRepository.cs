using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_OnlineAmountRepository : IGET_OnlineAmountRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_OnlineAmountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_OnlineAmount>> getasync(GET_OnlineAmountDomain input)
        {
            List<GET_OnlineAmount> list = new List<GET_OnlineAmount>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_OnlineAmount]('" + input.BAID + "' ,'" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' ,'" + input.DATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_OnlineAmount
                        {
                            BAID = reader.GetString("BAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            ACCNO = reader.GetString("ACCNO"),
                            IFSCCODE = reader.GetString("IFSCCODE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            PKID = reader.GetString("PKID"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                           


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_OnlineAmount>> getkeyword(GET_OnlineAmountDomain input)
        {
            List<GET_OnlineAmount> list = new List<GET_OnlineAmount>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_OnlineAmount]('" + input.BAID + "' ,'" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' ,'" + input.DATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_OnlineAmount
                        {
                            BAID = reader.GetString("BAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            ACCNO = reader.GetString("ACCNO"),
                            IFSCCODE = reader.GetString("IFSCCODE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            PKID = reader.GetString("PKID"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),



                        });
                    }
                }
            }
            return list;
        }
    }
}
