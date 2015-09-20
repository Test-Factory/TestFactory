using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using TestFactory.Business.Models;


namespace TestFactory.Business.Components
{
    public abstract class UserContext
    {
        public static UserContext Current
        {
            get
            {
                return DependencyResolver.Current.GetService<UserContext>();
            }
        }

        public abstract bool IsLogged(string role);

        public abstract bool IsLoggedIn { get; }

        public abstract User User { get; }
    }
}
