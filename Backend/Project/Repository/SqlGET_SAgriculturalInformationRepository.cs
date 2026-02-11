using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SAgriculturalInformationRepository : IGET_SAgriculturalInformationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SAgriculturalInformationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SAgriculturalInformation>> getasync(GET_SAgriculturalInformationDomain input)
        {
            List<GET_SAgriculturalInformation> result = new List<GET_SAgriculturalInformation>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_SAgriculturalInformation]('" + input.SRAID + "' , '" + input.DSAID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_SAgriculturalInformation
                        {
                           SRAID = reader.GetString("SRAID"), 
                            DSAID = reader.GetString("DSAID"),
                            SURVEYNUMBER = reader.GetString("SURVEYNUMBER"),
                            CLUSTERNUMBER = reader.GetString("CLUSTERNUMBER"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            LANDAREA = reader.GetString("LANDAREA"),
                            LANDTYPE = reader.GetString("LANDTYPE"),
                            LANDNAME = reader.GetString("LANDNAME"),

                        });
                    }
                }
            }
            return result;
        }
    }
}
