using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateEntry_VTRepository : IGET_GateEntry_VTRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntry_VTRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateEntry_VT>> getasync(GET_GateEntry_VTDomain input)
        {
            List<GET_GateEntry_VT> list = new List<GET_GateEntry_VT>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntry_VT](@p0 , @p1 , @p2, @p3)order by date desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VEHNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GateEntry_VT
                        {
                             DPKID = reader.GetString("DPKID"),
                             MAID = reader.GetString("MAID"),
                             VEHNO = reader.GetString("VEHNO"),
                             TOKNNO = reader.GetString("TOKNNO"),
                             DATE = reader.GetString("DATE"),
                             FULLNAME = reader.GetString("FULLNAME"),
                             AADHARNO = reader.GetString("AADHARNO"),
                             MOBILENO = reader.GetString("MOBILENO"),
                             VILLAGE = reader.GetString("VILLAGE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            USERID = reader.GetString("USERID"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
