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
            RegisterToViewModel();
            RegisterFromViewModel();
            Mapper.AssertConfigurationIsValid();
        }
        private static void RegisterToViewModel()
        {
            Mapper
               .CreateMap<User, UserViewModel>()
               .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
                .CreateMap<Student, StudentViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
                .CreateMap<Category, CategoryWebModel>()
                .IncludeBase<BaseModel, BaseViewModel>();
            Mapper
               .CreateMap<Mark, MarkWebModel>()
               .IncludeBase<BaseModel, BaseViewModel>();

            Mapper.CreateMap<Group, GroupViewModel>()
               .IncludeBase<BaseModel, BaseViewModel>()
               .ForMember(groupvm => groupvm.Students, otp => otp.Ignore());

            
        }
        private static void RegisterFromViewModel()
        {
            Mapper.CreateMap<StudentViewModel, Student>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<CategoryWebModel, Category>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<MarkWebModel, Mark>()
               .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<GroupViewModel, Group>()
                .IncludeBase<BaseViewModel, BaseModel>();
        }
    }
}