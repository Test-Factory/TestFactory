using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;
using SimpleCrypto;
using System.Web;

namespace TestFactory.Business.Components.Managers
{
    public class UserManager : BaseManager<User, IUserDataProvider>
    {
        public UserManager(IUserDataProvider provider) : base(provider) { }

        public bool UserRole { get; set; }

        public User GetByEmail(string email)
        {
            return provider.GetByEmail(email);
        }

        public bool IsRoleAssigned(string email, string password)
        {
            var user = provider.GetByEmail(email);

            if (user == null)
            { 
                return false;
            }

            return CheckCorrectPassword(user, password);
        }

        public bool IsPasswordValid(User user, string password)
        {
            return (bool)HashDecoder.VerifyHash(password, user.Password, user.PasswordSalt);
        }

        private bool CheckCorrectPassword(User user, string password)
        {
            if (IsPasswordValid(user, password))
            {
                SetRole(user);
                return true;
            }
            return false;
        }

        private void SetRole(User user)
        {
           // User admin = new User();
           // admin.Id = "6be4c229-3846-4f5e-93e5-e803cf4b233d";
           // admin.Email = "Admin@gmail.com";
           // admin.PasswordSalt = new PBKDF2().GenerateSalt();
           // admin.Password = new PBKDF2().Compute("IAdmin", admin.PasswordSalt);
           // // admin.Roles_id = "439bdb3f-4fe6-4def-a329-09a20ed888dc";

           // admin.FacultyId = "FBF5D69A-54F8-4146-AC9C-417BAA3E5122";
           // admin.FirstName = "Admin";                    
           //// provider.Create(admin);
           

            HttpContext.Current.User = new System.Security.Principal
                        .GenericPrincipal(new System.Security.Principal.GenericIdentity(user.Email), new[] { user.Roles.Name });
        }
    }
}
