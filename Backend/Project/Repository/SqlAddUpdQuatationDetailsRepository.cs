using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddUpdQuatationDetailsRepository : IAddUpdQuatationDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAddUpdQuatationDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<AddUpdQuatationDetails> quatationDetails)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var AddUpdQuatationDetails in quatationDetails)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddQuatationDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@QAMDAID";
                    parameter.Value = AddUpdQuatationDetails.QAMDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QAMAID";
                    parameter.Value = AddUpdQuatationDetails.QAMAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QPRODUCT";
                    parameter.Value = AddUpdQuatationDetails.QPRODUCT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QQUANTITY";
                    parameter.Value = AddUpdQuatationDetails.QQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QUOM";
                    parameter.Value = AddUpdQuatationDetails.QUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QRATE";
                    parameter.Value = AddUpdQuatationDetails.QRATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QTAMT";
                    parameter.Value = AddUpdQuatationDetails.QTAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QIGST";
                    parameter.Value = AddUpdQuatationDetails.QIGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QSGST";
                    parameter.Value = AddUpdQuatationDetails.QSGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QCGST";
                    parameter.Value = AddUpdQuatationDetails.QCGST;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = AddUpdQuatationDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = AddUpdQuatationDetails.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QCESS";
                    parameter.Value = AddUpdQuatationDetails.QCESS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = AddUpdQuatationDetails.HSNCODE;
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
