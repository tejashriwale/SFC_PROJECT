using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public class SqlWareHouseRepository : IWareHouseRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlWareHouseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        async Task<List<ApiResponseMessage>> IWareHouseRepository.WareHouseAsync(WareHouseMaster wareHouse)
        {
            List<ApiResponseMessage> responseMessages = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdWarehouseMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@WAID";
                parameter.Value = wareHouse.WAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WNAME";
                parameter.Value = wareHouse.WNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WAVAILABLECAPCITY";
                parameter.Value = wareHouse.WAVAILABLECAPCITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WINTERCOMEXTENSION";
                parameter.Value = wareHouse.WINTERCOMEXTENSION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WADDRESS";
                parameter.Value = wareHouse.WADDRESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WCITY";
                parameter.Value = wareHouse.WCITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WSTOREID";
                parameter.Value = wareHouse.WSTOREID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WSTATE";
                parameter.Value = wareHouse.WSTATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WLANDMARK";
                parameter.Value = wareHouse.WLANDMARK;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WAREA";
                parameter.Value = wareHouse.WAREA;
                command.Parameters.Add(parameter);

                //parameter = command.CreateParameter();
                //parameter.ParameterName = "@WDESCRIPTION";
                //parameter.Value = wareHouse.WDESCRIPTION;
                //command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WDESCRIPTION";
                parameter.Value = wareHouse.WDESCRIPTION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = wareHouse.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = wareHouse.DEPTID;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        responseMessages.Add(new ApiResponseMessage
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues")
                        });
                    }
                }
            }
            return responseMessages;
        }  
    }
}
