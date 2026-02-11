using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VoucherMasterNaveRepository : IGET_VoucherMasterNaveRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VoucherMasterNaveRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VoucherMasterNave>> getasync(GET_VoucherMasterNaveDomain input)
        {
            List<GET_VoucherMasterNave> list = new List<GET_VoucherMasterNave>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VoucherMasterNave]('" + input.VMKID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VoucherMasterNave
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
