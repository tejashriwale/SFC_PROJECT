using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_UserLoginesarchRepository : IGET_UserProfileSearchRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_UserLoginesarchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_UserProfileSearchDomain>> GetAll(GET_UserProfileSearch input)
        {
            List<GET_UserProfileSearchDomain> list = new List<GET_UserProfileSearchDomain>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_UserMastersSearch]('" + input.EMAIL +"' , '"+ input.PASSWORD +"')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_UserProfileSearchDomain
                        {
                            Username = reader.GetString("Username"),
                            EMAIL = reader.GetString("EMAIL"),
                            Password = reader.GetString("Password"),
                            AccessPolicy = reader.GetString("AccessPolicy"),
                            companyID = reader.GetString("companyID"),
                            departmentID = reader.GetString("departmentID"),
                            UAID = reader.GetString("UAID"),
                            DEPARTMENTNAME = reader.GetString("DEPARTMENTNAME"),
                            COMPANYNAME = reader.GetString("COMPANYNAME"),
                            COMPANYSTATE = reader.GetString("COMPANYSTATE"),
                            REGISTRATIONDATE=reader.GetString("REGISTRATIONDATE"),
                            Message = reader.GetString("Message"),
                            UROLEID = reader.GetString("UROLEID"),
                            LANGUAGE = reader.GetString("LANGUAGE"),
                            Status = reader.GetInt32("Status"),
                        });
                    }
                }

            }
            return list;
            
        }
    }
}
