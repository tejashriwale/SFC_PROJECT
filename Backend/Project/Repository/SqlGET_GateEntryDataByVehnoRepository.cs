using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateEntryDataByVehnoRepository : IGET_GateEntryDataByVehnoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryDataByVehnoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateEntryDataByVehno>> getasync(GET_GateEntryDataByVehnoDomain input)
        {
            List<GET_GateEntryDataByVehno> list = new List<GET_GateEntryDataByVehno>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntryDataByVehno](@p0 , @p1)order by date desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VEHNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GateEntryDataByVehno
                        {
                            DPKID = reader.GetString("DPKID"),
                            MAID = reader.GetString("MAID"),
                            VEHNO = reader.GetString("VEHNO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            UID = reader.GetString("UID"),
                            DATE = reader.GetString("DATE"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            USERID = reader.GetString("USERID"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
