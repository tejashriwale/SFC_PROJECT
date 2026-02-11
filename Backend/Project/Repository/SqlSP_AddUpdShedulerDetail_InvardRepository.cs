using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdShedulerDetail_InvardRepository : ISP_AddUpdShedulerDetail_InvardRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdShedulerDetail_InvardRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdShedulerDetail_Invard> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdShedulerDetail_Invard in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdShedulerDetail_Invard]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@SHDAID";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.SHDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SHAID";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.SHAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SHSDATE";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.SHSDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VDAID";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.VDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PACKER";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.PACKER;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WORKER";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.WORKER;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VEHNO";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.VEHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PSIZE";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.PSIZE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PQUNTITY";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.PQUNTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.ISDELETED;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CGRADE";
                    parameter.Value = SP_AddUpdShedulerDetail_Invard.CGRADE;
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
