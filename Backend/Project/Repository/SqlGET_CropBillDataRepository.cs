using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropBillDataRepository : IGET_CropBillDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropBillDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropBillData>> getasync(GET_CropBillDataDomain input)
        {
            List<GET_CropBillData> list = new List<GET_CropBillData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropBillData]( '" + input.CROPID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropBillData
                        {
                            CDAID = reader.GetString("CDAID"),
                            CROPID = reader.GetString("CROPID"),
                            VISITINGDATE = reader.GetString("VISITINGDATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            CRATECOUNT = reader.GetString("CRATECOUNT"),
                            CROPINVERT = reader.GetString("CROPINVERT"),
                            PQUNTITY = reader.GetString("PQUNTITY"),
                            VEHNO = reader.GetString("VEHNO"),
                            SIZE = reader.GetString("SIZE"),
                            COUNT = reader.GetString("COUNT"),
                            PSIZE = reader.GetString("PSIZE"),
                            PACKER = reader.GetString("PACKER"),
                            PACKERNAME = reader.GetString("PACKERNAME"),
                            WORKER = reader.GetString("WORKER"),
                            WORKERNAME = reader.GetString("WORKERNAME"),
                            RATE = reader.GetDouble("RATE"),
                            ADVANCE = reader.GetDouble("ADVANCE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
