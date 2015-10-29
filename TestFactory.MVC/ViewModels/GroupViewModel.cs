﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace TestFactory.MVC.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        [Required(ErrorMessage="Поле повинно бути заповненим")]
        [DataType(DataType.Text)]
        [AllowHtml]
        public virtual string FullName { get; set; }

        [Required(ErrorMessage="Поле повинно бути заповненим")]
        [DataType(DataType.Text)]
        [AllowHtml]
        public virtual string ShortName { get; set; }      

        public virtual IList<StudentViewModel> Students { get; set; }

        [ScaffoldColumn(false)]
        public virtual string Faculty { get; set; }      

        public GroupViewModel()
        {
            Students = new List<StudentViewModel>();
            Id = Guid.NewGuid().ToString();
        }
    }
}
