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
                command.CommandText = "SELECT * FROM [GET_GateEntryDetailByIdApp](@p0 ,@p1 , @p2 , @p3 ,@p4)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.FAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.UID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
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
