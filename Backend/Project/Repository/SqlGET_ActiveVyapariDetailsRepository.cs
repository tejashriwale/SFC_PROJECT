using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ActiveVyapariDetailsRepository : IGET_ActiveVyapariDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ActiveVyapariDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ActiveVyapariDetails>> getasync(GET_ActiveVyapariDetailsDomain input)
        {
            List<GET_ActiveVyapariDetails> list = new List<GET_ActiveVyapariDetails>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ActiveVyapariDetails]('" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ActiveVyapariDetails
                        {
                            VNAME = reader.GetString("VNAME"),
                            CONTACT = reader.GetString("CONTACT"),
                            AADHAR = reader.GetString("AADHAR"),
                            PAN = reader.GetString("PAN"),
                            GSTIN = reader.GetString("GSTIN"),
                            COMPANY_NAME = reader.GetString("COMPANY_NAME"),
                            ACCOUNT_NO = reader.GetString("ACCOUNT_NO"),
                            BANK_NAME = reader.GetString("BANK_NAME"),
                            IFSC = reader.GetString("IFSC"),
                         

                        });
                    }
                }
            }
            return list;
        }
    }
}
