using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;

namespace TestFactory
{
    public static class Framework
    {

        public static GroupManager groupManager
         {
             get { 
                 return DependencyResolver.Current.GetService<GroupManager>();
             }
         }
        public static StudentManager studentManager
        {
            get { 
                return DependencyResolver.Current.GetService<StudentManager>(); 
            }
        }

        public static SubjectManager SubjectManager
        {
            get
            {
                return DependencyResolver.Current.GetService<SubjectManager>();
            }
        }

        public static UserManager userManager
        {
            get {
                return DependencyResolver.Current.GetService<UserManager>(); 
            }
        }

        public static RoleManager roleManager
        {
            get
            {
                return DependencyResolver.Current.GetService<RoleManager>();
            }
        }

        public static SubjectManager subjectManager
        {
            get
            {
                return DependencyResolver.Current.GetService<SubjectManager>();
            }
        }

        public static SubjectMarkManager subjectMarkManager
        {
            get
            {
                return DependencyResolver.Current.GetService<SubjectMarkManager>();
            }
        }

        public static MarkManager markManager
        {
            get { 
                return DependencyResolver.Current.GetService<MarkManager>(); 
            }
        }

        public static CategoryManager categoryManager
        {
            get
            {
                return DependencyResolver.Current.GetService<CategoryManager>();
            }
        }
	   
        public static StudentWithGroupManager studentWithGroupManager
        {
            get
            {
                return DependencyResolver.Current.GetService<StudentWithGroupManager>();
            }
        }
       
        public static AverageMarkForFacultyManager averageMarkForFacultyManager
        {
            get
            {
                return DependencyResolver.Current.GetService<AverageMarkForFacultyManager>();
            }
        }
        
        public static FrequencyMarkForFacultyByCategoryManager frequencyMarkForFacultyByCategoryManager
        {
            get
            {
                return DependencyResolver.Current.GetService<FrequencyMarkForFacultyByCategoryManager>();
            }
        }
    }
}