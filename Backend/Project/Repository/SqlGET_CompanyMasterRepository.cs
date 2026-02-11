using System.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CompanyMasterRepository : IGET_CompanyMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CompanyMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CompanyMaster>> getasync(GET_CompanyMasterDomain input)
        {
            List<GET_CompanyMaster> list = new List<GET_CompanyMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CompanyMaster]('" + input.CPAID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_CompanyMaster
                        {
                            CPAID = reader.GetString("CPAID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            CNAME = reader.GetString("CNAME"),
                            CCONTACT = reader.GetString("CCONTACT"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CADDRESS = reader.GetString("CADDRESS"),
                            CPINCODE = reader.GetString("CPINCODE"),
                            CREGNO = reader.GetString("CREGNO"),
                            CREGDATE = reader.GetString("CREGDATE"),
                            CPAN = reader.GetString("CPAN"),
                            CGSTNO = reader.GetString("CGSTNO"),
                            CSTATUS = reader.GetString("CSTATUS"),
                            CIFSC = reader.GetString("CIFSC"),
                            CACCOUNTNAME = reader.GetString("CACCOUNTNAME"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            CTELEPHONE = reader.GetString("CTELEPHONE"),
                            CAREA = reader.GetString("CAREA"),
                            CLANDMARK = reader.GetString("CLANDMARK"),
                            CCITY = reader.GetString("CCITY"),
                            CSTATE = reader.GetString("CSTATE"),
                            OPENINGDT = reader.GetString("OPENINGDT"),
                            CTYPE = reader.GetString("CTYPE"),
                            INSTAGRAMID = reader.GetString("INSTAGRAMID"),
                            TWITTERID = reader.GetString("TWITTERID"),
                            FACEBOOKID = reader.GetString("FACEBOOKID"),
                            CITY  = reader.GetString("CITY"),
                            STATE = reader.GetString("STATE"),
                           



                        });
                    }
                }
            }
            return list;
        }
    }
}
