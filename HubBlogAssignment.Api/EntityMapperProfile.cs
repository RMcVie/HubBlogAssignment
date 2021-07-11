using AutoMapper;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Api
{
    public class EntityMapperProfile : Profile
    {
        public EntityMapperProfile()
        {
            CreateMap<Post, PostReadDto>().ReverseMap();
            CreateMap<PostDmlDto, Post>()
                .ForMember(d=>d.Id, opts => opts.Ignore())
                .ForMember(d=>d.CreatedDateTimeUtc, opts => opts.Ignore())
                .ForMember(d=>d.Comments, opts => opts.Ignore())
                .ForMember(d=>d.User, opts => opts.Ignore())
                .ForMember(d=>d.Categories, opts => opts.Ignore());
            CreateMap<Comment, CommentReadDto>().ReverseMap();
            CreateMap<CommentDmlDto, Comment>()
                .ForMember(d => d.Id, opts => opts.Ignore())
                .ForMember(d => d.CreatedDateTimeUtc, opts => opts.Ignore())
                .ForMember(d => d.Score, opts => opts.Ignore())
                .ForMember(d=>d.User, opts => opts.Ignore());
        }
    }
}
