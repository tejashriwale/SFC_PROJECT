using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_SRequisitionDetailByIdRepository : IGET_SRequisitionDetailByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SRequisitionDetailByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SRequisitionDetailById>> getaqsync(GET_SRequisitionDetailByIdDomain input)
        {
            List<GET_SRequisitionDetailById> list = new List<GET_SRequisitionDetailById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM[GET_SRequisitionDetailById]('" + input.SRAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SRequisitionDetailById
                        {
                            SRDAID = reader.GetString("SRDAID"),
                            SRAID = reader.GetString("SRAID"),
                            SRDPRODUCT = reader.GetString("SRDPRODUCT"),
                            SRDDESCRIPTION = reader.GetString("SRDDESCRIPTION"),
                            SRDQUANTITY = reader.GetString("SRDQUANTITY"),
                            SRDUOM = reader.GetString("SRDUOM"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
