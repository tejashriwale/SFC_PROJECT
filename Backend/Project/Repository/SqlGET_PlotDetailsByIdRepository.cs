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
                command.CommandText = "SELECT * FROM [GET_PlotDetailsById]('" + input.PLTAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
