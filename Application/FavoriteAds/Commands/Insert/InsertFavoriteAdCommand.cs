using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.FavoriteAds.Commands.Insert;

public class InsertFavoriteAdCommand : IRequest<string>
{
    public string AdvertismentId { get; set; }
    public string UserId { get; set; }
}

public class InsertFavoriteAdCommandHandler : IRequestHandler<InsertFavoriteAdCommand, string>
{
    private readonly IMongoContext context;

    public InsertFavoriteAdCommandHandler(IMongoContext context) => this.context = context;

    public async Task<string> Handle(InsertFavoriteAdCommand request, CancellationToken cancellationToken)
    {
        FavoriteAd favoriteAd = new FavoriteAd() { AdShortInfoId = request.AdvertismentId, UserId = request.UserId };
        await context.FavoriteAds.InsertOneAsync(favoriteAd);
        return favoriteAd.Id;
    }
}
