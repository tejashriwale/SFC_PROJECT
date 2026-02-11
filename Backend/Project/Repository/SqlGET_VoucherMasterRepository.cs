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
                command.CommandText = "SELECT * FROM [GET_VoucherMaster]('" + input.VMKID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
