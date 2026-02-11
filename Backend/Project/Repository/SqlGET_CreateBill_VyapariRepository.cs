using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CreateBill_VyapariRepository : IGET_CreateBill_VyapariRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CreateBill_VyapariRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CreateBill_Vyapari>> getasync(GET_CreateBill_VyapariDomain input)
        {
            List<GET_CreateBill_Vyapari> list = new List<GET_CreateBill_Vyapari>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CreateBill_Vyapari]('" + input.VYAPARINAME + "'  , '" + input.COMPANYID + "' , '" + input.DEPTID + "', '" + input.BILLNO + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CreateBill_Vyapari
                        {
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VNAME = reader.GetString("VNAME"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI"),
                            DATE = reader.GetString("DATE"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            SEVA = reader.GetString("SEVA"),
                            TOTALLOSS = reader.GetDouble("TOTALLOSS"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            FNAME = reader.GetString("FNAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            RATE = reader.GetDouble("RATE"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            
                        });
                    }
                }
            }
            return list;
        }
    }
}
