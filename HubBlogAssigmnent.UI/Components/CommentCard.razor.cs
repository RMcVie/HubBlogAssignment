using HubBlogAssignment.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubBlogAssigmnent.UI.Components
{
    public partial class CommentCard
    {
        [Parameter] public CommentReadDto Comment { get; set; }
        protected bool Liked { get; set; } = false;
    }
}
