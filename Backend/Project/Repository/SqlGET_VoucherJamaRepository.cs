using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VoucherJamaRepository : IGET_VoucherJamaRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VoucherJamaRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VoucherJama>> getasync(GET_VoucherJamaDomain input)
        {
            List<GET_VoucherJama> list = new List<GET_VoucherJama>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VoucherJama]('" + input.VoucherAID + "' ,  '" + input.OrganizationID + "' , '" + input.DivisionID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VoucherJama
                        {
                            VoucherAID = reader.GetInt32("VoucherAID"),
                            OrganizationID = reader.GetString("OrganizationID"),
                            DivisionID = reader.GetString("DivisionID"),
                            VoucherTID = reader.GetString("VoucherTID"),
                            SerialNumber = reader.GetString("SerialNumber"),
                            VoucherDate = reader.GetString("VoucherDate"),
                            VoucherAmount = reader.GetDouble("VoucherAmount"),
                            DRACC = reader.GetString("DRACC"),
                            CRACC = reader.GetString("CRACC"),
                            ReferenceTID = reader.GetString("ReferenceTID"),
                            ReferenceKey = reader.GetString("ReferenceKey"),
                            Narration = reader.GetString("Narration"),
                            GRPKEY = reader.GetString("GRPKEY"),
                            BATCHKEY = reader.GetString("BATCHKEY"),
                            OID = reader.GetString("OID"),
                            VOUCHERTYPE = reader.GetString("VOUCHERTYPE"),
                            JAMANAVE = reader.GetString("JAMANAVE"),
                            FNAME = reader.GetString("FNAME"),
                            JAMANAVENAME = reader.GetString("JAMANAVENAME"),
                            DRACCENAME = reader.GetString("DRACCENAME"),
                            VoucherNumber = reader.GetString("VoucherNumber"),
                            STATUS = reader.GetBoolean("STATUS"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
