using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Models;

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

            Mapper.CreateMap<Group, GroupViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>()
                //.ForMember(groupvm => groupvm.Students, otp => otp.Ignore());
            ;

            Mapper.CreateMap<GroupViewModel, Group>()
                .IncludeBase<BaseViewModel, BaseModel>()
                .ForMember(groupvm=> groupvm.Students, otp=>otp.Ignore());
            
            Mapper.AssertConfigurationIsValid();
        }
    }
}