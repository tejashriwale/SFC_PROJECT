using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SingleRowCropInvertDetailRepository : IGET_SingleRowCropInvertDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SingleRowCropInvertDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SingleRowCropInvertDetail>> getasync(GET_SingleRowCropInvertDetailDomain input)
        {
            List<GET_SingleRowCropInvertDetail> result = new List<GET_SingleRowCropInvertDetail>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SingleRowCropInvertDetail]('" + input.CDAID + "','" + input.COMPANYID + "'  , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_SingleRowCropInvertDetail
                        {
                            SHSDATE = reader.GetString("SHSDATE"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BILLNO = reader.GetString("BILLNO"),
                            CDAID = reader.GetString("CDAID"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CRATECOUNT = reader.GetString("CRATECOUNT"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            DATE = reader.GetString("DATE"),
                            FARMERNAME  = reader.GetString("FARMERNAME"),
                            ITEMNE = reader.GetString("ITEMNE"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHT = reader.GetDouble("WEIGHT")
                        });

                    }
                }
            }
            return result;
        }
    }
}
