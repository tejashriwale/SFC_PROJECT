using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariLoginRepository : IGET_VyapariLoginRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariLoginRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariLogin>> getasync(GET_VyapariLoginDomain input)
        {
            List<GET_VyapariLogin> result = new List<GET_VyapariLogin>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_VyapariLogin]('" + input.VPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariLogin
                        {
                            VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VEMAIL = reader.GetString("VEMAIL"),
                            VAADHAR = reader.GetString("VAADHAR"),
                            VBIRTHDAYDATE = reader.GetString("VBIRTHDAYDATE"),
                            UAID = reader.GetString("UAID"),
                          
                            
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_VyapariLogin>> getkeyword(GET_VyapariLoginDomain input)
        {
            List<GET_VyapariLogin> result = new List<GET_VyapariLogin>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_VyapariLogin]('" + input.VPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariLogin
                        {
                            VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VEMAIL = reader.GetString("VEMAIL"),
                            VAADHAR = reader.GetString("VAADHAR"),
                            VBIRTHDAYDATE = reader.GetString("VBIRTHDAYDATE"),
                            UAID = reader.GetString("UAID"),


                        });
                    }
                }
            }
            return result;
        }
    }
}
