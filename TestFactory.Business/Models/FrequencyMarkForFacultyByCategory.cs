using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
     public class FrequencyMarkForFacultyByCategory
    {
        public virtual string Faculty { get; set; }

        public virtual string Code { get; set; }

        public virtual string CategoryId { get; set; }

        public virtual int Value { get; set; }

        public virtual int Count { get; set; }

        #region overrides 
        protected bool Equals(FrequencyMarkForFacultyByCategory other)
        {
            return string.Equals(Faculty, other.Faculty) && string.Equals(Code, other.Code) && string.Equals(CategoryId, other.CategoryId) && Value == other.Value && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((FrequencyMarkForFacultyByCategory)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Faculty != null ? Faculty.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Code != null ? Code.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CategoryId != null ? CategoryId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Value;
                hashCode = (hashCode * 397) ^ Count;
                return hashCode;
            }
        } 
        #endregion
    }
}
