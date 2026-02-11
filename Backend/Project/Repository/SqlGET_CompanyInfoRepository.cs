using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CompanyInfoRepository : IGET_CompanyInfoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CompanyInfoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CompanyInfo>> getasync(GET_CompanyInfoDomain input)
        {
            List<GET_CompanyInfo> list = new List<GET_CompanyInfo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_CompanyInfo]('" + input.CINFOID + "' , '" + input.KEYWORD + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CompanyInfo
                        {
                            CINFOID		 = reader.GetString("CINFOID"),
                            COMPANYID	 = reader.GetString("COMPANYID"),
                            CNAME		 = reader.GetString("CNAME"),
                            CTELEPHONE	 = reader.GetString("CTELEPHONE"),
                            CEMAIL		 = reader.GetString("CEMAIL"),
                            CCONTACT	 = reader.GetString("CCONTACT"),
                            CPAN		 = reader.GetString("CPAN"),
                            CGSTNO		 = reader.GetString("CGSTNO"),
                           
                            CADDRESS	 = reader.GetString("CADDRESS"),
                           
                            INSTAGRAMID	 = reader.GetString("INSTAGRAMID"),
                            TWITTERID	 = reader.GetString("TWITTERID"),
                            FACEBOOKID = reader.GetString("FACEBOOKID"),
                            

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CompanyInfo>> getkeyword(GET_CompanyInfoDomain input)
        {
            List<GET_CompanyInfo> list = new List<GET_CompanyInfo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CompanyInfo]('" + input.CINFOID + "' , '" + input.KEYWORD + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CompanyInfo
                        {
                            CINFOID = reader.GetString("CINFOID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            CNAME = reader.GetString("CNAME"),
                            CTELEPHONE = reader.GetString("CTELEPHONE"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CCONTACT = reader.GetString("CCONTACT"),
                            CPAN = reader.GetString("CPAN"),
                            CGSTNO = reader.GetString("CGSTNO"),
                           
                            CADDRESS = reader.GetString("CADDRESS"),
                           
                            INSTAGRAMID = reader.GetString("INSTAGRAMID"),
                            TWITTERID = reader.GetString("TWITTERID"),
                            FACEBOOKID = reader.GetString("FACEBOOKID"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
