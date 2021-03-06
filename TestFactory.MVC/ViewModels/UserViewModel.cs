﻿using System;
using System.ComponentModel.DataAnnotations;
using TestFactory.Business.Models;

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

        [ScaffoldColumn(false)]
        public virtual string FacultyId { get; set; }

        [ScaffoldColumn(false)]
        public virtual string Roles_id { get; set; }

        [ScaffoldColumn(false)]
        public virtual string PasswordSalt { get; set; }

        [ScaffoldColumn(false)]
        public virtual Role Roles { get; set; }

        public UserViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
