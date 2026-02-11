using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerFamilyDetailRepository : IGET_FarmerFamilyDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerFamilyDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerFamilyDetail>> getasync(GET_FarmerFamilyDetailDomain input)
        {
            List<GET_FarmerFamilyDetail> list = new List<GET_FarmerFamilyDetail>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerFamilyDetail]('" + input.FADDHARNO + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerFamilyDetail
                        {
                            FAID = reader.GetString("FAID"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            FCITY = reader.GetString("FCITY"),
                            
                        });
                    }
                }
            }
            return list;
        }
    }
    
}
