using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GoodReceiptNoteMasterRepository : IGET_GoodReceiptNoteMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GoodReceiptNoteMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GoodReceiptNoteMaster>> getasync(GET_GoodReceiptNoteMasterDomain input)
        {
           List<GET_GoodReceiptNoteMaster> list = new List<GET_GoodReceiptNoteMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GoodReceiptNoteMaster](@p0 , @p1 , @p2 , @p3)order by GRNDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.GRNAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_GoodReceiptNoteMaster
                        {
                            GRNAID = reader.GetString("GRNAID"),
                            GRNTRANSACTIONID = reader.GetString("GRNTRANSACTIONID"),
                            PCAID = reader.GetString("PCAID"),
                            GRNDATE = reader.GetString("GRNDATE"),
                            GRNNUMBER = reader.GetString("GRNNUMBER"),
                            GRNCHALLANNO = reader.GetString("GRNCHALLANNO"),
                            GRNCHALLANDATE = reader.GetString("GRNCHALLANDATE"),
                            GRNVECHILENO = reader.GetString("GRNVECHILENO"),
                            GRNVENDOR = reader.GetString("GRNVENDOR"),
                            GRNCONSIGNER = reader.GetString("GRNCONSIGNER"),
                            GRNINSPECTEDBY = reader.GetString("GRNINSPECTEDBY"),
                            GRNNARRATION = reader.GetString("GRNNARRATION"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PCREFF = reader.GetString("PCREFF"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_GoodReceiptNoteMaster>> getkeyword(GET_GoodReceiptNoteMasterDomain input)
        {
            List<GET_GoodReceiptNoteMaster> list = new List<GET_GoodReceiptNoteMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GoodReceiptNoteMaster](@p0 , @p1 , @p2 , @p3)order by GRNDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.GRNAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GoodReceiptNoteMaster
                        {
                            GRNAID = reader.GetString("GRNAID"),
                            GRNTRANSACTIONID = reader.GetString("GRNTRANSACTIONID"),
                            PCAID = reader.GetString("PCAID"),
                            GRNDATE = reader.GetString("GRNDATE"),
                            GRNNUMBER = reader.GetString("GRNNUMBER"),
                            GRNCHALLANNO = reader.GetString("GRNCHALLANNO"),
                            GRNCHALLANDATE = reader.GetString("GRNCHALLANDATE"),
                            GRNVECHILENO = reader.GetString("GRNVECHILENO"),
                            GRNVENDOR = reader.GetString("GRNVENDOR"),
                            GRNCONSIGNER = reader.GetString("GRNCONSIGNER"),
                            GRNINSPECTEDBY = reader.GetString("GRNINSPECTEDBY"),
                            GRNNARRATION = reader.GetString("GRNNARRATION"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PCREFF = reader.GetString("PCREFF"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
