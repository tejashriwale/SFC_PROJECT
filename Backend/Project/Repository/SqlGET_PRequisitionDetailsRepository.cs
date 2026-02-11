using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionDetailsRepository : IGET_PRequisitionDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_PRequisitionDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PRequisitionDetails>> getasync(RepuisitonDomain input)
        {
           List<GET_PRequisitionDetails> list = new List<GET_PRequisitionDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionDetails]('" + input.PRAID + "','" + input.COMPANYID + "','" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionDetails
                        {
                            PRDAID = reader.GetString("PRDAID"),
                            PRAID = reader.GetString("PRAID"),
                            PRDPRODUCT = reader.GetString("PRDPRODUCT"),
                            PRDDESCRIPTION = reader.GetString("PRDDESCRIPTION"),
                            PRDQUANTITY = reader.GetString("PRDQUANTITY"),
                            PRDUOM = reader.GetString("PRDUOM"),
                            PNAME = reader.GetString("PNAME"),
                            PIGST = reader.GetDouble("PIGST"),
                            PSGST = reader.GetDouble("PSGST"),
                            PCGST = reader.GetDouble("PCGST"),
                            PCESS = reader.GetDouble("PCESS"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            RATE = reader.GetDouble("RATE"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            PRBASEQTY = reader.GetInt32("PRBASEQTY"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
