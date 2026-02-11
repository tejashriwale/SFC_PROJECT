using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdCompanyInfoRepository : ISP_AddUpdCompanyInfoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdCompanyInfoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdCompanyInfo input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdCompanyInfo]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@CINFOID";
                parameter.Value = input.CINFOID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = input.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CNAME";
                parameter.Value = input.CNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTELEPHONE";
                parameter.Value = input.CTELEPHONE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CEMAIL";
                parameter.Value = input.CEMAIL;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@CCONTACT";
                parameter.Value = input.CCONTACT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CPAN";
                parameter.Value = input.CPAN;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@CGSTNO";
                parameter.Value = input.CGSTNO;
                command.Parameters.Add(parameter);

              

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CADDRESS";
                parameter.Value = input.CADDRESS;
                command.Parameters.Add(parameter);

              

                parameter = command.CreateParameter();
                parameter.ParameterName = "@INSTAGRAMID";
                parameter.Value = input.INSTAGRAMID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TWITTERID";
                parameter.Value = input.TWITTERID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FACEBOOKID";
                parameter.Value = input.FACEBOOKID;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new ApiResponseMessage
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues")

                        });
                    }
                }
            }
            return list;
        }
    }
}
