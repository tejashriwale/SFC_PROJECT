using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdUserMastersRepository : ISP_AddUpdUserMastersRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdUserMastersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdUserMasters input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdUserMasters]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = input.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UCOMPANYID";
                    parameter.Value = input.UCOMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UBRANCHID";
                    parameter.Value = input.UBRANCHID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UPACSID";
                    parameter.Value = input.UPACSID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@USTOREID";
                    parameter.Value = input.USTOREID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UWAREHOUSEID";
                    parameter.Value = input.UWAREHOUSEID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UROLEID";
                    parameter.Value = input.UROLEID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UTITLE";
                    parameter.Value = input.UTITLE;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UFORENAME";
                    parameter.Value = input.UFORENAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UDESCRIPTION";
                    parameter.Value = input.UDESCRIPTION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UEMAILADDRESS";
                    parameter.Value = input.UEMAILADDRESS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ULOGINTYPE";
                    parameter.Value = input.ULOGINTYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UMOBILENUMBER";
                    parameter.Value = input.UMOBILENUMBER;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UPASSWORD";
                    parameter.Value = input.UPASSWORD;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UGROUP";
                    parameter.Value = input.UGROUP;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UACCESSPOLICY";
                    parameter.Value = input.UACCESSPOLICY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UACCESSDAYS";
                    parameter.Value = input.UACCESSDAYS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UACCESSTIMESTART";
                    parameter.Value = input.UACCESSTIMESTART;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UACCESSTIMEEND";
                    parameter.Value = input.UACCESSTIMEEND;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UREGISTRATIONDATE";
                    parameter.Value = input.UREGISTRATIONDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UEXPIRYDATE";
                    parameter.Value = input.UEXPIRYDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UREFERENCEKEY";
                    parameter.Value = input.UREFERENCEKEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ULOGINKEY";
                    parameter.Value = input.ULOGINKEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ULASTLOGINDATETIME";
                    parameter.Value = input.ULASTLOGINDATETIME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ULOGINCOUNT";
                    parameter.Value = input.ULOGINCOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@USTATUSID";
                    parameter.Value = input.USTATUSID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UGOOGLECLIENTID";
                    parameter.Value = input.UGOOGLECLIENTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UFACEBOOKCLIENTID";
                    parameter.Value = input.UFACEBOOKCLIENTID;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAPPLICATIONKEY";
                    parameter.Value = input.UAPPLICATIONKEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAPPLICATIONDATE";
                    parameter.Value = input.UAPPLICATIONDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAPPLICATIONHOME";
                    parameter.Value = input.UAPPLICATIONHOME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ULANGUAGEID";
                    parameter.Value = input.ULANGUAGEID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDAYEND";
                    parameter.Value = input.ISDAYEND;
                    command.Parameters.Add(parameter);

                    dbContext.Database.OpenConnection();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new ApiResponseMessage
                            {
                                IsSuccessful = reader.GetInt32("IsSuccessful"),
                                ResponseCode = reader.GetString("ResponseCode"),
                                ResponseMessage = reader.GetString("ResponseMessage"),
                                ResponseValues = reader.GetString("ResponseValues")
                            });
                        }

                    }
                }
                return result;
            }
        }
    }
}
