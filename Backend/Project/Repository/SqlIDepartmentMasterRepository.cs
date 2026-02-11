using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlIDepartmentMasterRepository : IDepartmentMasterRepository
    {

        private readonly ApplicationDbContext dbContext;

        public SqlIDepartmentMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(DepartmentMaster departmentMaster)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdDepartmentMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;


               
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTAID";
                parameter.Value = departmentMaster.DEPTAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@WAID";
                parameter.Value = departmentMaster.WAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@STOREID";
                parameter.Value = departmentMaster.STOREID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DNAME";
                parameter.Value = departmentMaster.DNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DINTERCOMEXTENSION";
                parameter.Value = departmentMaster.DINTERCOMEXTENSION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DCODE";
                parameter.Value = departmentMaster.DCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DADDRESS";
                parameter.Value = departmentMaster.DADDRESS;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@DCITY";
                parameter.Value = departmentMaster.DCITY;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@DAREA";
                parameter.Value = departmentMaster.DAREA;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@DSTATE";
                parameter.Value = departmentMaster.DSTATE;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@DLANDMARK";
                parameter.Value = departmentMaster.DLANDMARK;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@DPINCODE";
                parameter.Value = departmentMaster.DPINCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = departmentMaster.COMPANYID;
                command.Parameters.Add(parameter);

               

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new ApiResponseMessage
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues")
                        });
                    }
                }


            }
            return result;

        }
    }
}
