using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data.Errors
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(object entity)
            : base($"Entity of Type {entity.GetType()} Already Exists!")
        { }
    }

    public class EntityDoesNotExistException : Exception
    {
        public EntityDoesNotExistException(Type type)
            : base($"Entity of Type {type} Does Not Exist!")
        { }
    }
}
        