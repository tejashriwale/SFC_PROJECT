using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public class SqlSP_AddUpdAuctionShedDeatilRepository : ISP_AddUpdAuctionShedDeatilRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdAuctionShedDeatilRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdAuctionShedDeatil> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdAuctionShedDeatil in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdAuctionShedDeatil]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@DPKID";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.DPKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@APKID";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.APKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VYAPARI";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.VYAPARI;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@JAALI";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.JAALI;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WEIGHT";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.WEIGHT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WEIGHTLESS";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.WEIGHTLESS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VILLAGE";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.VILLAGE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROP_TYPE";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.CROP_TYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@FARMERNAME";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.FARMERNAME;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOKEN";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.TOKEN;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@MAID";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.MAID;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RATE";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.RATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.DEPTID;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdAuctionShedDeatil.ISDELETED;
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
