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
                command.CommandText = "SELECT Top 20 * FROM [GET_RokdaVyapari](@p0 , @p1 , @p2 , @p3)order by CONVERT(date, TRNDT, 106) asc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.UAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
                command.CommandText = "SELECT * FROM [GET_RokdaVyapari](@p0 , @p1 , @p2 , @p3)order by CONVERT(date, TRNDT, 106) asc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.UAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
