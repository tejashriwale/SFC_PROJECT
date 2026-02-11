using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdVisitorDetailRepository : ISP_AddUpdVisitorDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdVisitorDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdVisitorDetail> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdVisitorDetail in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdVisitorDetail]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@VDAID";
                    parameter.Value = SP_AddUpdVisitorDetail.VDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROPTYPE";
                    parameter.Value = SP_AddUpdVisitorDetail.CROPTYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROPVARIETY";
                    parameter.Value = SP_AddUpdVisitorDetail.CROPVARIETY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROPAREA";
                    parameter.Value = SP_AddUpdVisitorDetail.CROPAREA;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EXPECTEDHARVESTINGDATE";
                    parameter.Value = SP_AddUpdVisitorDetail.EXPECTEDHARVESTINGDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EXPECTEDPRODUCTION";
                    parameter.Value = SP_AddUpdVisitorDetail.EXPECTEDPRODUCTION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PHOTOES";
                    parameter.Value = SP_AddUpdVisitorDetail.PHOTOES;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RATE";
                    parameter.Value = SP_AddUpdVisitorDetail.RATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ADVANCEMONEY";
                    parameter.Value = SP_AddUpdVisitorDetail.ADVANCEMONEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_AddUpdVisitorDetail.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdVisitorDetail.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdVisitorDetail.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VAID";
                    parameter.Value = SP_AddUpdVisitorDetail.VAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdVisitorDetail.ISDELETED;
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
