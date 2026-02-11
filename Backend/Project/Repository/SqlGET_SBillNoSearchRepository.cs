using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SBillNoSearchRepository : IGET_SBillNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SBillNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SBillNoSearch>> getasync(GET_SBillNoSearchDomain input)
        {
           List<GET_SBillNoSearch> list = new List<GET_SBillNoSearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SBillNoSearch]('" + input.SBBILLNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SBillNoSearch
                        {
                            SBBILLDATE = reader.GetString("SBBILLDATE"),
                            SBBILLNO = reader.GetString("SBBILLNO"),
                            SBNARRATION = reader.GetString("SBNARRATION"),
                            SBCONSIGNER = reader.GetString("SBCONSIGNER"),
                            SBDQUANTITY = reader.GetString("SBDQUANTITY"),
                            SBDTAMT = reader.GetDouble("SBDTAMT"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            SBPRODUCT = reader.GetString("SBPRODUCT"),
                            SBDUOM = reader.GetString("SBDUOM"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            SBCONSIGNERNAME = reader.GetString("SBCONSIGNERNAME"),
                            SBVENDORID = reader.GetString("SBVENDORID"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
