using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_ShedulerMasterDataRepository : IGET_ShedulerMasterDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ShedulerMasterDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_ShedulerMasterData>> getasync(GET_ShedulerMasterDataDomain input)
        {
            List<GET_ShedulerMasterData> list = new List<GET_ShedulerMasterData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ShedulerMasterData] (@p0 , @p1 , @p2 ,  @p3 , @p4)ORDER BY ISCOMPLITED , ODATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ShedulerMasterData
                        {
                           AREA = reader.GetString("AREA"),
                           CONTACTNO = reader.GetString("CONTACTNO"),
                           CROPNAME = reader.GetString("CROPNAME"),
                           CTYPE = reader.GetString("CTYPE"),
                           EHDATE = reader.GetString("EHDATE"),
                           FAID = reader.GetString("FAID"),
                           FARMERNAME = reader.GetString("FARMERNAME"),
                           RATE = reader.GetDouble("RATE"),
                           VAID = reader.GetString("VAID"),
                           VDAID = reader.GetString("VDAID"),
                           EPRODUCTION = reader.GetString("EPRODUCTION"),
                           CVARIETY = reader.GetString("CVARIETY"),
                           ISCOMPLITED = reader.GetString("ISCOMPLITED"),
                            SHAID = reader.GetString("SHAID"),
                            CROPID = reader.GetString("CROPID"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_ShedulerMasterData>> getkeyword(GET_ShedulerMasterDataDomain input)
        {
            List<GET_ShedulerMasterData> list = new List<GET_ShedulerMasterData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ShedulerMasterData] ( @p0 , @p1, @p2 ,  @p3 , @p4)ORDER BY ISCOMPLITED , ODATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ShedulerMasterData
                        {
                            AREA = reader.GetString("AREA"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CTYPE = reader.GetString("CTYPE"),
                            EHDATE = reader.GetString("EHDATE"),
                            FAID = reader.GetString("FAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            RATE = reader.GetDouble("RATE"),
                            VAID = reader.GetString("VAID"),
                            VDAID = reader.GetString("VDAID"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            ISCOMPLITED = reader.GetString("ISCOMPLITED"),
                            SHAID = reader.GetString("SHAID"),
                            CROPID = reader.GetString("CROPID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
