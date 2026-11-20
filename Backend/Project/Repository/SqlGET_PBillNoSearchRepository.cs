using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PBillNoSearchRepository : IGET_PBillNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PBillNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PBillNoSearch>> getasync(GET_PBillNoSearchDomain input)
        {
            List<GET_PBillNoSearch> list = new List<GET_PBillNoSearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PBillNoSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PBBILLNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_PBillNoSearch
                        {
                            PBAID = reader.GetString("PBAID"),
                            PBPRODUCT = reader.GetString("PBPRODUCT"),
                            PBDUOM = reader.GetString("PBDUOM"),
                            PBDQUANTITY = reader.GetString("PBDQUANTITY"),
                            PBDRATE = reader.GetDouble("PBDRATE"),
                            PBDTAMT = reader.GetDouble("PBDTAMT"),
                            PBDIGST = reader.GetDouble("PBDIGST"),
                            PBDSGST = reader.GetDouble("PBDSGST"),
                            PBDCGST = reader.GetDouble("PBDCGST"),
                            PBDAID = reader.GetString("PBDAID"),
                            PBVENDORID = reader.GetString("PBVENDORID"),
                            PBCONSIGNER = reader.GetString("PBCONSIGNER"),
                            PBVEHICALNO = reader.GetString("PBVEHICALNO"),
                            PBPOSUPPLY = reader.GetString("PBPOSUPPLY"),
                            PBBILLNO = reader.GetString("PBBILLNO"),
                            PBDATE = reader.GetString("PBDATE"),
                            PBBILLDATE = reader.GetString("PBBILLDATE"),
                            PBDUEDATE = reader.GetString("PBDUEDATE"),
                            PBNARRATION = reader.GetString("PBNARRATION"),
                            PBTRANSPORT = reader.GetString("PBTRANSPORT"),
                            PBTERMANDCONDITION = reader.GetString("PBTERMANDCONDITION"),
                            PBCHALLANNO = reader.GetString("PBCHALLANNO"),
                            PBDISCOUNT = reader.GetDouble("PBDISCOUNT"),
                            PBNETAMOUNT = reader.GetDouble("PBNETAMOUNT"),
                            VBILLNO = reader.GetString("VBILLNO"),
                            QNODAYS = reader.GetInt32("QNODAYS"),
                            QPAYMENTTERMS = reader.GetString("QPAYMENTTERMS"),
                            QPDUEDATE = reader.GetString("QPDUEDATE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            PAYMENTYPE = reader.GetString("PAYMENTYPE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
