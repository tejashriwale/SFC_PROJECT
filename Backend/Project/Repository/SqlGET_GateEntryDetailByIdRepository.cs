using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_GateEntryDetailByIdRepository : IGET_GateEntryDetailByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryDetailByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GateEntryDetailById>> getasync(GET_GateEntryDetailByIdDomain inupt)
        {
            List<GET_GateEntryDetailById> list = new List<GET_GateEntryDetailById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateEntryDetailById](@p0 ,@p1 ,@p2 , @p3 , @p4)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(inupt.VEHNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(inupt.UID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(inupt.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(inupt.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(inupt.DATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_GateEntryDetailById
                        {
                            DPKID = reader.GetString("DPKID"),
                           
                            MAID = reader.GetString("MAID"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            VILLAGE = reader.GetString("VILLAGE"),                        
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            VEHNO = reader.GetString("VEHNO"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            DATE = reader.GetString("DATE"),
                            UID = reader.GetString("UID"),
                          
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                        });
                    }
                }
            }
            return list;
        }

      
    }
}
