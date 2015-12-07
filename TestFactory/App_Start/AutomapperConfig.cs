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
               .CreateMap<SubjectViewModel, Subject>()
               .ForMember(s => s.FacultyId, otp => otp.Ignore())
               .ForMember(s => s.Groups, otp => otp.Ignore());

            Mapper
               .CreateMap<Subject, SubjectViewModel>()
               .ForMember(s => s.GroupId, otp => otp.Ignore());

            Mapper
              .CreateMap<Role, RoleViewModel>()
              .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
               .CreateMap<User, UserViewModel>()
               .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
              .CreateMap<Subject, SubjectViewModel>()
              .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
             .CreateMap<Faculty, FacultyViewModel>()
             .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
                .CreateMap<Student, StudentViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();

            Mapper
                .CreateMap<StudentWithGroup, StudentWithGroupViewModel>();

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

            Mapper
              .CreateMap<SubjectMark, SubjectMarkViewModel>()
              .IncludeBase<BaseModel, BaseViewModel>();

            Mapper.CreateMap<Group, GroupViewModel>()
               .IncludeBase<BaseModel, BaseViewModel>()
               .ForMember(groupvm => groupvm.Students, otp => otp.Ignore());
        }
        private static void RegisterFromViewModel()
        {
            Mapper.CreateMap<StudentViewModel, Student>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<GroupViewModel, Group>()
               .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<FacultyViewModel, Faculty>()
               .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<UserViewModel, User>()
               .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<RoleViewModel, Role>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<StudentWithGroupViewModel, StudentWithGroup>();

            Mapper.CreateMap<AverageMarkForFacultyViewModel, AverageMarkForFaculty>();

            Mapper.CreateMap<FrequencyMarkForFacultyByCategoryViewModel, FrequencyMarkForFacultyByCategory>();

            Mapper.CreateMap<CategoryViewModel, Category>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<SubjectViewModel, Subject>()
                .IncludeBase<BaseViewModel, BaseModel>()
                .ForMember(s => s.Groups, otp => otp.Ignore())
                .ForMember(s => s.FacultyId, otp => otp.Ignore());

            Mapper.CreateMap<SubjectMarkViewModel, SubjectMark>()
                .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<MarkViewModel, Mark>()
               .IncludeBase<BaseViewModel, BaseModel>();

            Mapper.CreateMap<MarkViewModel, Mark>()
               .IncludeBase<BaseViewModel, BaseModel>();
        }
    }
}