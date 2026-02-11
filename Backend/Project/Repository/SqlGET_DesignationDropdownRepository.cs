using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_DesignationDropdownRepository : IGET_DesignationDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DesignationDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_DesignationDropdown>> getasync(GET_DesignationDropdownDomain input)
        {
            List<GET_DesignationDropdown> list = new List<GET_DesignationDropdown>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_DesignationDropdown]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_DesignationDropdown
                        {
                            DAID = reader.GetString("DAID"),
                            DNAME = reader.GetString("DNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
