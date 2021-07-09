using AutoMapper;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignemnt.AZFunction
{
    public class EntityMapperProfile : Profile
    {
        public EntityMapperProfile()
        {
            CreateMap<PostDb, PostReadDto>().ReverseMap();
            CreateMap<PostDmlDto, PostDb>()
                .ForMember(d=>d.PostId, opts => opts.Ignore())
                .ForMember(d=>d.CreatedDateTimeUtc, opts => opts.Ignore())
                .ForMember(d=>d.CreatedUser, opts => opts.Ignore())
                .ForMember(d=>d.Comments, opts => opts.Ignore());
            CreateMap<CommentDb, CommentReadDto>().ReverseMap();
            CreateMap<CommentDmlDto, CommentDb>()
                .ForMember(d => d.CommentId, opts => opts.Ignore())
                .ForMember(d => d.CreatedDateTimeUtc, opts => opts.Ignore())
                .ForMember(d => d.CreatedUser, opts => opts.Ignore())
                .ForMember(d => d.PostId, opts => opts.Ignore())
                .ForMember(d => d.Score, opts => opts.Ignore());
        }
    }
}
