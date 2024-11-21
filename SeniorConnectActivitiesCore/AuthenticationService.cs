using MySql.Data.MySqlClient;
using SeniorConnectActivitiesShared.Models;
using System.Data;

namespace SeniorConnectActivitiesCore
{
    public class AuthenticationService
    {
        public async Task<bool> Login(string formEmail, string formPassword, List<UserModel> users)
        {
            var matchingUser = users.FirstOrDefault(u => u.Email == formEmail && u.Password == formPassword);

            if (matchingUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

