using HubBlogAssignment.Shared.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubBlogAssignment.UI.Components
{
    public partial class CategorySelector
    {
        protected IEnumerable<CategoryReadDto> Categories { get; set; }
    }
}
