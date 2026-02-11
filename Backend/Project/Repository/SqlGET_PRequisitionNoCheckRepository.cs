using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PRequisitionNoCheckRepository : IGET_PRequisitionNoCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PRequisitionNoCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PRequisitionNoCheck>> getasync(GET_PRequisitionNoCheckDomain input)
        {
            List<GET_PRequisitionNoCheck> list = new List<GET_PRequisitionNoCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionNoCheck]('" + input.PRAID + "' , '" + input.PREQNO + "' , '" + input.PRVANDORID + "' , '" + input.COMPANYID + "', '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionNoCheck
                        {

                            QAMAID = reader.GetString("QAMAID"),
                            QVAID = reader.GetString("QVAID"),
                            PRNO = reader.GetString("PRNO"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
