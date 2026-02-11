using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VoucherRepository : IGET_VoucherRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VoucherRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Voucher>> getasync(GET_VoucherDomain input)
        {
            List<GET_Voucher> list = new List<GET_Voucher>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Voucher]('" + input.VoucherAID + "' , '"+input.KEYWORD + "'  ,'" + input.OrganizationID + "' , '" + input.DivisionID + "')ORDER BY VoucherDate DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Voucher
                        {
                            VoucherAID = reader.GetInt32("VoucherAID"),
                            OrganizationID = reader.GetString("OrganizationID"),
                            DivisionID = reader.GetString("DivisionID"),
                            VoucherTID = reader.GetString("VoucherTID"),
                            SerialNumber = reader.GetString("SerialNumber"),
                            VoucherDate = reader.GetString("VoucherDate"),
                            VoucherAmount = reader.GetDouble("VoucherAmount"),
                            DRACC= reader.GetString("DRACC"),
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

        public async Task<List<GET_Voucher>> getkeyword(GET_VoucherDomain input)
        {
            List<GET_Voucher> list = new List<GET_Voucher>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Voucher]('" + input.VoucherAID + "' , '"+input.KEYWORD + "'  , '" + input.OrganizationID + "' , '" + input.DivisionID + "')ORDER BY VoucherDate DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Voucher
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
