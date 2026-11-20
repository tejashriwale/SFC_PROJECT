using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PChallanMasterRepository : IGET_PChallanMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PChallanMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PChallanMaster>> getasync(GET_PChallanMasterDomain masterDomain)
        {
           List<GET_PChallanMaster> list = new List<GET_PChallanMaster> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PChallanMaster](@p0 , @p1,@p2 , @p3 , @p4)order by PCDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(masterDomain.PCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(masterDomain.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(masterDomain.STATUSID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(masterDomain.COMPANYID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(masterDomain.DEPTID) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync ())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_PChallanMaster
                        {
                            PCAID = reader.GetString("PCAID"),
                            PCTRNNO = reader.GetString("PCTRNNO"),
                            PCSELLER = reader.GetString("PCSELLER"),
                            PCCONSIGNER = reader.GetString("PCCONSIGNER"),
                            PCVNO = reader.GetString("PCVNO"),
                            PCPOSUPPLY = reader.GetString("PCPOSUPPLY"),
                            PCNARRATION = reader.GetString("PCNARRATION"),
                            PCDATE = reader.GetString("PCDATE"),
                            PCNAMT = reader.GetDouble("PCNAMT"),
                            PCGAMT = reader.GetDouble("PCGAMT"),
                            POREFF = reader.GetString("POREFF"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            VENDOREMAIL = reader.GetString("VENDOREMAIL"),
                            VENDORCONTACT = reader.GetString("VENDORCONTACT"),
                            PCPOSUPPLYLABLE = reader.GetString("PCPOSUPPLYLABLE"),
                            PONO = reader.GetString("PONO"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PChallanMaster>> getkeyword(GET_PChallanMasterDomain masterDomain)
        {
            List<GET_PChallanMaster> list = new List<GET_PChallanMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_PChallanMaster](@p0 , @p1,@p2 , @p3 , @p4)order by PCDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(masterDomain.PCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(masterDomain.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(masterDomain.STATUSID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(masterDomain.COMPANYID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(masterDomain.DEPTID) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PChallanMaster
                        {
                            PCAID = reader.GetString("PCAID"),
                            PCTRNNO = reader.GetString("PCTRNNO"),
                            PCSELLER = reader.GetString("PCSELLER"),
                            PCCONSIGNER = reader.GetString("PCCONSIGNER"),
                            PCVNO = reader.GetString("PCVNO"),
                            PCPOSUPPLY = reader.GetString("PCPOSUPPLY"),
                            PCNARRATION = reader.GetString("PCNARRATION"),
                            PCDATE = reader.GetString("PCDATE"),
                            PCNAMT = reader.GetDouble("PCNAMT"),
                            PCGAMT = reader.GetDouble("PCGAMT"),
                            POREFF = reader.GetString("POREFF"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            VENDOREMAIL = reader.GetString("VENDOREMAIL"),
                            VENDORCONTACT = reader.GetString("VENDORCONTACT"),
                            PCPOSUPPLYLABLE = reader.GetString("PCPOSUPPLYLABLE"),
                            PONO = reader.GetString("PONO"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
