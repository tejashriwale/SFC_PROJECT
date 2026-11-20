using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerFromFamilyDetailsRepository : IGET_FarmerFromFamilyDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerFromFamilyDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerFromFamilyDetails>> getasync(GET_FarmerFromFamilyDetailsDomain input)
        {
            List<GET_FarmerFromFamilyDetails> result = new List<GET_FarmerFromFamilyDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerFromFamilyDetails](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_FarmerFromFamilyDetails
                        {
                            FAID = reader.GetString("FAID"),
                            FDAADHARNO = reader.GetString("FDAADHARNO"),
                            FDBIRTHDATE = reader.GetString("FDBIRTHDATE"),
                            FDCONTACT = reader.GetString("FDCONTACT"),
                            FDNAME = reader.GetString("FDNAME"),
                            FDRELATION = reader.GetString("FDRELATION"),
                            FDAID = reader.GetString("FDAID"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME")

                        });
                    }
                }
            }
            return result;
        }
    }
}
