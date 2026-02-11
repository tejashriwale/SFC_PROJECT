using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdPlotDetailsDtoRepository : ISP_AddUpdPlotDetailsDtoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdPlotDetailsDtoRepository(ApplicationDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdPlotDetails> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdPlotDetails in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdPlotDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@PLTDAID";
                    parameter.Value = SP_AddUpdPlotDetails.PLTDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PLOTNO";
                    parameter.Value = SP_AddUpdPlotDetails.PLOTNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PLOTAREA";
                    parameter.Value = SP_AddUpdPlotDetails.PLOTAREA;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PLOTVARIETY";
                    parameter.Value = SP_AddUpdPlotDetails.PLOTVARIETY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@NOOFPLANTS";
                    parameter.Value = SP_AddUpdPlotDetails.NOOFPLANTS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PLOTTYPE";
                    parameter.Value = SP_AddUpdPlotDetails.PLOTTYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@LATLONG";
                    parameter.Value = SP_AddUpdPlotDetails.LATLONG;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRONDT";
                    parameter.Value = SP_AddUpdPlotDetails.PRONDT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HARVESTDT";
                    parameter.Value = SP_AddUpdPlotDetails.HARVESTDT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@NOOFFRUIT";
                    parameter.Value = SP_AddUpdPlotDetails.NOOFFRUIT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DAYSHARVEST";
                    parameter.Value = SP_AddUpdPlotDetails.DAYSHARVEST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@REMARK";
                    parameter.Value = SP_AddUpdPlotDetails.REMARK;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PLOTPHOTO";
                    parameter.Value = SP_AddUpdPlotDetails.PLOTPHOTO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GEOLOCATION";
                    parameter.Value = SP_AddUpdPlotDetails.GEOLOCATION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PLTAID";
                    parameter.Value = SP_AddUpdPlotDetails.PLTAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EXPTURN";
                    parameter.Value = SP_AddUpdPlotDetails.EXPTURN;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdPlotDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdPlotDetails.DEPTID;
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
