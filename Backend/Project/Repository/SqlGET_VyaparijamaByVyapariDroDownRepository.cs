using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyaparijamaByVyapariDroDownRepository : IGET_VyaparijamaByVyapariDroDownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyaparijamaByVyapariDroDownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyaparijamaByVyapariDroDown>> getasync(GET_VyaparijamaByVyapariDroDownDomain input)
        {
            List<GET_VyaparijamaByVyapariDroDown> list = new List<GET_VyaparijamaByVyapariDroDown>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyaparijamaByVyapariDroDown]('" + input.COMPANYID + "' , '" + input.DEPTID + "'  ,'" + input.VSHORTNAME + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyaparijamaByVyapariDroDown
                        {
                            VPAID = reader.GetString("VPAID"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
                            VNAME = reader.GetString("VNAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VCOMPNAME = reader.GetString("VCOMPNAME"),
                            VGSTIN = reader.GetString("VGSTIN"),
                            TRNDT = reader.GetString("TRNDT"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
