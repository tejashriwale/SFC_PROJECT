using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionMasterRepositroy : IGET_PRequisitionMasterRepositroy
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_PRequisitionMasterRepositroy(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PRequisitionMaster>> getasync(GET_PRequisitionMasterDomain input)
        {
            List<GET_PRequisitionMaster> list = new List<GET_PRequisitionMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PRequisitionMaster](@p0, @p1 ,@p2,@p3)order by PRQDATE1 desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionMaster
                        {
                            PRAID = reader.GetString("PRAID"),
                            PRQDATE = reader.GetString("PRQDATE"),
                            PREQNO = reader.GetString("PREQNO"),
                            PREXPCDATE = reader.GetString("PREXPCDATE"),
                            PRCATEGORY = reader.GetString("PRCATEGORY"),
                            PRVANDORID = reader.GetString("PRVANDORID"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRTERMANDCONDITION = reader.GetString("PRTERMANDCONDITION"),
                            PRMDATE = reader.GetString("PRMDATE"),
                            PRNO = reader.GetString("PRNO"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PRequisitionMaster>> getkeyword(GET_PRequisitionMasterDomain input)
        {
            List<GET_PRequisitionMaster> list = new List<GET_PRequisitionMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionMaster](@p0, @p1 ,@p2,@p3)order by PRQDATE1 desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionMaster
                        {
                            PRAID = reader.GetString("PRAID"),
                            PRQDATE = reader.GetString("PRQDATE"),
                            PREQNO = reader.GetString("PREQNO"),
                            PREXPCDATE = reader.GetString("PREXPCDATE"),
                            PRCATEGORY = reader.GetString("PRCATEGORY"),
                            PRVANDORID = reader.GetString("PRVANDORID"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRTERMANDCONDITION = reader.GetString("PRTERMANDCONDITION"),
                            PRMDATE = reader.GetString("PRMDATE"),
                            PRNO = reader.GetString("PRNO"),
                        });
                    }
                }
            }
            return list;
        }
            
    }
}
