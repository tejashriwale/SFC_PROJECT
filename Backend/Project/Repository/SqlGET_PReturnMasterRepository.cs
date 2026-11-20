using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PReturnMasterRepository : IGET_PReturnMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PReturnMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PReturnMaster>> getasync(GET_PReturnMasterDomain input)
        {
            List<GET_PReturnMaster> list = new List<GET_PReturnMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PReturnMaster](@p0 , @p1 , @p2 , @p3 , @p4 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.RTYPE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PReturnMaster
                        {
                            PBDATE = reader.GetString("PBDATE"),
                            PRAID = reader.GetString("PRAID"),
                            PBNO = reader.GetString("PBNO"),
                            POTRNNO = reader.GetString("POTRNNO"),
                            PSELLER = reader.GetString("PSELLER"),
                            PRCONSIGNER = reader.GetString("PRCONSIGNER"),
                            PBREFERENCE = reader.GetString("PBREFERENCE"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRDATE = reader.GetString("PRDATE"),
                            PRNAMT = reader.GetDouble("PRNAMT"),
                            PRGAMT = reader.GetDouble("PRGAMT"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PReturnMaster>> getkeyword(GET_PReturnMasterDomain input)
        {
            List<GET_PReturnMaster> list = new List<GET_PReturnMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PReturnMaster](@p0 , @p1 , @p2 , @p3 , @p4 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.RTYPE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PReturnMaster
                        {
                            PBDATE = reader.GetString("PBDATE"),
                            PRAID = reader.GetString("PRAID"),
                            PBNO = reader.GetString("PBNO"),
                            POTRNNO = reader.GetString("POTRNNO"),
                            PSELLER = reader.GetString("PSELLER"),
                            PRCONSIGNER = reader.GetString("PRCONSIGNER"),
                            PBREFERENCE = reader.GetString("PBREFERENCE"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRDATE = reader.GetString("PRDATE"),
                            PRNAMT = reader.GetDouble("PRNAMT"),
                            PRGAMT = reader.GetDouble("PRGAMT"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
