using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddUpdPRequisitionDetailsRepository : IAddUpdPRequisitionDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAddUpdPRequisitionDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<AddUpdPRequisitionDetails> pRequisitionDetailsList)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            foreach (var pRequisitionDetails in pRequisitionDetailsList)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdPRequisitionDetails]";
                    command.CommandType = CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDAID";
                    parameter.Value = pRequisitionDetails.PRDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRAID";
                    parameter.Value = pRequisitionDetails.PRAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDPRODUCT";
                    parameter.Value = pRequisitionDetails.PRDPRODUCT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDDESCRIPTION";
                    parameter.Value = pRequisitionDetails.PRDDESCRIPTION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDQUANTITY";
                    parameter.Value = pRequisitionDetails.PRDQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDUOM";
                    parameter.Value = pRequisitionDetails.PRDUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = pRequisitionDetails.HSNCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = pRequisitionDetails.ISDELETED;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = pRequisitionDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = pRequisitionDetails.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRBASEQTY";
                    parameter.Value = pRequisitionDetails.PRBASEQTY;
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
