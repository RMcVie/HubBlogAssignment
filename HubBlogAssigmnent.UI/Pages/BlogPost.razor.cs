using HubBlogAssignemnt.UI.Services;
using HubBlogAssignment.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubBlogAssigmnent.UI.Pages
{
    public partial class BlogPost
    {
        [Parameter] public int PostId { get; set; }
        [Inject] protected IPostService PostService {get;set;}
        protected PostReadDto Post { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Post = await PostService.GetPost(PostId);
        }
    }
}
