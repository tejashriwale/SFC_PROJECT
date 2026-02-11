using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateEntryDetailRepository : IGET_GateEntryDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateEntryDetail>> getasync(GET_GateEntryDetailDomain input)
        {
            List<GET_GateEntryDetail> result = new List<GET_GateEntryDetail>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateEntryDetail]('" + input.DPKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.DATE + "')ORDER BY ISVERIFIED,DATEO DESC";

                dbContext.Database.OpenConnection();


                using (var reader = await  command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_GateEntryDetail
                        {
                            DATE = reader.GetString("DATE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            DPKID = reader.GetString("DPKID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            MAID = reader.GetString("MAID"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            UID = reader.GetString("UID"),
                            DATEO = reader.GetDateTime("DATEO"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            ISVERIFIED = reader.GetBoolean("ISVERIFIED")
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_GateEntryDetail>> getkeyword(GET_GateEntryDetailDomain input)
        {
            List<GET_GateEntryDetail> result = new List<GET_GateEntryDetail>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntryDetail]('" + input.DPKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.DATE + "')ORDER BY ISVERIFIED,DATEO DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GateEntryDetail
                        {
                            DATE = reader.GetString("DATE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            DPKID = reader.GetString("DPKID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            MAID = reader.GetString("MAID"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            UID = reader.GetString("UID"),
                            DATEO = reader.GetDateTime("DATEO"),

                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            ISVERIFIED = reader.GetBoolean("ISVERIFIED")
                        });
                    }
                }
            }
            return result;
        }
    }
}
