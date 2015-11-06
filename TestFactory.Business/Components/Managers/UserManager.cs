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

            if (IsPasswordValid(user, password))
            {
                HttpContext.Current.User = new System.Security.Principal
                        .GenericPrincipal(new System.Security.Principal.GenericIdentity(user.FirstName), new[] { user.Roles.Name });
                return true;
            }
         
            return false;
        }

        public bool IsPasswordValid(User user, string password)
        {
            return (bool)HashDecoder.VerifyHash(password, user.Password, user.PasswordSalt);
        }
    }
}
