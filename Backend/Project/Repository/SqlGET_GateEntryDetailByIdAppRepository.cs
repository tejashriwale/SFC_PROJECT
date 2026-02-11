using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_GateEntryDetailByIdAppRepository : IGET_GateEntryDetailByIdAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryDetailByIdAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateEntryDetailByIdApp>> getasync(GET_GateEntryDetailByIdAppDomain input)
        {
            List<GET_GateEntryDetailByIdApp> result = new List<GET_GateEntryDetailByIdApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntryDetailByIdApp]('" + input.FAID + "' ,'" + input.UID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' ,'" + input.DATE + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GateEntryDetailByIdApp
                        {
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            DATE = reader.GetString("DATE"),
                            DPKID = reader.GetString("DPKID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            MAID = reader.GetString("MAID"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CROP_NAME = reader.GetString("CROP_NAME"),
                            UID = reader.GetString("UID"),
                            USERID = reader.GetString("USERID"),
                            GFULLNAME = reader.GetString("GFULLNAME")

                        });
                    }
                }
            }
            return result;
        }
    }
}
