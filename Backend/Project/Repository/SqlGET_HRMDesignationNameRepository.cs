using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRMDesignationNameRepository : IGET_HRMDesignationNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HRMDesignationNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_HRMDesignationName>> getasync(GET_HRMDesignationNameDomain input)
        {
            List<GET_HRMDesignationName> list = new List<GET_HRMDesignationName> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_HRMDesignationName]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync ())
                {
                    while(await  reader.ReadAsync ())
                    {
                        list.Add(new GET_HRMDesignationName
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
