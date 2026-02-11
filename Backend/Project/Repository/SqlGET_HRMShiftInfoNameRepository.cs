using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRMShiftInfoNameRepository : IGET_HRMShiftInfoNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HRMShiftInfoNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_HRMShiftInfoName>> getasync(GET_HRMShiftInfoNameDomain input)
        {
            List<GET_HRMShiftInfoName> list = new List<GET_HRMShiftInfoName> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_HRMShiftInfoName]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync ())
                {
                    while(await  reader.ReadAsync ())
                    {
                        list.Add(new GET_HRMShiftInfoName
                        {
                            SHID = reader.GetString("SHID"),
                            SHNAME = reader.GetString("SHNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
