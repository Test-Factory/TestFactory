using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;
using TestFactory.Business.Data_Provider_Contracts;
using SimpleCrypto;

namespace TestFactory.Business.Components.Managers
{
    public class UserManager : BaseManager<User, IUserDataProvider>
    {
        public UserManager(IUserDataProvider provider) : base(provider){ }

        public IList<User> GetList()
        {
            return null;
        }

        public bool IsPasswordValid(string email, string password)
        {
            var user = provider.GetByEmail(email);
            if(user != null)
            {
                return String.Equals(user.Password, new PBKDF2().Compute(password, user.PasswordSalt));
            }
            return false;
        }
    }
}
