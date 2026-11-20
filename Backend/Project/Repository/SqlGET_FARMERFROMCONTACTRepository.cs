using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FARMERFROMCONTACTRepository : IGET_FARMERFROMCONTACTRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FARMERFROMCONTACTRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<List<GET_FARMERFROMCONTACT>> getasync(GET_FARMERFROMCONTACTDomain input)
        {
            List<GET_FARMERFROMCONTACT> list = new List<GET_FARMERFROMCONTACT>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FARMERFROMCONTACT](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.FCONTACTNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FARMERFROMCONTACT
                        {
                            FNAME = reader.GetString("FNAME"),
                            FAID = reader.GetString("FAID"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                          
                        });
                    }
                }
            }
            return list;
        }
    }
   
}
