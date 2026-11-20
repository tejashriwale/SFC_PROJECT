using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;
using System.Data;
using System.Data.Common;

namespace Project.Repository
{
    public class SqlGET_CustomerLedgerRepository : IGET_CustomerLedgerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CustomerLedgerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private bool HasColumn(DbDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private string SafeGetString(DbDataReader reader, string col, bool exists)
        {
            if (!exists) return "";
            var ord = reader.GetOrdinal(col);
            return reader.IsDBNull(ord) ? "" : reader.GetString(ord);
        }

        private double SafeGetDouble(DbDataReader reader, string col, bool exists)
        {
            if (!exists) return 0;
            var ord = reader.GetOrdinal(col);
            return reader.IsDBNull(ord) ? 0 : reader.GetDouble(ord);
        }

        public async Task<List<GET_CustomerLedger>> getasync(GET_CustomerLedgerDomain input)
        {
            List<GET_CustomerLedger> list = new List<GET_CustomerLedger>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CustomerLedger](@p0 ,  @p1   ,@p2,@p3,@p4,@p5)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.REFAID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.SDATE) ?? ""); command.Parameters.Add(command_p4); }
                { var command_p5 = command.CreateParameter(); command_p5.ParameterName = "@p5"; command_p5.Value = (object)(Convert.ToString(input.EDATE) ?? ""); command.Parameters.Add(command_p5); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var hasCropname = HasColumn(reader, "CROPNAME");
                    var hasWtpersku = HasColumn(reader, "WTPERSKU");
                    var hasQtycrt = HasColumn(reader, "QTYCRT");
                    var hasQtykg = HasColumn(reader, "QTYKG");
                    var hasRate = HasColumn(reader, "RATE");
                    var hasReleasedate = HasColumn(reader, "RELEASEDATE");
                    var hasPcharges = HasColumn(reader, "PCHARGES");

                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerLedger
                        {
                           MAID = reader.GetString("MAID"),
                           CBAL = reader.GetDouble("CBAL"),
                           REFAID = reader.GetString("REFAID"),
                           TOTSAMT = reader.GetDouble("TOTSAMT"),
                           PAMT = reader.GetDouble("PAMT"),
                           TRAID = reader.GetString("TRAID"),
                           TRNDT = reader.GetString("TRNDT"),
                           CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                           VEHICLENO = reader.IsDBNull(reader.GetOrdinal("VEHICLENO")) ? "" : reader.GetString("VEHICLENO"),
                           ADVAMT = reader.IsDBNull(reader.GetOrdinal("ADVAMT")) ? 0 : reader.GetDouble("ADVAMT"),
                           CROPNAME = SafeGetString(reader, "CROPNAME", hasCropname),
                           WTPERSKU = SafeGetDouble(reader, "WTPERSKU", hasWtpersku),
                           QTYCRT = SafeGetDouble(reader, "QTYCRT", hasQtycrt),
                           QTYKG = SafeGetDouble(reader, "QTYKG", hasQtykg),
                           RATE = SafeGetDouble(reader, "RATE", hasRate),
                           RELEASEDATE = SafeGetString(reader, "RELEASEDATE", hasReleasedate),
                           PCHARGES = SafeGetDouble(reader, "PCHARGES", hasPcharges),
                        });
                    }
                }
            }
            return list;
        }
    }
}
