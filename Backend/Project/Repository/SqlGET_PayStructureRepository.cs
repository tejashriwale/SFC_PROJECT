using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PayStructureRepository : IGET_PayStructureRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PayStructureRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PayStructure>> getasync(GET_PayStructureDomain input)
        {
            List<GET_PayStructure> list = new List<GET_PayStructure>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PayStructure]('" + input.PAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PayStructure
                        {
                            PAID = reader.GetString("PAID"),
                            PNAME = reader.GetString("PNAME"),
                            PELEMENT = reader.GetString("PELEMENT"),
                            PTYPE = reader.GetString("PTYPE"),
                            PTPVALUE = reader.GetString("PTPVALUE"),
                            PSTATUS = reader.GetString("PSTATUS"),
                            PDESCRIPTION = reader.GetString("PDESCRIPTION"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
