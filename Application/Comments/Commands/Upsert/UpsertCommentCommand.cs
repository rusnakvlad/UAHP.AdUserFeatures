using Application.Comments.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.Comments.Commands.Delete;

public class UpsertCommentCommand : IRequest<CommentDTO>
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string AdId { get; set; }
    public string Text { get; set; }
}

public class UpsertCommentCommandHandler : IRequestHandler<UpsertCommentCommand, CommentDTO>
{
    private readonly IMongoContext context;
    private readonly IMapper mapper;
    
    public UpsertCommentCommandHandler(IMongoContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<CommentDTO> Handle(UpsertCommentCommand request, CancellationToken cancellationToken)
    {
        Comment comment;
        if (!string.IsNullOrEmpty(request.Id) && !string.IsNullOrWhiteSpace(request.Id))
        {
            var filter = Builders<Comment>.Filter.Eq("_id", request.Id);
            comment = context.Comments.Find(filter).FirstOrDefault();

            if (comment == null)
            {
                throw new Exception("Not found advertisment with such id");
            }

            var update = Builders<Comment>.Update
                .Set(c => c.Text, request.Text)
                .Set(c => c.DateOfComment, DateTime.Now);

            await context.Comments.UpdateOneAsync(filter, update);
            return mapper.Map<CommentDTO>(context.Comments.Find(filter).FirstOrDefault());
        }
        else
        {
            comment = new Comment()
            {
                UserId = request.UserId,
                AdId = request.AdId,
                Text = request.Text,
                DateOfComment = DateTime.Now,
            };
            await context.Comments.InsertOneAsync(comment);
            return mapper.Map<CommentDTO>(comment);
        }
    }
}