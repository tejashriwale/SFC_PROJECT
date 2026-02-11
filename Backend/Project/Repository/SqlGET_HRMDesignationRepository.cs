using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRMDesignationRepository : IGET_HRMDesignationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HRMDesignationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_HRMDesignation>> getasync(GET_HRMDesignationDomain input)
        {
            List<GET_HRMDesignation> list = new List<GET_HRMDesignation> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_HRMDesignation]('" + input.DAID + "' , '"+input.KEYWORD+"'  ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_HRMDesignation
                        {
                            DAID = reader.GetString("DAID"),
                            DNAME = reader.GetString("DNAME"),
                            DDATE = reader.GetString("DDATE"),
                            DSTATUS = reader.GetBoolean("DSTATUS"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_HRMDesignation>> getkeyword(GET_HRMDesignationDomain input)
        {
            List<GET_HRMDesignation> list = new List<GET_HRMDesignation>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_HRMDesignation]('" + input.DAID + "' , '" + input.KEYWORD + "'  ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_HRMDesignation
                        {
                            DAID = reader.GetString("DAID"),
                            DNAME = reader.GetString("DNAME"),
                            DDATE = reader.GetString("DDATE"),
                            DSTATUS = reader.GetBoolean("DSTATUS"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
