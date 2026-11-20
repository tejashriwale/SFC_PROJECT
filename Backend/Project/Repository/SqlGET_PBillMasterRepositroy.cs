using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PBillMasterRepositroy : IGET_PBillMasterRepositroy
    {
        private ApplicationDbContext dbContext;
        public SqlGET_PBillMasterRepositroy(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PBillMaster>> getasync(GET_PBillMasterDomain input)
        {
           List<GET_PBillMaster> list = new List<GET_PBillMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PBillMaster](@p0 , @p1 , @p2 , @p3)order by PBBILLDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PBAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PBillMaster
                        {
                            PBAID = reader.GetString("PBAID"),
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
                            PCREFF = reader.GetString("PCREFF"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            VENDOREMAIL = reader.GetString("VENDOREMAIL"),
                            VENDORCONTACT = reader.GetString("VENDORCONTACT"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PBillMaster>> getkeyword(GET_PBillMasterDomain input)
        {
            List<GET_PBillMaster> list = new List<GET_PBillMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PBillMaster](@p0 , @p1 , @p2 , @p3)order by PBBILLDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PBAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PBillMaster
                        {
                            PBAID = reader.GetString("PBAID"),
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
                            PCREFF = reader.GetString("PCREFF"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            VENDOREMAIL = reader.GetString("VENDOREMAIL"),
                            VENDORCONTACT = reader.GetString("VENDORCONTACT"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
