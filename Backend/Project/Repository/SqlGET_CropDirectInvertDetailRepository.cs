using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropDirectInvertDetailRepository : IGET_CropDirectInvertDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropDirectInvertDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropDirectInvertDetail>> getasync(GET_CropDirectInvertDetailDomain input)
        {
            List<GET_CropDirectInvertDetail> list = new List<GET_CropDirectInvertDetail>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropDirectInvertDetail]( '" + input.CROPID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropDirectInvertDetail
                        {
                           CDAID = reader.GetString("CDAID"),
                           CGRADE = reader.GetString("CGRADE"),
                           CRATECOUNT = reader.GetString("CRATECOUNT"),
                           PACKER = reader.GetString("PACKER"),
                           PACKINGSIZE = reader.GetString("PACKINGSIZE"),
                           SHDAID = reader.GetString("SHDAID"),
                           SHSDATE = reader.GetString("SHSDATE"),
                           SHAID = reader.GetString("SHAID"),
                           VDAID = reader.GetString("VDAID"),
                           CROPID = reader.GetString("CROPID"),
                           CROPINVERT = reader.GetString("CROPINVERT"),
                           PQUNTITY = reader.GetString("PQUNTITY"),
                           PSIZE = reader.GetString("PSIZE"),
                           VEHNO = reader.GetString("VEHNO"),
                           WEIGHT = reader.GetDouble("WEIGHT"),
                           RATE = reader.GetDouble("RATE"),
                           WORKER = reader.GetString("WORKER"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
