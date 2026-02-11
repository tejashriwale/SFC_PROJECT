using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdVyapariSanctionRepository : ISP_AddUpdVyapariSanctionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdVyapariSanctionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdVyapariSanction input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdVyapariSanction]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@VSAID";
                parameter.Value = input.VSAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@VNAME";
                parameter.Value = input.VNAME;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = input.UAID;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@AMOUNT";
                parameter.Value = input.AMOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = input.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = input.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DATE";
                parameter.Value = input.DATE;
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
