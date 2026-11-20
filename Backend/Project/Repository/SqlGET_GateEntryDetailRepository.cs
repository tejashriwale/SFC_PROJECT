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
                command.CommandText = "SELECT Top 20 * FROM [GET_GateEntryDetail](@p0 , @p1 , @p2 , @p3 , @p4)ORDER BY ISVERIFIED,DATEO DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DPKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }

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
                command.CommandText = "SELECT * FROM [GET_GateEntryDetail](@p0 , @p1 , @p2 , @p3 , @p4)ORDER BY ISVERIFIED,DATEO DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DPKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
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
