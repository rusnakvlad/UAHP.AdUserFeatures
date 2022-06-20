using Application.Common.CommonDTOs;
using Application.Common.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace Application.FavoriteAds.Queries.GetFavoriteAds;

public class GetFavoriteAdsByUserIdQuery : IRequest<List<AdShortInfoDTO>>
{
    public string UserId { get; set; }
}

public class GetFavoriteAdsByUserIdQueryHandler : IRequestHandler<GetFavoriteAdsByUserIdQuery, List<AdShortInfoDTO>>
{
    private readonly IMongoContext context;
    public GetFavoriteAdsByUserIdQueryHandler(IMongoContext context) => this.context = context;

    public async Task<List<AdShortInfoDTO>> Handle(GetFavoriteAdsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = from user in context.Users.AsQueryable().Where(u => u.Id == request.UserId)
                    join fav in context.FavoriteAds.AsQueryable() on user.Id equals fav.UserId
                    join ad in context.Advertisments.AsQueryable() on fav.AdShortInfoId equals ad.Id
                    select new AdShortInfoDTO()
                    {
                        Id = fav.Id,
                        AdExternalId = ad.adExternalId,
                        OwnerId = user.Id,
                        TitleImage = ad.TitleImage,
                        Price = ad.Price,
                        RoomNumber = ad.RoomNumber,
                        Balkon = ad.Balkon,
                        HouseYear = ad.HouseYear,
                        AreaOfHouse = ad.AreaOfHouse
                    };
        var ads = query.ToList();
        return ads;
    }
}