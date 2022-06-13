using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.Users.Commands.Delete;

public class DeleteUserCommand : IRequest
{
    public string Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IMongoContext context;
    public DeleteUserCommandHandler(IMongoContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<User>.Filter.Eq("_id", request.Id);
        await context.Users.DeleteOneAsync(filter, cancellationToken);
        return Unit.Value;
    }
}