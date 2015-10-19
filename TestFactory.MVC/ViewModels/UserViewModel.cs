using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TestFactory.MVC.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        [DataType(DataType.EmailAddress,ErrorMessage="Введіть адресу електронної пошти")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        [DataType(DataType.Password,ErrorMessage="Введіть пароль")]
        public string Password { get; set; }
    }
}
