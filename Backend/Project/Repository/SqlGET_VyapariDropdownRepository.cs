using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariDropdownRepository : IGET_VyapariDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariDropdown>> getasync(GET_VyapariDropdownDomain input)
        {
            List<GET_VyapariDropdown> list = new List<GET_VyapariDropdown>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariDropdown]( '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.VPAID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariDropdown
                        {
                           VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                           
                            RATE = reader.GetDouble("RATE"),
                            VYAPARI = reader.GetString("VYAPARI")
                        });
                    }
                }
            }
            return list;
        }
    }
}
