using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{ 
    public class SqlGET_FBillPrintRepository : IGET_FBillPrintRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FBillPrintRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FBillPrint>> getasync(GET_FBillPrintDomain input)
        {
            List<GET_FBillPrint> list = new List<GET_FBillPrint>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_FBillPrint]('" + input.BAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FBillPrint
                        {
                            BAID = reader.GetString("BAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            TOTALCARETS = reader.GetDouble("TOTALCARETS"),              
                            SEVA = reader.GetString("SEVA"),
                            CROPTYPE = reader.GetString("CROPTYPE"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            FNAME = reader.GetString("FNAME"),
                            JALI = reader.GetInt32("JALI"),
                            RATE    = reader.GetDouble("RATE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            SEVAM = reader.GetString("SEVAM"),
                            SevaID = reader.GetString("SevaID"),
                            SevaValue = reader.GetString("SevaValue"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE")




                        });
                    }
                }
            }
            return list;
        }
    }
}
