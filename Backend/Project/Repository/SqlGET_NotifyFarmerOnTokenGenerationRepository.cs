using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_NotifyFarmerOnTokenGenerationRepository : IGET_NotifyFarmerOnTokenGenerationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_NotifyFarmerOnTokenGenerationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_NotifyFarmerOnTokenGeneration>> getasync(GET_NotifyFarmerOnTokenGenerationDomain input)
        {
            List<GET_NotifyFarmerOnTokenGeneration> list = new List<GET_NotifyFarmerOnTokenGeneration>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_NotifyFarmerOnTokenGeneration]('" + input.AADHARNO + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_NotifyFarmerOnTokenGeneration
                        {
                            FAID = reader.GetString("FAID"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FNAME = reader.GetString("FNAME"),
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
}
