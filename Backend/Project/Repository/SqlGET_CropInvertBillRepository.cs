using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropInvertBillRepository : IGET_CropInvertBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropInvertBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropInvertBill>> getasync(GET_CropInvertBillDomain input)
        {
            List<GET_CropInvertBill> list = new List<GET_CropInvertBill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CropInvertBill]('" + input.CROPID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertBill
                        {
                        
                            SHAID = reader.GetString("SHAID"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CRATECOUNT = reader.GetString("CRATECOUNT"),
                            DATE = reader.GetString("DATE"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            RATE = reader.GetDouble("RATE"),
                            CROPINVERT = reader.GetString("CROPINVERT"),
                            CTYPE = reader.GetString("CTYPE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            PACKER = reader.GetString("PACKER"),
                            WORKER = reader.GetString("WORKER"),
                            VEHNO = reader.GetString("VEHNO"),
                            FAID = reader.GetString("FAID"),
                            BILLNO = reader.GetString("BILLNO"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
