using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SRequisitionMasterRepository : IGET_SRequisitionMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SRequisitionMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SRequisitionMaster>> getasync(GET_SRequisitionMasterDomain input)
        {
            List<GET_SRequisitionMaster> list = new List<GET_SRequisitionMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SRequisitionMaster](@p0 , @p1 ,@p2 , @p3)ORDER BY SYSDT  desc ";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SRequisitionMaster
                        {
                            SRAID = reader.GetString("SRAID"),
                            SRQDATE = reader.GetString("SRQDATE"),
                            SREQNO = reader.GetString("SREQNO"),
                            SREXPCDATE = reader.GetString("SREXPCDATE"),
                            SRCATEGORY = reader.GetString("SRCATEGORY"),
                            SRVANDORID = reader.GetString("SRVANDORID"),
                            SRNARRATION = reader.GetString("SRNARRATION"),
                            SRTERMANDCONDITION = reader.GetString("SRTERMANDCONDITION"),
                            SRMDATE = reader.GetString("SRMDATE"),
                            SRNO = reader.GetString("SRNO"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            SYSDT = reader.GetDateTime("SYSDT")
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SRequisitionMaster>> getkeyword(GET_SRequisitionMasterDomain input)
        {
            List<GET_SRequisitionMaster> list = new List<GET_SRequisitionMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SRequisitionMaster](@p0 , @p1 ,@p2 , @p3)ORDER BY SYSDT  desc ";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SRequisitionMaster
                        {
                            SRAID = reader.GetString("SRAID"),
                            SRQDATE = reader.GetString("SRQDATE"),
                            SREQNO = reader.GetString("SREQNO"),
                            SREXPCDATE = reader.GetString("SREXPCDATE"),
                            SRCATEGORY = reader.GetString("SRCATEGORY"),
                            SRVANDORID = reader.GetString("SRVANDORID"),
                            SRNARRATION = reader.GetString("SRNARRATION"),
                            SRTERMANDCONDITION = reader.GetString("SRTERMANDCONDITION"),
                            SRMDATE = reader.GetString("SRMDATE"),
                            SRNO = reader.GetString("SRNO"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            SYSDT = reader.GetDateTime("SYSDT")
                        });
                    }
                }
            }
            return list;
        }
            
    }
}
