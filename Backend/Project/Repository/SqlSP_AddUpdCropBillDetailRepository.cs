using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;
using System.Linq;

namespace Project.Repository
{
    public class SqlSP_AddUpdCropBillDetailRepository : ISP_AddUpdCropBillDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdCropBillDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdCropBillDetail> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdCropBillDetail in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdCropBillDetail]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@CDBID";
                    parameter.Value = SP_AddUpdCropBillDetail.CDBID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CBKID";
                    parameter.Value = SP_AddUpdCropBillDetail.CBKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CDAID";
                    parameter.Value = SP_AddUpdCropBillDetail.CDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DATE";
                    parameter.Value = SP_AddUpdCropBillDetail.DATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VEHNO";
                    parameter.Value = SP_AddUpdCropBillDetail.VEHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PACKER";
                    parameter.Value = SP_AddUpdCropBillDetail.PACKER;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WORKER";
                    parameter.Value = SP_AddUpdCropBillDetail.WORKER;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PSIZE";
                    parameter.Value = SP_AddUpdCropBillDetail.PSIZE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PQUNTITY";
                    parameter.Value = SP_AddUpdCropBillDetail.PQUNTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WEIGHT";
                    parameter.Value = SP_AddUpdCropBillDetail.WEIGHT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CRATECOUNT";
                    parameter.Value = SP_AddUpdCropBillDetail.CRATECOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROPINVERT";
                    parameter.Value = SP_AddUpdCropBillDetail.CROPINVERT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdCropBillDetail.COMPANYID;
                    command.Parameters.Add(parameter);
                    dbContext.Database.OpenConnection();

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdCropBillDetail.DEPTID;
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
