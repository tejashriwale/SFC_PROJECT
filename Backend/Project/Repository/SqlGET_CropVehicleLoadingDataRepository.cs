using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropVehicleLoadingDataRepository : IGET_CropVehicleLoadingDataRepository
    {
        private readonly  ApplicationDbContext dbContext;
        public SqlGET_CropVehicleLoadingDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_CropVehicleLoadingData>> getasync(GET_CropVehicleLoadingDataDomain input)
        {
            List<GET_CropVehicleLoadingData> list = new List<GET_CropVehicleLoadingData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CropVehicleLoadingData]('" + input.COMPANYID + "', '" + input.DEPTID + "' , '" + input.CGRADE + "', '" + input.CROP + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropVehicleLoadingData
                        {
                            STAID = reader.GetString("STAID"),
                            CHARGES = reader.GetDouble("CHARGES"),
                            CGRADE = reader.GetString("CGRADE"),
                            CIAID = reader.GetString("CIAID"),
                            CROP = reader.GetString("CROP"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            RATE = reader.GetDouble("RATE"),
                            TOTCR = reader.GetInt32("TOTCR"),
                            TOTWT = reader.GetDouble("TOTWT"),
                            TRNDT = reader.GetString("TRNDT"),
                            MSIZE = reader.GetString("MSIZE"),
                            GRADE = reader.GetString("GRADE"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
