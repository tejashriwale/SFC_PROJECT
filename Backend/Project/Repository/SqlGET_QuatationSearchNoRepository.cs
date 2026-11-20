using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_QuatationSearchNoRepository : IGET_QuatationSearchNoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_QuatationSearchNoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_QuatationSearchNo>> getasync(GET_QuatationSearchNoDomain input)
        {
            List<GET_QuatationSearchNo> list = new List<GET_QuatationSearchNo>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_QuatationSearchNo](@p0,@p1, @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_QuatationSearchNo
                        {
                            QAMAID = reader.GetString("QAMAID"),
                            QVAID = reader.GetString("QVAID"),
                            QVCONTACT = reader.GetString("QVCONTACT"),
                            QVEMAIL = reader.GetString("QVEMAIL"),
                            QVADDRESS = reader.GetString("QVADDRESS"),
                            PRNO = reader.GetString("PRNO"),
                            QMNAMT = reader.GetDouble("QMNAMT"),
                            QGAMT = reader.GetDouble("QGAMT"),
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            QDUEDATE = reader.GetString("QDUEDATE"),
                            QTRANSPORTTERMS = reader.GetString("QTRANSPORTTERMS"),
                            QPAYMENTTERMS = reader.GetString("QPAYMENTTERMS"),
                            ISAPPROVED = reader.GetBoolean("ISAPPROVED"),
                            QPDUEDATE = reader.GetString("QPDUEDATE"),
                            QNODAYS = reader.GetInt32("QNODAYS"),
                            VENDORNAME = reader.GetString("VENDORNAME"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
