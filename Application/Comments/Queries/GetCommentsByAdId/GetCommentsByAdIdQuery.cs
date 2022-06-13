using Application.Comments.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using MongoDB.Driver;

namespace Application.Comments.Queries.GetCommentsByAdId;

public class GetCommentsByAdIdQuery : IRequest<List<CommentDTO>>
{
    public string AdId { get; set; }
}

public class GetCommentsByAdIdQueryHandler : IRequestHandler<GetCommentsByAdIdQuery, List<CommentDTO>>
{
    private readonly IMongoContext context;
    private readonly IMapper mapper;

    public GetCommentsByAdIdQueryHandler(IMongoContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<List<CommentDTO>> Handle(GetCommentsByAdIdQuery request, CancellationToken cancellationToken)
    {
        var comments = context.Comments.AsQueryable().Where(comment => comment.AdId == request.AdId);
        return mapper.Map<List<CommentDTO>>(comments);
    }
}
