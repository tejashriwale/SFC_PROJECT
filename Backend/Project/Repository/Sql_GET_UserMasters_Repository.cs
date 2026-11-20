using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_UserMasters_Repository : I_GET_UserMasters_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_UserMasters_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_UserMasters_>> getasync(UserMasterDomain input)
        {
           List<_GET_UserMasters_> list = new List<_GET_UserMasters_>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from [GET_UserMasters](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.UAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
                    {
                        list.Add(new _GET_UserMasters_
                        {
                            UAID = reader.GetString("UAID"),
                            UBRANCHID = reader.GetString("UBRANCHID"),
                            UPACSID = reader.GetString("UPACSID"),
                            UWAREHOUSEID = reader.GetString("UWAREHOUSEID"),
                            UROLEID = reader.GetString("UROLEID"),
                            UTITLE = reader.GetString("UTITLE"),
                            UFORENAME = reader.GetString("UFORENAME"),
                            UDESCRIPTION = reader.GetString("UDESCRIPTION"),
                            UEMAILADDRESS = reader.GetString("UEMAILADDRESS"),
                            UMOBILENUMBER = reader.GetString("UMOBILENUMBER"),
                            UPASSWORD = reader.GetString("UPASSWORD"),
                            UGROUP = reader.GetString("UGROUP"),
                            UACCESSPOLICY = reader.GetString("UACCESSPOLICY"),
                            UACCESSDAYS = reader.GetString("UACCESSDAYS"),
                            UACCESSTIMESTART = reader.GetString("UACCESSTIMESTART"),
                            UACCESSTIMEEND = reader.GetString("UACCESSTIMEEND"),
                            UREGISTRATIONDATE = reader.GetString("UREGISTRATIONDATE"),
                            UEXPIRYDATE = reader.GetString("UEXPIRYDATE"),
                            UREFERENCEKEY = reader.GetString("UREFERENCEKEY"),
                            ULOGINKEY = reader.GetString("ULOGINKEY"),
                            ULASTLOGINDATETIME = reader.GetString("ULASTLOGINDATETIME"),
                            ULOGINCOUNT = reader.GetString("ULOGINCOUNT"),
                            USTATUSID = reader.GetString("USTATUSID"),
                            UGOOGLECLIENTID = reader.GetString("UGOOGLECLIENTID"),
                            UFACEBOOKCLIENTID = reader.GetString("UGOOGLECLIENTID"),
                            UAPPLICATIONKEY = reader.GetString("UAPPLICATIONKEY"),
                            UAPPLICATIONDATE = reader.GetString("UAPPLICATIONDATE"),
                            UAPPLICATIONHOME = reader.GetString("UAPPLICATIONHOME"),
                            ULANGUAGEID = reader.GetString("ULANGUAGEID"),
                            ULOGINTYPE = reader.GetString("ULOGINTYPE"),
                            UCOMPANYID = reader.GetString("UCOMPANYID"),
                            USTOREID = reader.GetString("USTOREID"),
                            EMPNAME = reader.GetString("EMPNAME"),
                            DNAME = reader.GetString("DNAME"),
                            ISLOGIN = reader.GetBoolean("ISLOGIN"),
                            
                        });
                    }
                }
                return list;
            }
        }

        public async Task<List<_GET_UserMasters_>> GetByGroupAsync(UserMasterDomain input)
        {
            List<_GET_UserMasters_> list = new List<_GET_UserMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "SELECT * FROM [dbo].[GET_UserMasters](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.UAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_UserMasters_
                        {
                            UAID = reader.GetString("UAID"),
                            UBRANCHID = reader.GetString("UBRANCHID"),
                            UPACSID = reader.GetString("UPACSID"),
                            UWAREHOUSEID = reader.GetString("UWAREHOUSEID"),
                            UROLEID = reader.GetString("UROLEID"),
                            UTITLE = reader.GetString("UTITLE"),
                            UFORENAME = reader.GetString("UFORENAME"),
                            UDESCRIPTION = reader.GetString("UDESCRIPTION"),
                            UEMAILADDRESS = reader.GetString("UEMAILADDRESS"),
                            UMOBILENUMBER = reader.GetString("UMOBILENUMBER"),
                            UPASSWORD = reader.GetString("UPASSWORD"),
                            UGROUP = reader.GetString("UGROUP"),
                            UACCESSPOLICY = reader.GetString("UACCESSPOLICY"),
                            UACCESSDAYS = reader.GetString("UACCESSDAYS"),
                            UACCESSTIMESTART = reader.GetString("UACCESSTIMESTART"),
                            UACCESSTIMEEND = reader.GetString("UACCESSTIMEEND"),
                            UREGISTRATIONDATE = reader.GetString("UREGISTRATIONDATE"),
                            UEXPIRYDATE = reader.GetString("UEXPIRYDATE"),
                            UREFERENCEKEY = reader.GetString("UREFERENCEKEY"),
                            ULOGINKEY = reader.GetString("ULOGINKEY"),
                            ULASTLOGINDATETIME = reader.GetString("ULASTLOGINDATETIME"),
                            ULOGINCOUNT = reader.GetString("ULOGINCOUNT"),
                            USTATUSID = reader.GetString("USTATUSID"),
                            UGOOGLECLIENTID = reader.GetString("UGOOGLECLIENTID"),
                            UFACEBOOKCLIENTID = reader.GetString("UGOOGLECLIENTID"),
                            UAPPLICATIONKEY = reader.GetString("UAPPLICATIONKEY"),
                            UAPPLICATIONDATE = reader.GetString("UAPPLICATIONDATE"),
                            UAPPLICATIONHOME = reader.GetString("UAPPLICATIONHOME"),
                            ULANGUAGEID = reader.GetString("ULANGUAGEID"),
                            ULOGINTYPE = reader.GetString("ULOGINTYPE"),
                            UCOMPANYID = reader.GetString("UCOMPANYID"),
                            USTOREID = reader.GetString("USTOREID"),
                            EMPNAME = reader.GetString("EMPNAME"),
                            DNAME = reader.GetString("DNAME"),
                            ISLOGIN = reader.GetBoolean("ISLOGIN"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_UserMasters_>> getkeyword(UserMasterDomain input)
        {
            List<_GET_UserMasters_> list = new List<_GET_UserMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "SELECT * FROM [dbo].[GET_UserMasters](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.UAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_UserMasters_
                        {
                            UAID = reader.GetString("UAID"),
                            UBRANCHID = reader.GetString("UBRANCHID"),
                            UPACSID = reader.GetString("UPACSID"),
                            UWAREHOUSEID = reader.GetString("UWAREHOUSEID"),
                            UROLEID = reader.GetString("UROLEID"),
                            UTITLE = reader.GetString("UTITLE"),
                            UFORENAME = reader.GetString("UFORENAME"),
                            UDESCRIPTION = reader.GetString("UDESCRIPTION"),
                            UEMAILADDRESS = reader.GetString("UEMAILADDRESS"),
                            UMOBILENUMBER = reader.GetString("UMOBILENUMBER"),
                            UPASSWORD = reader.GetString("UPASSWORD"),
                            UGROUP = reader.GetString("UGROUP"),
                            UACCESSPOLICY = reader.GetString("UACCESSPOLICY"),
                            UACCESSDAYS = reader.GetString("UACCESSDAYS"),
                            UACCESSTIMESTART = reader.GetString("UACCESSTIMESTART"),
                            UACCESSTIMEEND = reader.GetString("UACCESSTIMEEND"),
                            UREGISTRATIONDATE = reader.GetString("UREGISTRATIONDATE"),
                            UEXPIRYDATE = reader.GetString("UEXPIRYDATE"),
                            UREFERENCEKEY = reader.GetString("UREFERENCEKEY"),
                            ULOGINKEY = reader.GetString("ULOGINKEY"),
                            ULASTLOGINDATETIME = reader.GetString("ULASTLOGINDATETIME"),
                            ULOGINCOUNT = reader.GetString("ULOGINCOUNT"),
                            USTATUSID = reader.GetString("USTATUSID"),
                            UGOOGLECLIENTID = reader.GetString("UGOOGLECLIENTID"),
                            UFACEBOOKCLIENTID = reader.GetString("UGOOGLECLIENTID"),
                            UAPPLICATIONKEY = reader.GetString("UAPPLICATIONKEY"),
                            UAPPLICATIONDATE = reader.GetString("UAPPLICATIONDATE"),
                            UAPPLICATIONHOME = reader.GetString("UAPPLICATIONHOME"),
                            ULANGUAGEID = reader.GetString("ULANGUAGEID"),
                            ULOGINTYPE = reader.GetString("ULOGINTYPE"),
                            UCOMPANYID = reader.GetString("UCOMPANYID"),
                            USTOREID = reader.GetString("USTOREID"),
                            EMPNAME = reader.GetString("EMPNAME"),
                            DNAME = reader.GetString("DNAME"),
                            ISLOGIN = reader.GetBoolean("ISLOGIN"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
