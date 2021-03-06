﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestFactory.MVC.ViewModels
{
    public class SubjectViewModel: BaseViewModel
    {
        [Required(ErrorMessage =  "Поле має бути встановлено")] 
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Перевищує ліміт в 100 символів")] 
        public virtual string ShortName { get; set; }
        public virtual string LongName { get; set; }
        public virtual string GroupId { get; set; }

        public SubjectViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
