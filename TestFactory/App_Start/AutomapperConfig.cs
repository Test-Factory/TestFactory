using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Models;
using  AutoMapper;

namespace TestFactory.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper
                .CreateMap<User, UserViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();
            Mapper
                .CreateMap<Student, StudentViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();
            Mapper
                .CreateMap<Group, GroupViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();
        }
    }
}