using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.Business.Models
{
    public class SubjectWithGroup 
    {
        public virtual string Name { get; set; }
        public virtual string SubjectId { get; set; }
        public virtual string GroupId { get; set; }

        #region overrides
        protected bool Equals(SubjectWithGroup other)
        {
            return string.Equals(SubjectId, other.SubjectId) && string.Equals(Name, other.Name) && string.Equals(GroupId, other.GroupId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((SubjectWithGroup)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (SubjectId != null ? SubjectId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GroupId != null ? GroupId.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}
