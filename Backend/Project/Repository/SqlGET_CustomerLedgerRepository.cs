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
                command.CommandText = "SELECT  * FROM [GET_CustomerLedger]('" + input.COMPANYID + "' ,  '" + input.DEPTID + "'   ,'" + input.REFAID + "','" + input.MAID + "','" + input.SDATE + "','" + input.EDATE + "')";
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
