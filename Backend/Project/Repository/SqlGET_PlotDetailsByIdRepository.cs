using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PlotDetailsByIdRepository : IGET_PlotDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PlotDetailsByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PlotDetailsById>> getasync(GET_PlotDetailsByIdDomain input)
        {
            List<GET_PlotDetailsById> result = new List<GET_PlotDetailsById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PlotDetailsById](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PLTAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await reader .ReadAsync())
                    {
                        result.Add(new GET_PlotDetailsById
                        {
                            GEOLOCATION = reader.GetString("GEOLOCATION"),
                            DAYSHARVEST = reader.GetString("DAYSHARVEST"),
                            EXPTURN = reader.GetString("EXPTURN"),
                            HARVESTDT = reader.GetString("HARVESTDT"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            LATLONG = reader.GetString("LATLONG"),
                            NOOFFRUIT = reader.GetString("NOOFFRUIT"),
                            NOOFPLANTS = reader.GetString("NOOFPLANTS"),
                            PLTAID = reader.GetString("PLTAID"),
                            PLTDAID = reader.GetString("PLTDAID"),
                            PLOTAREA = reader.GetString("PLOTAREA"),
                            PLOTPHOTO = reader.GetString("PLOTPHOTO"),
                            PLOTNO = reader.GetString("PLOTNO"),
                            PLOTTYPE = reader.GetString("PLOTTYPE"),
                            PLOTVARIETY = reader.GetString("PLOTVARIETY"),
                            PRONDT = reader.GetString("PRONDT"),
                            REMARK = reader.GetString("REMARK")

                        });
                    }
                }
            }
            return result;
        }
    }
}
