using System.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SBillMasterRepository : IGET_SBillMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
       
        public SqlGET_SBillMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SBillMaster>> getasync(GET_SBillMasterDomain input)
        {
            List<GET_SBillMaster> list = new List<GET_SBillMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SBillMaster]('" + input.SBAID + "' , '"+input.KEYWORD+"' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.STATUSID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SBillMaster
                        {
                            SBAID = reader.GetString("SBAID"),
                            SBVENDORID = reader.GetString("SBVENDORID"),
                            SBCONSIGNER = reader.GetString("SBCONSIGNER"),
                            SBVEHICALNO = reader.GetString("SBVEHICALNO"),
                            SBPOSUPPLY = reader.GetString("SBPOSUPPLY"),
                            SBBILLNO = reader.GetString("SBBILLNO"),
                            SBBILLDATE = reader.GetString("SBBILLDATE"),
                            SBDUEDATE = reader.GetString("SBDUEDATE"),
                            SBNARRATION = reader.GetString("SBNARRATION"),
                            SBTRANSPORT = reader.GetString("SBTRANSPORT"),
                            SBTERMANDCONDITION = reader.GetString("SBTERMANDCONDITION"),
                            SBCHALLANNO = reader.GetString("SBCHALLANNO"),
                            SBDISCOUNT = reader.GetDouble("SBDISCOUNT"),
                            SBNETAMOUNT = reader.GetDouble("SBNETAMOUNT"),
                            VBILLNO = reader.GetString("VBILLNO"),
                            QNODAYS = reader.GetInt32("QNODAYS"),
                            QPAYMENTTERMS = reader.GetString("QPAYMENTTERMS"),
                            QPDUEDATE = reader.GetString("QPDUEDATE"),
                            SCREFF = reader.GetString("SCREFF"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            SBGROSSAMT = reader.GetDouble("SBGROSSAMT"),
                            SBILLTOTAL = reader.GetDouble("SBILLTOTAL"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            TRANSPORTER = reader.GetString("TRANSPORTER"),
                            


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SBillMaster>> getKeyword(GET_SBillMasterDomain input)
        {
            List<GET_SBillMaster> list = new List<GET_SBillMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SBillMaster]('" + input.SBAID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.STATUSID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SBillMaster
                        {
                            SBAID = reader.GetString("SBAID"),
                            SBVENDORID = reader.GetString("SBVENDORID"),
                            SBCONSIGNER = reader.GetString("SBCONSIGNER"),
                            SBVEHICALNO = reader.GetString("SBVEHICALNO"),
                            SBPOSUPPLY = reader.GetString("SBPOSUPPLY"),
                            SBBILLNO = reader.GetString("SBBILLNO"),
                            SBBILLDATE = reader.GetString("SBBILLDATE"),
                            SBDUEDATE = reader.GetString("SBDUEDATE"),
                            SBNARRATION = reader.GetString("SBNARRATION"),
                            SBTRANSPORT = reader.GetString("SBTRANSPORT"),
                            SBTERMANDCONDITION = reader.GetString("SBTERMANDCONDITION"),
                            SBCHALLANNO = reader.GetString("SBCHALLANNO"),
                            SBDISCOUNT = reader.GetDouble("SBDISCOUNT"),
                            SBNETAMOUNT = reader.GetDouble("SBNETAMOUNT"),
                            VBILLNO = reader.GetString("VBILLNO"),
                            QNODAYS = reader.GetInt32("QNODAYS"),
                            QPAYMENTTERMS = reader.GetString("QPAYMENTTERMS"),
                            QPDUEDATE = reader.GetString("QPDUEDATE"),
                            SCREFF = reader.GetString("SCREFF"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            SBGROSSAMT = reader.GetDouble("SBGROSSAMT"),
                            SBILLTOTAL = reader.GetDouble("SBILLTOTAL"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            TRANSPORTER = reader.GetString("TRANSPORTER"),
                            


                        });
                    }
                }
            }
            return list;
        }
   
    }
}
