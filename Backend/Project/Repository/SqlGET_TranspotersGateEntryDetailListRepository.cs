using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TranspotersGateEntryDetailListRepository : IGET_TranspotersGateEntryDetailListRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TranspotersGateEntryDetailListRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_TranspotersGateEntryDetailList>> getasync(GET_TranspotersGateEntryDetailListDomain input)
        {
            List<GET_TranspotersGateEntryDetailList> list = new List<GET_TranspotersGateEntryDetailList>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_TranspotersGateEntryDetailList](@p0 , @p1 , @p2)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.TAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TranspotersGateEntryDetailList
                        {
                            DPKID = reader.GetString("DPKID"),
                            MAID = reader.GetString("MAID"),
                            VEHNO = reader.GetString("VEHNO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            DATE = reader.GetString("DATE"),
                            SYSDT = reader.GetString("SYSDT"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            CROPLABEL = reader.GetString("CROPLABEL"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
