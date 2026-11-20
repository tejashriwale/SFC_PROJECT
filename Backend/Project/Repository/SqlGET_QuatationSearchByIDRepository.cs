using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_QuatationSearchByIDRepository : IGET_QuatationSearchByIDRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_QuatationSearchByIDRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_QuatationSearchByID>> getasync(GET_QuatationSearchByIDDomain input)
        {
           List<GET_QuatationSearchByID> list = new List<GET_QuatationSearchByID>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_QuatationSearchByID](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.QAMAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_QuatationSearchByID
                        {
                            QAMDAID = reader.GetString("QAMDAID"),
                            QAMAID = reader.GetString("QAMAID"),
                            QPRODUCT = reader.GetString("QPRODUCT"),
                            QQUANTITY = reader.GetString("QQUANTITY"),
                            QUOM = reader.GetString("QUOM"),
                            QRATE = reader.GetDouble("QRATE"),
                            QTAMT = reader.GetDouble("QTAMT"),
                            QIGST = reader.GetDouble("QIGST"),
                            QSGST = reader.GetDouble("QSGST"),
                            QCGST = reader.GetDouble("QCGST"),
                            QCESS = reader.GetDouble("QCESS"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                        });
                    }
                }
            }    
            return list;
        }
    }
}
