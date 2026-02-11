using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CreateBillRepository : IGET_CreateBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CreateBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CreateBill>> getasync(GET_CreateBillDomain input)
        {
            List<GET_CreateBill> list = new List<GET_CreateBill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CreateBill]('" + input.BAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CreateBill
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            SEVA = reader.GetString("SEVA"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VNAME = reader.GetString("VNAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            RATE = reader.GetDouble("RATE"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALCOST = reader.GetDouble("TOTALCOST"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            CHEQUEAPPROVED = reader.GetString("CHEQUEAPPROVED"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT")
                        });
                    }
                }
            }
            return list;
        }

        
        
    }
}
