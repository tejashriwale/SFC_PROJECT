using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateCashInvertRepository : IGET_GateCashInvertRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateCashInvertRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateCashInvert>> getasync(GET_GateCashInvertDomain input)
        {
            List<GET_GateCashInvert> list = new List<GET_GateCashInvert> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateCashInvert](@p0 , @p1 , @p2 , @p3)ORDER BY CASDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CASID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_GateCashInvert
                        {
                            CASID = reader.GetString("CASID"),
                            CASDATE = reader.GetString("CASDATE"),
                            CASBANKNAME = reader.GetString("CASBANKNAME"),
                            CASINVERTCASH = reader.GetInt32("CASINVERTCASH"),
                            CAS2000 = reader.GetInt32("CAS2000"),
                            CAS500 = reader.GetInt32("CAS500"),
                            CAS200 = reader.GetInt32("CAS200"),
                            CAS100 = reader.GetInt32("CAS100"),
                            CAS10 = reader.GetInt32("CAS10"),
                            CAS50 = reader.GetInt32("CAS50"),
                            CAS20 = reader.GetInt32("CAS20"),
                            CASCOINS = reader.GetInt32("CASCOINS"),
                            CASTOTAL = reader.GetInt32("CASTOTAL"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                           


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_GateCashInvert>> getkeyword(GET_GateCashInvertDomain input)
        {
            List<GET_GateCashInvert> list = new List<GET_GateCashInvert>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_GateCashInvert](@p0 , @p1 , @p2 , @p3)ORDER BY CASDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CASID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GateCashInvert
                        {
                            CASID = reader.GetString("CASID"),
                            CASDATE = reader.GetString("CASDATE"),
                            CASBANKNAME = reader.GetString("CASBANKNAME"),
                            CASINVERTCASH = reader.GetInt32("CASINVERTCASH"),
                            CAS2000 = reader.GetInt32("CAS2000"),
                            CAS500 = reader.GetInt32("CAS500"),
                            CAS200 = reader.GetInt32("CAS200"),
                            CAS100 = reader.GetInt32("CAS100"),
                            CAS10 = reader.GetInt32("CAS10"),
                            CAS50 = reader.GetInt32("CAS50"),
                            CAS20 = reader.GetInt32("CAS20"),
                            CASCOINS = reader.GetInt32("CASCOINS"),
                            CASTOTAL = reader.GetInt32("CASTOTAL"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                           


                        });
                    }
                }
            }
            return list;
        }
    }
}
