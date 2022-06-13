using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.FavoriteAds.Commands.Delete;

public class DeleteFavoriteAdCommand : IRequest
{
    public string Id { get; set; }
}

public class DeleteFavoriteAdCommandHandler : IRequestHandler<DeleteFavoriteAdCommand>
{
    private readonly IMongoContext context;

    public DeleteFavoriteAdCommandHandler(IMongoContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteFavoriteAdCommand request, CancellationToken cancellationToken)
    {
        var fitler = Builders<FavoriteAd>.Filter.Eq("_id", request.Id);
        await context.FavoriteAds.DeleteOneAsync(fitler);
        return Unit.Value;
    }
}