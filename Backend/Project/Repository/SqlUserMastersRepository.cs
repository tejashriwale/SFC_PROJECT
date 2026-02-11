using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlUserMastersRepository : IUserMastersRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlUserMastersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>>getasync(UserMasters userMasters)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdUserMasters]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = userMasters.UAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UCOMPANYID";
                parameter.Value = userMasters.UCOMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UBRANCHID";
                parameter.Value = userMasters.UBRANCHID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ULOGINTYPE";
                parameter.Value = userMasters.ULOGINTYPE;
                command.Parameters.Add(parameter);



                parameter = command.CreateParameter();
                parameter.ParameterName = "@UPACSID";
                parameter.Value = userMasters.UPACSID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@USTOREID";
                parameter.Value = userMasters.USTOREID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UWAREHOUSEID";
                parameter.Value = userMasters.UWAREHOUSEID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UROLEID";
                parameter.Value = userMasters.UROLEID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UTITLE";
                parameter.Value = userMasters.UTITLE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UFORENAME";
                parameter.Value = userMasters.UFORENAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UDESCRIPTION";
                parameter.Value = userMasters.UDESCRIPTION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UEMAILADDRESS";
                parameter.Value = userMasters.UEMAILADDRESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UMOBILENUMBER";
                parameter.Value = userMasters.UMOBILENUMBER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UPASSWORD";
                parameter.Value = userMasters.UPASSWORD;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UGROUP";
                parameter.Value = userMasters.UGROUP;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UACCESSPOLICY";
                parameter.Value = userMasters.UACCESSPOLICY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UACCESSDAYS";
                parameter.Value = userMasters.UACCESSDAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UACCESSTIMESTART";
                parameter.Value = userMasters.UACCESSTIMESTART;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UACCESSTIMEEND";
                parameter.Value = userMasters.UACCESSTIMEEND;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UREGISTRATIONDATE";
                parameter.Value = userMasters.UREGISTRATIONDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UEXPIRYDATE";
                parameter.Value = userMasters.UEXPIRYDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UREFERENCEKEY";
                parameter.Value = userMasters.UREFERENCEKEY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ULOGINKEY";
                parameter.Value = userMasters.ULOGINKEY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ULASTLOGINDATETIME";
                parameter.Value = userMasters.ULASTLOGINDATETIME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ULOGINCOUNT";
                parameter.Value = userMasters.ULOGINCOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@USTATUSID";
                parameter.Value = userMasters.USTATUSID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UGOOGLECLIENTID";
                parameter.Value = userMasters.UGOOGLECLIENTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UFACEBOOKCLIENTID";
                parameter.Value = userMasters.UFACEBOOKCLIENTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAPPLICATIONKEY";
                parameter.Value = userMasters.UAPPLICATIONKEY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAPPLICATIONDATE";
                parameter.Value = userMasters.UAPPLICATIONDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAPPLICATIONHOME";
                parameter.Value = userMasters.UAPPLICATIONHOME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ULANGUAGEID";
                parameter.Value = userMasters.ULANGUAGEID;
                command.Parameters.Add(parameter);


                dbContext.Database.OpenConnection();

               using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
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
