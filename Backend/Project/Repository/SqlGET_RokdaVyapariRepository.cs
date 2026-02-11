using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RokdaVyapariRepository : IGET_RokdaVyapariRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RokdaVyapariRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RokdaVyapari>> getasync(GET_RokdaVyapariDomain input)
        {
            List<GET_RokdaVyapari> result = new List<GET_RokdaVyapari>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_RokdaVyapari]('" + input.UAID + "' , '"+input.KEYWORD+"' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')order by CONVERT(date, TRNDT, 106) asc";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_RokdaVyapari
                        {
                            VDAID = reader.GetString("VDAID"),
                            TRNDT = reader.GetString("TRNDT"),
                            VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                            TID	 = reader.GetString("TID"),
                            UAID = reader.GetString("UAID"),
                            REFF = reader.GetString("REFF"),
                            ISCLOSE = reader.GetBoolean("ISCLOSE"),
                            STID = reader.GetString("STID"),
                            BPAMT = reader.GetDouble("BPAMT"),
                            REFKEY = reader.GetString("REFKEY")



                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_RokdaVyapari>> getkeyword(GET_RokdaVyapariDomain input)
        {
            List<GET_RokdaVyapari> result = new List<GET_RokdaVyapari>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RokdaVyapari]('" + input.UAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')order by CONVERT(date, TRNDT, 106) asc";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_RokdaVyapari
                        {
                            VDAID = reader.GetString("VDAID"),
                            TRNDT = reader.GetString("TRNDT"),
                            VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                            TID = reader.GetString("TID"),
                            UAID = reader.GetString("UAID"),
                            REFF = reader.GetString("REFF"),
                            ISCLOSE = reader.GetBoolean("ISCLOSE"),
                            STID = reader.GetString("STID"),
                            BPAMT = reader.GetDouble("BPAMT"),
                            REFKEY = reader.GetString("REFKEY")



                        });
                    }
                }
            }
            return result;
        }
    }
}
