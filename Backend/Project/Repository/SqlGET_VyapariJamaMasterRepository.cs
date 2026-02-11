using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariJamaMasterRepository : IGET_VyapariJamaMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariJamaMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariJamaMaster>> getasync(GET_VyapariJamaMasterDomain input)
        {
            List<GET_VyapariJamaMaster> list = new List<GET_VyapariJamaMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariJamaMaster]('" + input.VJAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariJamaMaster
                        {
                            VJAID = reader.GetString("VJAID"),
                            VYAPARI = reader.GetString("VYAPARI"),
                            TRANSACTIONMODE = reader.GetString("TRANSACTIONMODE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            COMPANYNAME = reader.GetString("COMPANYNAME"),
                            GSTNNUMBER = reader.GetString("GSTNNUMBER"),
                            TOTALRECEIVINGAMOUNT = reader.GetDouble("TOTALRECEIVINGAMOUNT"),
                            PAIDAMOUNT = reader.GetDouble("PAIDAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            DATE = reader.GetString("DATE"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_VyapariJamaMaster>> getkeyword(GET_VyapariJamaMasterDomain input)
        {
            List<GET_VyapariJamaMaster> list = new List<GET_VyapariJamaMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariJamaMaster]('" + input.VJAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariJamaMaster
                        {
                            VJAID = reader.GetString("VJAID"),
                            VYAPARI = reader.GetString("VYAPARI"),
                            TRANSACTIONMODE = reader.GetString("TRANSACTIONMODE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            COMPANYNAME = reader.GetString("COMPANYNAME"),
                            GSTNNUMBER = reader.GetString("GSTNNUMBER"),
                            TOTALRECEIVINGAMOUNT = reader.GetDouble("TOTALRECEIVINGAMOUNT"),
                            PAIDAMOUNT = reader.GetDouble("PAIDAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            DATE = reader.GetString("DATE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
