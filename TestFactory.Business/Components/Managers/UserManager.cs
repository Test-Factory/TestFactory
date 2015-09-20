using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;
using SimpleCrypto;
using System.Security.Cryptography;

namespace TestFactory.Business.Components.Managers
{
    public class UserManager : BaseManager<User, IUserDataProvider>
    {
        public UserManager(IUserDataProvider provider) : base(provider){ }

        public bool UserRole { get; set; }

        public User GetByEmail(string email)
        {
            return provider.GetByEmail(email);
        }

        public void AddFirstRole()
        {
            User admin = new User();
            admin.Email = "TF.Filler@ukr.net";
            admin.FirstName = "Filler";
            admin.LastName = "TF";
            admin.PasswordSalt = new PBKDF2().GenerateSalt();
            admin.Password = new PBKDF2().Compute("IFiller", admin.PasswordSalt);
            //admin.Role = false;

            provider.Create(admin);
        }

        public bool IsPasswordValid(string email, string password)
        {
            var user = provider.GetByEmail(email);
            if(user != null)
            {
                bool correctPass = String.Equals(user.Password, new PBKDF2().Compute(password, user.PasswordSalt));
                if (correctPass)
                {
                    //UserRole = user.Role;
                }
                return correctPass;
            }
            return false;
        }
    }
}
