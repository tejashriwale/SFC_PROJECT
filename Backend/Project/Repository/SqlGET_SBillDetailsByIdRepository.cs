using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SBillDetailsByIdRepository : IGET_SBillDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SBillDetailsByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SBillDetailsById>> getasync(GET_SBillDetailsByIdDomain input)
        {
            List<GET_SBillDetailsById> list = new List<GET_SBillDetailsById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SBillDetailsById]('" + input.SBAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SBillDetailsById
                        {
                            SBDAID = reader.GetString("SBDAID"),
                            SBAID = reader.GetString("SBAID"),
                            SBPRODUCT = reader.GetString("SBPRODUCT"),
                            SBDUOM = reader.GetString("SBDUOM"),
                            SBDQUANTITY = reader.GetString("SBDQUANTITY"),
                            SBDRATE = reader.GetDouble("SBDRATE"),
                            SBDTAMT = reader.GetDouble("SBDTAMT"),
                            SBDIGST = reader.GetDouble("SBDIGST"),
                            SBDSGST = reader.GetDouble("SBDSGST"),
                            SBDCGST = reader.GetDouble("SBDCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            SBDTOTAL = reader.GetDouble("SBDTOTAL"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
