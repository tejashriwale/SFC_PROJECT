using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public class SqlAddUpdCompanyRepository : IAddUpdCompanyRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAddUpdCompanyRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getassync(AddUpdCompany addUpdCompany)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdateCompany]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = addUpdCompany.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CPAID";
                parameter.Value = addUpdCompany.CPAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CNAME";
                parameter.Value = addUpdCompany.CNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTELEPHONE";
                parameter.Value = addUpdCompany.CTELEPHONE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CCONTACT";
                parameter.Value = addUpdCompany.CCONTACT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CEMAIL";
                parameter.Value = addUpdCompany.CEMAIL;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CADDRESS";
                parameter.Value = addUpdCompany.CADDRESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CAREA";
                parameter.Value = addUpdCompany.CAREA;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CLANDMARK";
                parameter.Value = addUpdCompany.CLANDMARK;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CCITY";
                parameter.Value = addUpdCompany.CCITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CSTATE";
                parameter.Value = addUpdCompany.CSTATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CPINCODE";
                parameter.Value = addUpdCompany.CPINCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CREGNO";
                parameter.Value = addUpdCompany.CREGNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CREGDATE";
                parameter.Value = addUpdCompany.CREGDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CPAN";
                parameter.Value = addUpdCompany.CPAN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CGSTNO";
                parameter.Value = addUpdCompany.CGSTNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CSTATUS";
                parameter.Value = addUpdCompany.CSTATUS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CIFSC";
                parameter.Value = addUpdCompany.CIFSC;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CACCOUNTNAME";
                parameter.Value = addUpdCompany.CACCOUNTNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CACCOUNTNUMBER";
                parameter.Value = addUpdCompany.CACCOUNTNUMBER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CBANKNAME";
                parameter.Value = addUpdCompany.CBANKNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CBRANCHNAME";
                parameter.Value = addUpdCompany.CBRANCHNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@OPENINGDT";
                parameter.Value = addUpdCompany.OPENINGDT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTYPE";
                parameter.Value = addUpdCompany.CTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@INSTAGRAMID";
                parameter.Value = addUpdCompany.INSTAGRAMID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TWITTERID";
                parameter.Value = addUpdCompany.TWITTERID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FACEBOOKID";
                parameter.Value = addUpdCompany.FACEBOOKID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = addUpdCompany.UAID;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
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
