using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CashDetailsNearbyCashRepository : IGET_CashDetailsNearbyCashRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CashDetailsNearbyCashRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CashDetailsNearbyCash>> getasync(GET_CashDetailsNearbyCashDomain input)
        {
            List<GET_CashDetailsNearbyCash> list = new List<GET_CashDetailsNearbyCash>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CashDetailsNearbyCash](@p0 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.Amount) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CashDetailsNearbyCash
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
