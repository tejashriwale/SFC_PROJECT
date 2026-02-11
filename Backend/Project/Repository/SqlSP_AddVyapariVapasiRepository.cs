using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddVyapariVapasiRepository : ISP_AddVyapariVapasiRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddVyapariVapasiRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddVyapariVapasi> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddVyapariVapasi in input)
            { 
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddVyapariVapasi]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@VPID";
                    parameter.Value = SP_AddVyapariVapasi.VPID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VSAID";
                    parameter.Value = SP_AddVyapariVapasi.VSAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VSDAID";
                    parameter.Value = SP_AddVyapariVapasi.VSDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DUE_DAY";
                    parameter.Value = SP_AddVyapariVapasi.DUE_DAY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TO_DAY";
                    parameter.Value = SP_AddVyapariVapasi.TO_DAY;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VAPASI";
                    parameter.Value = SP_AddVyapariVapasi.VAPASI;
                    command.Parameters.Add(parameter);

  

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddVyapariVapasi.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddVyapariVapasi.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddVyapariVapasi.ISDELETED;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_AddVyapariVapasi.UAID;
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
