using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VoucherMasterRepository : IGET_VoucherMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VoucherMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VoucherMaster>> getasync(GET_VoucherMasterDomain input)
        {
            List<GET_VoucherMaster> list = new List<GET_VoucherMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VoucherMaster](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VMKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_VoucherMaster
                        {
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            JAMANAVE = reader.GetString("JAMANAVE"),
                            VMKID = reader.GetString("VMKID"),
                            REFERENCE = reader.GetString("REFERENCE"),
                            VOUCHERTYPE = reader.GetString("VOUCHERTYPE"),
                            REMARK = reader.GetString("REMARK"),
                            DEPOSITE = reader.GetString("DEPOSITE"),
                            FNAME = reader.GetString("FNAME"),
                            DEPOSITENAME = reader.GetString("DEPOSITENAME"),
                            JAMANAVENAME = reader.GetString("JAMANAVENAME"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
