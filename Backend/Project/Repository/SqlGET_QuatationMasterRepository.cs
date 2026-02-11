using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_QuatationMasterRepository : IGET_QuatationMasterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_QuatationMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_QuatationMaster>> getasync(GET_QuatationMasterDomain input)
        {
            List<GET_QuatationMaster> list = new List<GET_QuatationMaster>();
           using(var command = dbContext.Database.GetDbConnection().CreateCommand())
           {
                command.CommandText = "SELECT Top 20 * FROM [GET_QuatationMaster]('" + input.QAMAID + "', '"+input.KEYWORD+"' ,'"+input.COMPANYID+"' , '"+input.DEPTID+ "')order by QODATE DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_QuatationMaster
                        {
                            QAMAID = reader.GetString("QAMAID"),
                            QVAID = reader.GetString("QVAID"),
                            QVCONTACT = reader.GetString("QVCONTACT"),
                            QVEMAIL = reader.GetString("QVEMAIL"),
                            QVADDRESS = reader.GetString("QVADDRESS"),
                            PRNO = reader.GetString("PRNO"),
                            QMNAMT = reader.GetDouble("QMNAMT"),
                            QGAMT = reader.GetDouble("QGAMT"),
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            QDUEDATE = reader.GetString("QDUEDATE"),
                            QTRANSPORTTERMS = reader.GetString("QTRANSPORTTERMS"),
                            QPAYMENTTERMS = reader.GetString("QPAYMENTTERMS"),
                            ISAPPROVED = reader.GetBoolean("ISAPPROVED"),
                            QPDUEDATE = reader.GetString("QPDUEDATE"),
                            QNODAYS = reader.GetInt32("QNODAYS"),
                            PRREFF = reader.GetString("PRREFF"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            CIFSC = reader.GetString("CIFSC"),
                            ImplicationValue = reader.GetString("ImplicationValue"),
                        });
                    }
                }
           }
           return list;
        }

        public async Task<List<GET_QuatationMaster>> getkeyword(GET_QuatationMasterDomain input)
        {
            List<GET_QuatationMaster> list = new List<GET_QuatationMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_QuatationMaster]('" + input.QAMAID + "', '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')order by QODATE DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_QuatationMaster
                        {
                            QAMAID = reader.GetString("QAMAID"),
                            QVAID = reader.GetString("QVAID"),
                            QVCONTACT = reader.GetString("QVCONTACT"),
                            QVEMAIL = reader.GetString("QVEMAIL"),
                            QVADDRESS = reader.GetString("QVADDRESS"),
                            PRNO = reader.GetString("PRNO"),
                            QMNAMT = reader.GetDouble("QMNAMT"),
                            QGAMT = reader.GetDouble("QGAMT"),
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            QDUEDATE = reader.GetString("QDUEDATE"),
                            QTRANSPORTTERMS = reader.GetString("QTRANSPORTTERMS"),
                            QPAYMENTTERMS = reader.GetString("QPAYMENTTERMS"),
                            ISAPPROVED = reader.GetBoolean("ISAPPROVED"),
                            QPDUEDATE = reader.GetString("QPDUEDATE"),
                            QNODAYS = reader.GetInt32("QNODAYS"),
                            PRREFF = reader.GetString("PRREFF"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            CIFSC = reader.GetString("CIFSC"),
                            ImplicationValue = reader.GetString("ImplicationValue"),
                        });
                    }
                }
            }
            return list;
        }
            
    }
}
