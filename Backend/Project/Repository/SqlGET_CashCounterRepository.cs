using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CashCounterRepository : IGET_CashCounterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CashCounterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CashCounter>> getasync(GET_CashCounterDomain input)
        {
           List<GET_CashCounter> list = new List<GET_CashCounter>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CashCounter]('" + input.PKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATEO DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_CashCounter
                        {
                            PKID = reader.GetString("PKID"),
                            TOKENNO = reader.GetString("TOKENNO"),
                            TARIKH = reader.GetString("TARIKH"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BILLAMOUNT = reader.GetDouble("BILLAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            STATUS = reader.GetBoolean("STATUS"),
                            ISCLOSE = reader.GetBoolean("ISCLOSE"),
                            BILLNO = reader.GetString("BILLNO"),
                            BAID = reader.GetString("BAID"),
                            FAID = reader.GetString("FAID"),
                            CHEQUENO = reader.GetString("CHEQUENO"),
                            DATEO = reader.GetDateTime("DATEO"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CashCounter>> keyword(GET_CashCounterDomain input)
        {
            List<GET_CashCounter> list = new List<GET_CashCounter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CashCounter]('" + input.PKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATEO DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CashCounter
                        {
                            PKID = reader.GetString("PKID"),
                            TOKENNO = reader.GetString("TOKENNO"),
                            TARIKH = reader.GetString("TARIKH"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BILLAMOUNT = reader.GetDouble("BILLAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            STATUS = reader.GetBoolean("STATUS"),
                            BILLNO = reader.GetString("BILLNO"),
                            BAID = reader.GetString("BAID"),
                            FAID = reader.GetString("FAID"),
                            DATEO = reader.GetDateTime("DATEO"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
