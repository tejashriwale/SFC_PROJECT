using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CustomerTranDataRepository : IGET_CustomerTranDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CustomerTranDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CustomerTranData>> getasync(GET_CustomerTranDataDomain input)
        {
            List<GET_CustomerTranData> list = new List<GET_CustomerTranData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CustomerTranData]( @p0 , @p1 , @p2 , @p3)ORDER BY TRNSR ";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.REFAID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerTranData
                        {
                            STATUS = reader.GetBoolean("STATUS"),
                            TRNSID = reader.GetString("TRNSID"),
                            ADVAMT = reader.GetDouble("ADVAMT"),
                            MAID = reader.GetString("MAID"),
                            REFAID = reader.GetString("REFAID"),
                            CBAL = reader.GetDouble("CBAL"),
                            NARR = reader.GetString("NARR"),
                            PAMT = reader.GetDouble("PAMT"),
                            TRNDT = reader.GetString("TRNDT"),
                            TRAID = reader.GetString("TRAID"),
                            TRNSR = reader.GetInt32("TRNSR"),
                            CROPID = reader.GetString("CROPID")


                        });
                    }
                }
            }
            return list;
        }
    }
}
