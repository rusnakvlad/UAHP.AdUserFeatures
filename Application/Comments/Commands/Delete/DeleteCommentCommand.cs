using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.Comments.Commands.Delete;

public class DeleteCommentCommand : IRequest
{
    public string Id { get; set; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly IMongoContext context;

    public DeleteCommentCommandHandler(IMongoContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Comment>.Filter.Eq("_id", request.Id);
        await context.Comments.DeleteOneAsync(filter, cancellationToken);
        return Unit.Value;
    }
}