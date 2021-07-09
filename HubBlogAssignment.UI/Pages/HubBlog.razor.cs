using HubBlogAssignment.UI.Services;
using HubBlogAssignment.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubBlogAssigmnent.UI.Pages
{
    public partial class HubBlog : ComponentBase
    {
        protected IEnumerable<PostReadDto> Posts;
        [Inject] protected IPostService PostService { get; set; }
        [Inject] protected NavigationManager NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Posts = await PostService.GetPosts();
        }

        protected void NavigateToPost(int postId)
        {
            NavManager.NavigateTo($"/{postId}");
        }
    }
}
