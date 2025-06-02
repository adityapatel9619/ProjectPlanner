using ProjectPlanner.IMethod;
using ProjectPlanner.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using ProjectPlanner.Common;

namespace ProjectPlanner.Repository
{
    public class AccountRepository:IAccount
    {
        private readonly string _connectionString;

        public AccountRepository(ConnectionStringProvider connectionString)
        {
            _connectionString = connectionString.GetConnection;
        }


        public List<RegistrationModel> GetRegistrations()
        {
            try
            {
                List<RegistrationModel> registrationList = new List<RegistrationModel>();
                clsGlobal objGlobal = new clsGlobal();
                RegistrationModel model = new RegistrationModel();
                //model.Password = objGlobal.Encrypt(regs.Password.ToString().Trim());
                //model.Email = regs.UserNameOrEmail.ToString().Trim();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand search = new SqlCommand("PP_Register_SEARCH", conn);
                    search.CommandType = CommandType.StoredProcedure;
                    //search.Parameters.Add("@sEmailId", SqlDbType.VarChar, 200).Value = model.Email;
                    //search.Parameters.Add("@sPassword", SqlDbType.VarChar, 200).Value = model.Password;

                    

                    conn.Open();
                    SqlDataReader reader = search.ExecuteReader();

                    while (reader.Read())
                    {
                        RegistrationModel registration = new RegistrationModel
                        {
                            FirstName = reader["sFirstName"].ToString(),
                            LastName = reader["sLastName"].ToString(),
                            Email = reader["sEmailId"].ToString(),
                            UserName = reader["sUserName"].ToString(),
                            Password = reader["sPassword"].ToString()
                        };

                        registrationList.Add(registration);
                    }
                    reader.Close();

                    //Add Mail Block Also

                    conn.Close();

                    return registrationList.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Save new entry
        /// </summary>
        /// <param name="regs"></param>
        /// <returns></returns>
        public int SaveNewEntry(RegistrationViewModel regs)
        {
            try
            {
                clsGlobal objGlobal = new clsGlobal();
                RegistrationModel model = new RegistrationModel();
                model.FirstName = regs.FirstName.ToString().Trim();
                model.LastName = regs.LastName.ToString().Trim();
                model.UserName = regs.UserName.ToString().Trim();
                model.Password = objGlobal.Encrypt(regs.Password.ToString().Trim());
                model.Email = regs.Email.ToString().Trim();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand insert = new SqlCommand("PP_Register_INSERT",conn);
                    insert.CommandType = CommandType.StoredProcedure;
                    insert.Parameters.Add("@sFirstName", SqlDbType.VarChar, 100).Value = model.FirstName; 
                    insert.Parameters.Add("@sLastName", SqlDbType.VarChar, 100).Value = model.LastName; 
                    insert.Parameters.Add("@sEmailId", SqlDbType.VarChar, 200).Value = model.Email; 
                    insert.Parameters.Add("@sUserName", SqlDbType.VarChar, 100).Value = model.UserName; 
                    insert.Parameters.Add("@sPassword", SqlDbType.VarChar, 200).Value = model.Password;

                    var returnParameter = insert.Parameters.Add("@Message", SqlDbType.VarChar);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    conn.Open();
                    insert.ExecuteNonQuery();
                    //Add Mail Block Also

                    conn.Close();
                    return Convert.ToInt32(returnParameter.Value);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}