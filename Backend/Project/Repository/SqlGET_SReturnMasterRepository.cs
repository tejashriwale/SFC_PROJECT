using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SReturnMasterRepository : IGET_SReturnMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SReturnMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SReturnMaster>> getasync(GET_SReturnMasterDomain input)
        {
            List<GET_SReturnMaster> list = new List<GET_SReturnMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SReturnMaster](@p0, @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                { 
                    while(await reader .ReadAsync())
                    {
                        list.Add(new GET_SReturnMaster
                        {
                            SRAID = reader.GetString("SRAID"),
                            SBNO = reader.GetString("SBNO"),
                            SBDATE = reader.GetString("SBDATE"),
                            SOTRNNO = reader.GetString("SOTRNNO"),
                            SSELLER = reader.GetString("SSELLER"),
                            SRCONSIGNER = reader.GetString("SRCONSIGNER"),
                            SBREFEWENCE = reader.GetString("SBREFEWENCE"),
                            SRNARRATION = reader.GetString("SRNARRATION"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            TRANSPORTERNAME = reader.GetString("TRANSPORTERNAME"),
                            SRDATE = reader.GetString("SRDATE"),
                            SRNAMT = reader.GetDouble("SRNAMT"),
                            SRGAMT = reader.GetDouble("SRGAMT"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SReturnMaster>> getkeyword(GET_SReturnMasterDomain input)
        {
            List<GET_SReturnMaster> list = new List<GET_SReturnMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SReturnMaster](@p0, @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SReturnMaster
                        {
                            SRAID = reader.GetString("SRAID"),
                            SBNO = reader.GetString("SBNO"),
                            SBDATE = reader.GetString("SBDATE"),
                            SOTRNNO = reader.GetString("SOTRNNO"),
                            SSELLER = reader.GetString("SSELLER"),
                            SRCONSIGNER = reader.GetString("SRCONSIGNER"),
                            SBREFEWENCE = reader.GetString("SBREFEWENCE"),
                            SRNARRATION = reader.GetString("SRNARRATION"),
                            SRDATE = reader.GetString("SRDATE"),
                            SRNAMT = reader.GetDouble("SRNAMT"),
                            SRGAMT = reader.GetDouble("SRGAMT"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            TRANSPORTERNAME = reader.GetString("TRANSPORTERNAME"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
