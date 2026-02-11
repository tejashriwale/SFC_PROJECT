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
                command.CommandText = "SELECT Top 20 * FROM [GET_PChallanMaster]('" + masterDomain.PCAID + "' , '" + masterDomain.KEYWORD + "','"+masterDomain.STATUSID+"' , '" + masterDomain.COMPANYID + "' , '" + masterDomain.DEPTID + "')order by PCDATE DESC";
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
                command.CommandText = "SELECT  * FROM [GET_PChallanMaster]('" + masterDomain.PCAID + "' , '" + masterDomain.KEYWORD + "','" + masterDomain.STATUSID + "' , '" + masterDomain.COMPANYID + "' , '" + masterDomain.DEPTID + "')order by PCDATE DESC";
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
