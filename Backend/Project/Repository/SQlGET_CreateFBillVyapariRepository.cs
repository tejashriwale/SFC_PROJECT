using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SQlGET_CreateFBillVyapariRepository : IGET_CreateFBillVyapariRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SQlGET_CreateFBillVyapariRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CreateFBillVyapari>> getasync(GET_CreateFBillVyapariDomain input)
        {
            List<GET_CreateFBillVyapari> list = new List<GET_CreateFBillVyapari>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CreateFBillVyapari]('" + input.BAID + "', '" + input.BILLNO + "' , '" + input.DATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CreateFBillVyapari
                        {

                            BILLNO = reader.GetInt32("BILLNO"),

                            DATE = reader.GetString("DATE"),
                            SEVA = reader.GetString("SEVA"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            RATE = reader.GetDouble("RATE"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            TOTALLOSS = reader.GetDouble("TOTALLOSS"),
                            FNAME = reader.GetString("FNAME"),
                            JAALI = reader.GetInt32("JAALI"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI"),

                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),

                            VNAME = reader.GetString("VNAME"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
