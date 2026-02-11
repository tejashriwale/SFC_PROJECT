using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SQuatationMasterRepository : IGET_SQuatationMasterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_SQuatationMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SQuatationMaster>> getasync(GET_SQuatationMasterDomain input)
        {
            List<GET_SQuatationMaster> list = new List<GET_SQuatationMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SQuatationMaster]('" + input.SQAMAID + "', '"+input.KEYWORD+"' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' ,'" + input.SATATUSID + "')ORDER BY  SYSDT desc";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SQuatationMaster
                        {
                            SQAMAID = reader.GetString("SQAMAID"),
                            SQCAID = reader.GetString("SQCAID"),
                            CAID = reader.GetString("CAID"),
                            SPRNO = reader.GetString("SPRNO"),
                            SQMNAMT = reader.GetDouble("SQMNAMT"),
                            SQGAMT = reader.GetDouble("SQGAMT"),
                            SQNO = reader.GetString("SQNO"),
                            SQDATE = reader.GetString("SQDATE"),
                            SQDUEDATE = reader.GetString("SQDUEDATE"),
                            SQTRANSPORTTERMS = reader.GetString("SQTRANSPORTTERMS"),
                            SQPAYMENTTERMS = reader.GetString("SQPAYMENTTERMS"),
                            ISAPPROVED = reader.GetBoolean("ISAPPROVED"),
                            SQPDUEDATE = reader.GetString("SQPDUEDATE"),
                            SQNODAYS = reader.GetInt32("SQNODAYS"),
                            SRREFF = reader.GetString("SRREFF"),
                            SQTRANSPORTID = reader.GetString("SQTRANSPORTID"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                  
                            SYSDT = reader.GetDateTime("SYSDT")

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SQuatationMaster>> getkeyword(GET_SQuatationMasterDomain input)
        {
            List<GET_SQuatationMaster> list = new List<GET_SQuatationMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SQuatationMaster]('" + input.SQAMAID + "', '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' ,'" + input.SATATUSID + "') ORDER BY  SYSDT desc ";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SQuatationMaster
                        {
                            SQAMAID = reader.GetString("SQAMAID"),
                            SQCAID = reader.GetString("SQCAID"),
                            CAID = reader.GetString("CAID"),
                            SPRNO = reader.GetString("SPRNO"),
                            SQMNAMT = reader.GetDouble("SQMNAMT"),
                            SQGAMT = reader.GetDouble("SQGAMT"),
                            SQNO = reader.GetString("SQNO"),
                            SQDATE = reader.GetString("SQDATE"),
                            SQDUEDATE = reader.GetString("SQDUEDATE"),
                            SQTRANSPORTTERMS = reader.GetString("SQTRANSPORTTERMS"),
                            SQPAYMENTTERMS = reader.GetString("SQPAYMENTTERMS"),
                            ISAPPROVED = reader.GetBoolean("ISAPPROVED"),
                            SQPDUEDATE = reader.GetString("SQPDUEDATE"),
                            SQNODAYS = reader.GetInt32("SQNODAYS"),
                            SRREFF = reader.GetString("SRREFF"),
                            SQTRANSPORTID = reader.GetString("SQTRANSPORTID"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                    
                            SYSDT = reader.GetDateTime("SYSDT")

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
