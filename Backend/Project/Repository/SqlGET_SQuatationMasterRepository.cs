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
                command.CommandText = "SELECT Top 20 * FROM [GET_SQuatationMaster](@p0, @p1 , @p2 , @p3 ,@p4)ORDER BY  SYSDT desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SQAMAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.SATATUSID) ?? ""); command.Parameters.Add(command_p4); }
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
                command.CommandText = "SELECT * FROM [GET_SQuatationMaster](@p0, @p1 , @p2 , @p3 ,@p4) ORDER BY  SYSDT desc ";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SQAMAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.SATATUSID) ?? ""); command.Parameters.Add(command_p4); }
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
