﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.MVC.ViewModels
{
    public class GroupViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public virtual string FullName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public virtual string ShortName { get; set; }
    }
}
