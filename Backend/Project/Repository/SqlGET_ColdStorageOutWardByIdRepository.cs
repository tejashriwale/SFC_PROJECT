using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ColdStorageOutWardByIdRepository : IGET_ColdStorageOutWardByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ColdStorageOutWardByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ColdStorageOutWardById>> getasync(GET_ColdStorageOutWardByIdDomain input)
        {
            List<GET_ColdStorageOutWardById> list = new List<GET_ColdStorageOutWardById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_ColdStorageOutWardById]('" + input.CSID+ "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ColdStorageOutWardById
                        {
                            CSOWID = reader.GetString("CSOWID"),
                            LOTNO = reader.GetString("LOTNO"),
                            CNAME = reader.GetString("CNAME"),
                            THIRDPARTYNAME = reader.GetString("THIRDPARTYNAME"),
                            WEIGHTINKG = reader.GetDouble("WEIGHTINKG"),
                            ENDDATE = reader.GetString("ENDDATE"),
                            DAYINSLAB = reader.GetInt32("DAYINSLAB"),
                            RATE = reader.GetDouble("RATE"),
                            OUTWARDWEIGHT = reader.GetDouble("OUTWARDWEIGHT"),
                            REMAININGWEIGHT = reader.GetDouble("REMAININGWEIGHT"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                            STARTDATE = reader.GetString("STARTDATE"),
                            CSID = reader.GetString("CSID"),

                            ITEM = reader.GetString("ITEM"),



                        });
                    }
                }
            }
            return list;
        }
    }
}
