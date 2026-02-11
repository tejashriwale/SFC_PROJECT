using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdSFamilyInformationRepository : ISP_AddUpdSFamilyInformationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdSFamilyInformationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdSFamilyInformation> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdSFamilyInformation in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdSFamilyInformation]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@DSFAID";
                    parameter.Value = SP_AddUpdSFamilyInformation.DSFAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SRAID";
                    parameter.Value = SP_AddUpdSFamilyInformation.SRAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@NAME";
                    parameter.Value = SP_AddUpdSFamilyInformation.NAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RELATION";
                    parameter.Value = SP_AddUpdSFamilyInformation.RELATION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@AGE";
                    parameter.Value = SP_AddUpdSFamilyInformation.AGE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EDUCATION";
                    parameter.Value = SP_AddUpdSFamilyInformation.EDUCATION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BUSINESS";
                    parameter.Value = SP_AddUpdSFamilyInformation.BUSINESS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdSFamilyInformation.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdSFamilyInformation.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdSFamilyInformation.ISDELETED;
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
            }
            return result;
        }
    }
}
