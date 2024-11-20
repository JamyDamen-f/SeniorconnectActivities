using MySql.Data.MySqlClient;
using System.Data;

namespace SeniorConnectActivitiesCore
{
    public class AuthenticationService
    {
        public async Task<bool> Login(string formEmail, string email, string formPassword, string password)
        {
            //TODO: Check of the login juist is van de user


            if (formEmail != email || formPassword != password)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}

