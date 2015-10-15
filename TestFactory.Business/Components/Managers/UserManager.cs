using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;
using SimpleCrypto;
using System.Security.Cryptography;
using System.Web;

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
            User admin1 = new User();
            admin1.Email = "fillerFikt@gmail.com";
            admin1.FirstName = "FillerFikt";
            admin1.LastName = "FF";
            admin1.PasswordSalt = new PBKDF2().GenerateSalt();
            admin1.Password = new PBKDF2().Compute("IFiktF", admin1.PasswordSalt);

            Role rol1 = new Role();
            rol1.Id = "316987d9-9e4e-4cc4-b32a-b64112ca20be";
            rol1.Name = "Filler";
            admin1.Roles = rol1;

            provider.Create(admin1);
        }
        public bool IsPasswordValid(string email, string password)
        {
            //AddFirstRole();
            var user = provider.GetByEmail(email);
            if(user != null)
            {
                bool correctPass = String.Equals(user.Password, new PBKDF2().Compute(password, user.PasswordSalt));
                if (correctPass)
                {
                    HttpContext.Current.User = new System.Security.Principal
                        .GenericPrincipal(new System.Security.Principal.GenericIdentity(user.FirstName), new[] { user.Roles.Name });
                }
                return correctPass;
            }
            return false;
        }
    }
}
