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
            User admin = new User();
            admin.Email = "TF.Filler@ukr.net";
            admin.FirstName = "Filler";
            admin.LastName = "TF";
            admin.PasswordSalt = new PBKDF2().GenerateSalt();
            admin.Password = new PBKDF2().Compute("IFiller", admin.PasswordSalt);

            Role rol = new Role();
            rol.Id = "12dc6a23-8454-419f-ac75-2ea0560d27ef";
            rol.Name = "Filler";
            admin.Roles = rol;

            provider.Create(admin);
        }
        public bool IsPasswordValid(string email, string password)
        {
            password = "3253v";
            email = "TF.Filler@ukr.net";
            var user = provider.GetByEmail(email);
            if(user != null)
            {
                user.Password = "3253v";
                user.Email = "TF.Filler@ukr.net";
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
