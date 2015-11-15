using AutoMapper;
using TestFactory.Business.Components.Managers;
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
              .CreateMap<Subject, SubjectViewModel>()
              .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
                .CreateMap<Student, StudentViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
                .CreateMap<StudentWithGroup, StudentWithGroupViewModel>();

            Mapper
                .CreateMap<SubjectWithGroup, SubjectWithGroupViewModel>();

            Mapper
               .CreateMap<AverageMarkForFaculty, AverageMarkForFacultyViewModel>();

            Mapper
              .CreateMap<FrequencyMarkForFacultyByCategory, FrequencyMarkForFacultyByCategoryViewModel>();

            Mapper
                .CreateMap<Category, CategoryViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
               .CreateMap<Mark, MarkViewModel>()
               .IncludeBase<BaseModel, BaseViewModel>();

            Mapper.CreateMap<Group, GroupViewModel>()
               .IncludeBase<BaseModel, BaseViewModel>()
               .ForMember(groupvm => groupvm.Students, otp => otp.Ignore());
        }
        private static void RegisterFromViewModel()
        {
            Mapper.CreateMap<StudentViewModel, Student>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<StudentWithGroupViewModel, StudentWithGroup>();

            Mapper.CreateMap<AverageMarkForFacultyViewModel, AverageMarkForFaculty>();

            Mapper.CreateMap<SubjectWithGroupViewModel, SubjectWithGroup>();

            Mapper.CreateMap<FrequencyMarkForFacultyByCategoryViewModel, FrequencyMarkForFacultyByCategory>();

            Mapper.CreateMap<CategoryViewModel, Category>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<SubjectViewModel, Subject>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<MarkViewModel, Mark>()
               .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<GroupViewModel, Group>()
                .IncludeBase<BaseViewModel, BaseModel>();
        }
    }
}