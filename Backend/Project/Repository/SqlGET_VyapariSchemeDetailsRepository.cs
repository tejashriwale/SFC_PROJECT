using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariSchemeDetailsRepository : IGET_VyapariSchemeDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariSchemeDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariSchemeDetails>> getasync(GET_VyapariSchemeDetailsDomain input)
        {
            List<GET_VyapariSchemeDetails> result = new List<GET_VyapariSchemeDetails>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariSchemeDetails]('" + input.VSAID + "'  , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariSchemeDetails
                        {
                            VSAID = reader.GetString("VSAID"),
                            STARTDATE = reader.GetString("STARTDATE"),
                            ENDDATE = reader.GetString("ENDDATE"),
                            DUEDATE = reader.GetString("DUEDATE"),
                            KALAVADHI = reader.GetString("KALAVADHI"),
                            VSDAID = reader.GetString("VSDAID"),
                        


                        });
                    }
                }
            }
            return result;
        }
    }
}
