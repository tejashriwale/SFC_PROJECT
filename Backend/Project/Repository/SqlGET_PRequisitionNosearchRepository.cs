using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionNosearchRepository : IGET_PRequisitionNosearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PRequisitionNosearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PRequisitionNosearch>> getasync(GET_PRequisitionNosearchDomain gET_PRequisition)
        {
           List<GET_PRequisitionNosearch> list = new List<GET_PRequisitionNosearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionNosearch]('"+ gET_PRequisition.PRNO+"' , '"+ gET_PRequisition.COMPANYID+"' , '"+ gET_PRequisition.DEPTID+"')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionNosearch
                        {
                            PRAID = reader.GetString("PRAID"),
                            PRNO = reader.GetString("PRNO"),
                            PRQDATE = reader.GetString("PRQDATE"),
                            PRDAID = reader.GetString("PRDAID"),
                            PRDDESCRIPTION = reader.GetString("PRDDESCRIPTION"),
                            PRDPRODUCT = reader.GetString("PRDPRODUCT"),
                            PRDQUANTITY = reader.GetString("PRDQUANTITY"),
                            PRDUOM = reader.GetString("PRDUOM"),
                           
                        });
                    }
                }

            }
            return list;
        }
    }
}
