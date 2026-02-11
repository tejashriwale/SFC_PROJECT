using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlIGET_SRequisitionDetailsRepository : IGET_SRequisitionDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlIGET_SRequisitionDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SRequisitionDetails>> getasync(GET_SRequisitionDetailsDomain input)
        {
           List<GET_SRequisitionDetails> list = new List<GET_SRequisitionDetails> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_SRequisitionDetails]('" + input.SRDAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync ())
                {
                    while(await reader.ReadAsync ())
                    {
                        list.Add(new GET_SRequisitionDetails
                        {
                            SRAID = reader.GetString("SRAID"),
                            SRDAID = reader.GetString("SRDAID"),
                            SRDPRODUCT = reader.GetString("SRDPRODUCT"),
                            SRDDESCRIPTION = reader.GetString("SRDDESCRIPTION"),
                            SRDQUANTITY = reader.GetString("SRDQUANTITY"),
                            SRDUOM = reader.GetString("SRDUOM"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
