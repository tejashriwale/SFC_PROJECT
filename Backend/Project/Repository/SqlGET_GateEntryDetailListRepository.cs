using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateEntryDetailListRepository : IGET_GateEntryDetailListRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryDetailListRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateEntryDetailList>> getasync(GET_GateEntryDetailListDomain input)
        {
            List<GET_GateEntryDetailList> result = new List<GET_GateEntryDetailList>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntryDetailList]('" + input.MAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "', '"+input.DATE+ "')order by date desc";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_GateEntryDetailList
                        {
                            DATE = reader.GetString("DATE"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            DPKID = reader.GetString("DPKID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            MAID = reader.GetString("MAID"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            SYSDT = reader.GetString("SYSDT"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            USERID = reader.GetString("USERID"),

                        });
                    }
                }
            }
            return result;
        }
    }
}
