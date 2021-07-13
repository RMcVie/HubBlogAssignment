using HubBlogAssignment.UI.Services;
using HubBlogAssignment.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HubBlogAssignment.Shared.DML;
using HubBlogAssignment.Shared.Read;

namespace HubBlogAssignment.UI.Components
{
    public partial class CommentFeed : ComponentBase
    {
        [Inject] protected ICommentService CommentService { get; set; }
        [Parameter] public int PostId { get; set; }
        protected IEnumerable<CommentReadDto> Comments { get; set; }
        protected string CommentText { get; set; }
        private OrderBy orderBy;

        protected override Task OnInitializedAsync()
        {
            return LoadData(default);
        }

        protected async Task LoadData(OrderBy orderBy)
        {
            this.orderBy = orderBy;
            Comments = await CommentService.GetComments(PostId, orderBy);
        }

        protected async Task CreateComment()
        {
            await CommentService.CreateComment(PostId, new CommentDmlDto { Content = CommentText });
            await LoadData(orderBy);
        }
    }
}
