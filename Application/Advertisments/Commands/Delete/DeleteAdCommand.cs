using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.Advertisments.Commands.Delete;

public class DeleteAdCommand : IRequest<bool>
{
    public string Id { get; set; }
}

public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, bool>
{
    private readonly IMongoContext context;

    public DeleteAdCommandHandler(IMongoContext context) => this.context = context;

    public async Task<bool> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Ad>.Filter.Eq("_id", request.Id);
        var result = await context.Advertisments.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}