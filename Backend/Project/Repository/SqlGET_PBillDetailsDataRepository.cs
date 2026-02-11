using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PBillDetailsDataRepository : IGET_PBillDetailsDataRepository
    {
        private ApplicationDbContext dbContext;
        public SqlGET_PBillDetailsDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PBillDetailsData>> getasync(GET_PBillDetailsDataDomain input)
        {
            List<GET_PBillDetailsData> list = new List<GET_PBillDetailsData>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_PBillDetailsData]('" + input.PBAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_PBillDetailsData()
                        {
                            PBAID = reader.GetString("PBAID"),
                            PBDAID = reader.GetString("PBDAID"),
                            PBPRODUCT = reader.GetString("PBPRODUCT"),
                            PBDUOM = reader.GetString("PBDUOM"),
                            PBDQUANTITY = reader.GetString("PBDQUANTITY"),
                            PBDRATE = reader.GetDouble("PBDRATE"),
                            PBDTAMT = reader.GetDouble("PBDTAMT"),
                            PBDIGST = reader.GetDouble("PBDIGST"),
                            PBDSGST = reader.GetDouble("PBDSGST"),
                            PBDCGST = reader.GetDouble("PBDCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
