using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StockTransferRepository : IGET_StockTransferRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StockTransferRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_StockTransfer>> get(GET_StockTransferDomain input)
        {
            List<GET_StockTransfer> list = new List<GET_StockTransfer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_StockTransfer]('" + input.STMID + "' , '"+input.KEYWORD+"' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_StockTransfer
                        {
                            STMID = reader.GetString("STMID"),
                            STMTRANSNO = reader.GetString("STMTRANSNO"),
                            STMSTORE = reader.GetString("STMSTORE"),
                            STMWARHOUSE = reader.GetString("STMWARHOUSE"),
                            STMCOUNTER = reader.GetString("STMCOUNTER"),
                            STMDATE = reader.GetString("STMDATE"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_StockTransfer>> getasync(GET_StockTransferDomain input)
        {
            List<GET_StockTransfer> list = new List<GET_StockTransfer>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_StockTransfer]('" + input.STMID + "' ,'"+input.KEYWORD+"', '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_StockTransfer
                        {
                            STMID = reader.GetString("STMID"),
                            STMTRANSNO = reader.GetString("STMTRANSNO"),
                            STMSTORE = reader.GetString("STMSTORE"),
                            STMWARHOUSE = reader.GetString("STMWARHOUSE"),
                            STMCOUNTER = reader.GetString("STMCOUNTER"),
                            STMDATE = reader.GetString("STMDATE"),
                        });
                    }    
                }
            }
            return list;
        }
    }
}
