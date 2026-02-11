using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdRokdaVyapariBharnaRepository :ISP_AddUpdRokdaVyapariBharnaRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdRokdaVyapariBharnaRepository (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdRokdaVyapariBharna> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach(var SP_AddUpdRokdaVyapariBharna in input)
            {
                {
                    using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "[SP_AddUpdRokdaVyapariBharna]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@VDAID";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.VDAID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TRNDT";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.TRNDT;
                        command.Parameters.Add(parameter);


                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TAMT";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.TAMT;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@BPAMT";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.BPAMT;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@FDUEDT";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.FDUEDT;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@SDUEDT";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.SDUEDT;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@VPAID";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.VPAID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TID";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.TID;
                        command.Parameters.Add(parameter);


                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@STID";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.STID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TRNTID";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.TRNTID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TRNSR";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.TRNSR;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@REFKEY";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.REFKEY;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@UAID";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.UAID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@VREFF";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.VREFF;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@REFF";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.REFF;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@VTYPE";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.VTYPE;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@ISCLOSE";
                        parameter.Value = SP_AddUpdRokdaVyapariBharna.ISCLOSE;
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
                
            }
            return result;
        }
    }
}

