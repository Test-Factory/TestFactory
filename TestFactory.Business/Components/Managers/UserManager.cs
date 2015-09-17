using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;
using TestFactory.Business.Data_Provider_Contracts;
using SimpleCrypto;
using System.Security.Cryptography;

namespace TestFactory.Business.Components.Managers
{
    public class UserManager : BaseManager<User, IUserDataProvider>
    {
        public UserManager(IUserDataProvider provider) : base(provider){ }

        public User GetByEmail(string email)
        {
            return provider.GetByEmail(email);
        }

        public void AddFirstRole()
        {
            User admin = new User();
            admin.Email = "02bodia20@ukr.net";
            admin.FirstName = "Bodia";
            admin.LastName = "Semenets";
            admin.Password = "123321";
            admin.PasswordSalt = new PBKDF2().GenerateSalt();
            admin.Role = true;

            provider.Create(admin);
        }

        public bool IsPasswordValid(string email, string password)
        {
            var user = provider.GetByEmail(email);
            if(user != null)
            {
                var t = new PBKDF2().Compute(password, user.PasswordSalt);
                return String.Equals(user.Password, password);
            }
            return false;
        }
    }
}
