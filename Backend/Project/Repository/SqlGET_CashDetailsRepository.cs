using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CashDetailsRepository : IGET_CashDetailsRepository
    {
       private readonly ApplicationDbContext dbContext;
        public SqlGET_CashDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CashDetails>> getasync(GET_CashDetailsDomain input)
        {
            List<GET_CashDetails> list = new List<GET_CashDetails>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECt  * FROM [GET_CashDetails]('" + input.Amount + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CashDetails
                        {
                            Note = reader.GetInt32("Note"),
                            NoteCount = reader.GetInt32("NoteCount"),
                            Total = reader.GetInt32("Total"),
                            RemainingAmountMSG = reader.GetString("RemainingAmountMSG"),
                           

                        });
                    }

                }
            }
            return list;
        }
    }
}
