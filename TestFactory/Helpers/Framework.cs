using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Components;
using TestFactory.Business.Components.Managers;

namespace TestFactory
{
    public static class Framework
    {

        public static GroupManager GroupManager
         {
             get 
             { 
                 return DependencyResolver.Current.GetService<GroupManager>();
             }
         }

        public static StudentManager StudentManager
        {
            get 
            { 
                return DependencyResolver.Current.GetService<StudentManager>(); 
            }
        }

        public static UserManager userManager
        {
            get
            {
                return DependencyResolver.Current.GetService<UserManager>(); 
            }
        }

        public static SubjectManager SubjectManager
        {
            get
            {
                return DependencyResolver.Current.GetService<SubjectManager>();
            }
        }

        public static SubjectMarkManager SubjectMarkManager
        {
            get
            {
                return DependencyResolver.Current.GetService<SubjectMarkManager>();
            }
        }

        public static MarkManager MarkManager
        {
            get
        {
                return DependencyResolver.Current.GetService<MarkManager>(); 
            }
        }

        public static CategoryManager CategoryManager
        {
            get
            {
                return DependencyResolver.Current.GetService<CategoryManager>();
            }
        }
	   
        public static StudentWithGroupManager StudentWithGroupManager
        {
            get
            {
                return DependencyResolver.Current.GetService<StudentWithGroupManager>();
            }
        }
       
        public static AverageMarkForFacultyManager AverageMarkForFacultyManager
        {
            get
            {
                return DependencyResolver.Current.GetService<AverageMarkForFacultyManager>();
            }
        }
        
        public static FrequencyMarkForFacultyByCategoryManager FrequencyMarkForFacultyByCategoryManager
        {
            get
            {
                return DependencyResolver.Current.GetService<FrequencyMarkForFacultyByCategoryManager>();
            }
        }

        public static UserContext UserContext
        {
            get
            {
                return new UserContext();
            }
        }
    }
}