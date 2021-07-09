using HubBlogAssignment.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubBlogAssigmnent.UI.Components
{
    public partial class PostSummary
    {
        [Parameter] public PostReadDto Post { get; set; }
        [Parameter] public EventCallback<int> OnPostClickCallback { get; set; }

        protected async Task OnClick()
        {
            await OnPostClickCallback.InvokeAsync(Post.PostId);
        }
    }
}
