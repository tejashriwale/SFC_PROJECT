using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public class SqlCustomersPartnersRepository : ICustomersPartnersRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlCustomersPartnersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> GetAsync(CustomersPartners customersPartners)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdCustomersPartners]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@CAID";
                parameter.Value = customersPartners.CAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CCOMPANYNAME";
                parameter.Value = customersPartners.CCOMPANYNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTYPE";
                parameter.Value = customersPartners.CTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CADDRESS";
                parameter.Value = customersPartners.CADDRESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CCITY";
                parameter.Value = customersPartners.CCITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CPINCODE";
                parameter.Value = customersPartners.CPINCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTELEPHONENUMBER";
                parameter.Value = customersPartners.CTELEPHONENUMBER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CEMAIL";
                parameter.Value = customersPartners.CEMAIL;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CWEBSITEURL";
                parameter.Value = customersPartners.CWEBSITEURL;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CCONTACTPERSONNAME";
                parameter.Value = customersPartners.CCONTACTPERSONNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CCONTACTPERSONMOBILE";
                parameter.Value = customersPartners.CCONTACTPERSONMOBILE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CDESIGNATION";
                parameter.Value = customersPartners.CDESIGNATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CGSTIN";
                parameter.Value = customersPartners.CGSTIN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTRADENAME";
                parameter.Value = customersPartners.CTRADENAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTRADEADDRESS";
                parameter.Value = customersPartners.CTRADEADDRESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTRADECITY";
                parameter.Value = customersPartners.CTRADECITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTRADEPINCODE";
                parameter.Value = customersPartners.CTRADEPINCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CSTATE";
                parameter.Value = customersPartners.CSTATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CDISTANCE";
                parameter.Value = customersPartners.CDISTANCE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CPAN";
                parameter.Value = customersPartners.CPAN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CBUSINESSTYPE";
                parameter.Value = customersPartners.CBUSINESSTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CSTATUS";
                parameter.Value = customersPartners.CSTATUS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CIFSC";
                parameter.Value = customersPartners.CIFSC;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CACCOUNTNAME";
                parameter.Value = customersPartners.CACCOUNTNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CACCOUNTNUMBER";
                parameter.Value = customersPartners.CACCOUNTNUMBER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CBANKNAME";
                parameter.Value = customersPartners.CBANKNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CBRANCHNAME";
                parameter.Value = customersPartners.CBRANCHNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CAREA";
                parameter.Value = customersPartners.CAREA;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CLANDMARK";
                parameter.Value = customersPartners.CLANDMARK;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTRADEAREA";
                parameter.Value = customersPartners.CTRADEAREA;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CTRADELANDMARK";
                parameter.Value = customersPartners.CTRADELANDMARK;
                command.Parameters.Add(parameter);



                parameter = command.CreateParameter();
                parameter.ParameterName = "@CCATEGORY";
                parameter.Value = customersPartners.CCATEGORY;
                command.Parameters.Add(parameter);
                dbContext.Database.OpenConnection();

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = customersPartners.COMPANYID;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = customersPartners.DEPTID;
                command.Parameters.Add(parameter);
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
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
