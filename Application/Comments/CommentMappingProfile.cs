using Application.Comments.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Comments;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<Comment, CommentDTO>();
    }
}
