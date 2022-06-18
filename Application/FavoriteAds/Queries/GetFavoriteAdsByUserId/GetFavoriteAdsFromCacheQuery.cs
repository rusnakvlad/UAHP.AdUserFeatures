using Application.Common.CommonDTOs;
using Application.Common.Interfaces;
using MediatR;

namespace Application.FavoriteAds.Queries.GetFavoriteAdsByUserId;

public class GetFavoriteAdsFromCacheQuery : IRequest<List<AdShortInfoDTO>>
{
}

public class GetFavoriteAdsFromCacheQueryHandler : IRequestHandler<GetFavoriteAdsFromCacheQuery, List<AdShortInfoDTO>>
{
    private readonly ICacheService cacheService;
    public GetFavoriteAdsFromCacheQueryHandler(ICacheService cacheService) => this.cacheService = cacheService;

    public async Task<List<AdShortInfoDTO>> Handle(GetFavoriteAdsFromCacheQuery request, CancellationToken cancellationToken)
    {
        var result = cacheService.GetList<AdShortInfoDTO>($"favorite");
        return result;
    }
}
