using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateEntryDetailAllAppRepository : IGET_GateEntryDetailAllAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryDetailAllAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateEntryDetailAllApp>> getasync(GET_GateEntryDetailAllAppDomain input)
        {
            List<GET_GateEntryDetailAllApp> result = new List<GET_GateEntryDetailAllApp>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntryDetailAllApp]('" + input.MAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        result.Add(new GET_GateEntryDetailAllApp
                        {
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            DATE = reader.GetString("DATE"),
                            DPKID = reader.GetString("DPKID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            MAID = reader.GetString("MAID"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            SYSDT = reader.GetString("SYSDT"),
                            CROPLABEL = reader.GetString("CROPLABEL")


                        });
                    }
                }
            }
            return result;
        }
    }
}
