using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdShedulerDetailRepository : ISP_AddUpdShedulerDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdShedulerDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdShedulerDetail> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdShedulerDetail in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdShedulerDetail]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@SHDAID";
                    parameter.Value = SP_AddUpdShedulerDetail.SHDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SHAID";
                    parameter.Value = SP_AddUpdShedulerDetail.SHAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SHSDATE";
                    parameter.Value = SP_AddUpdShedulerDetail.SHSDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VDAID";
                    parameter.Value = SP_AddUpdShedulerDetail.VDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PACKER";
                    parameter.Value = SP_AddUpdShedulerDetail.PACKER;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WORKER";
                    parameter.Value = SP_AddUpdShedulerDetail.WORKER;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VEHNO";
                    parameter.Value = SP_AddUpdShedulerDetail.VEHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PSIZE";
                    parameter.Value = SP_AddUpdShedulerDetail.PSIZE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PQUNTITY";
                    parameter.Value = SP_AddUpdShedulerDetail.PQUNTITY;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdShedulerDetail.ISDELETED;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CGRADE";
                    parameter.Value = SP_AddUpdShedulerDetail.CGRADE;
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
            }
            return list;
        }
    }
}
