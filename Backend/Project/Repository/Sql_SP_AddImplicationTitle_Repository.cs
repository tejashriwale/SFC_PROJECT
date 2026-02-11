using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class Sql_SP_AddImplicationTitle_Repository : I_SP_AddImplicationTitle_Repository
    {
        private readonly ApplicationDbContext dbContext;
        public Sql_SP_AddImplicationTitle_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(_SP_AddImplicationTitle_ sP_AddImplicationTitle_)
        {
           List<ApiResponseMessage> list = new List<ApiResponseMessage> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "[SP_AddImplicationTitle]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@ImplicationAutoID";
                parameter.Value = sP_AddImplicationTitle_.ImplicationAutoID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ImplicationTitle";
                parameter.Value = sP_AddImplicationTitle_.ImplicationTitle;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ImplicationValue";
                parameter.Value = sP_AddImplicationTitle_.ImplicationValue;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ImplicationGroup";
                parameter.Value = sP_AddImplicationTitle_.ImplicationGroup;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ImplicationOrder";
                parameter.Value = sP_AddImplicationTitle_.ImplicationOrder;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
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
            return list;
        }
    }
}
