using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionNumberSearchRepository : IGET_PRequisitionNumberSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PRequisitionNumberSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PRequisitionNumberSearch>> getasync(GET_PRequisitionNosearchDomain gET_PRequisition)
        {
           List<GET_PRequisitionNumberSearch> list = new List<GET_PRequisitionNumberSearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionNumberSearch]('" + gET_PRequisition.PRNO + "' , '" + gET_PRequisition.COMPANYID + "' , '" + gET_PRequisition.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionNumberSearch
                        {
                            CAID = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CLANDMARK = reader.GetString("CLANDMARK"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CIFSC = reader.GetString("CIFSC"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            PRNO = reader.GetString("PRNO"),
                            PRAID = reader.GetString("PRAID"),
                            STATUS = reader.GetInt32("STATUS"),
                            PRVANDORID = reader.GetString("PRVANDORID")

                        });
                    }
                }
            }
            return list;
        }
    }
}
