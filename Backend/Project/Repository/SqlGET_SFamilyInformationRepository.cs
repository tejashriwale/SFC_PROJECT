using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SFamilyInformationRepository : IGET_SFamilyInformationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SFamilyInformationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SFamilyInformation>> getasync(GET_SFamilyInformationDomain input)
        {
            List<GET_SFamilyInformation> list = new List<GET_SFamilyInformation>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            { 
                command.CommandText = "SELECT * FROM[GET_SFamilyInformation]('" + input.SRAID + "' , '" + input.DSFAID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SFamilyInformation
                        {
                            AGE = reader.GetInt32("AGE"),
                            DSFAID = reader.GetString("DSFAID"),
                            BUSINESS = reader.GetString("BUSINESS"),
                            SRAID = reader.GetString("SRAID"),
                            EDUCATION = reader.GetString("EDUCATION"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            NAME = reader.GetString("NAME"),
                            RELATION = reader.GetString("RELATION"),
                            BUSINESSNAME = reader.GetString("BUSINESSNAME"),
                            RELATIONNAME = reader.GetString("RELATIONNAME")

                        });
                    }
                }
            }
            return list;
        }
    }
}
