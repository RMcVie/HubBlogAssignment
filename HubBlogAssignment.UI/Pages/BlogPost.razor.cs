using HubBlogAssignment.UI.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using HubBlogAssignment.Shared.Read;

namespace HubBlogAssignment.UI.Pages
{
    public partial class BlogPost : ComponentBase
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
