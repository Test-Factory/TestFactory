using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Models;

namespace TestFactory.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper
                
                .CreateMap<User, UserViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();
            AutoMapper.Mapper
                .CreateMap<Student, StudentViewModel>();
            AutoMapper.Mapper
                .CreateMap<Group, GroupViewModel>();
        }
    }
}