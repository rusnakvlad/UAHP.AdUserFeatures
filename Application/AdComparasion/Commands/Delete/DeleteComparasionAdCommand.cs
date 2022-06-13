using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.AdComparasion.Commands.Delete;

public class DeleteComparasionAdCommand : IRequest
{
    public string Id { get; set; }
}

public class DeleteComparasionAdCommandHandler : IRequestHandler<DeleteComparasionAdCommand>
{
    private readonly IMongoContext context;

    public DeleteComparasionAdCommandHandler(IMongoContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteComparasionAdCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<ComparasionAd>.Filter.Eq("_id", request.Id);
        await context.ComparasionAds.DeleteOneAsync(filter);
        return Unit.Value;
    }
}
