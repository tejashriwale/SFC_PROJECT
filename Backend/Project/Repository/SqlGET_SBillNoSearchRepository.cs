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
                command.CommandText = "SELECT * FROM [GET_SBillNoSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SBBILLNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
