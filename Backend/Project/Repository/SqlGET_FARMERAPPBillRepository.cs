using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FARMERAPPBillRepository : IGET_FARMERAPPBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FARMERAPPBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FARMERAPPBill>> getasync(GET_FARMERAPPBillDomain input)
        {
            List<GET_FARMERAPPBill> list = new List<GET_FARMERAPPBill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FARMERAPPBill](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.FAID) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FARMERAPPBill
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            BILLNO	 = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            FNAME = reader.GetString("FNAME"),
                           
                            
                        });

                    }
                }
            }
            return list;
        }
    }
}
