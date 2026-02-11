using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PQuatationNoCheckRepository : IGET_PQuatationNoCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PQuatationNoCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PQuatationNoCheck>> getasync(GET_PQuatationNoCheckDomain input)
        {
            List<GET_PQuatationNoCheck> list = new List<GET_PQuatationNoCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PQuatationNoCheck]('" + input.QAMAID + "' ,'" + input.QNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PQuatationNoCheck
                        {
                            QNO = reader.GetString("QNO"),
                            POAID = reader.GetString("POAID")


                        });
                    }
                }
            }
            return list;
        }
    }
}
