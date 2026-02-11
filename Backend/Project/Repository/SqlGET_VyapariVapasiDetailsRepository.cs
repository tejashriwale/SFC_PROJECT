using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VyapariVapasiDetailsRepository : IGET_VyapariVapasiDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariVapasiDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
      
        public async Task<List<GET_VyapariVapasiDetails>> getasync(GET_VyapariVapasiDetailsDomain input)
        {

            List<GET_VyapariVapasiDetails> result = new List<GET_VyapariVapasiDetails>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariVapasiDetails]('" + input.VPID + "'  , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariVapasiDetails
                        {
                            VPID = reader.GetString("VPID"),
                            VSAID = reader.GetString("VSAID"),
                            VSDAID = reader.GetString("VSDAID"),
                            DUE_DAY = reader.GetString("DUE_DAY"),
                            TO_DAY = reader.GetString("TO_DAY"),
                           
                            VAPASI = reader.GetString("VAPASI"),

                   

                        });
                    }
                }
            }
            return result;
        }
    }
}
