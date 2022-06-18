using Application.Common.CommonDTOs;
using Application.Common.Interfaces;
using MediatR;

namespace Application.FavoriteAds.Commands.Insert;
public class InsertFavoriteInCacheCommand : IRequest
{
    public AdShortInfoDTO AdShortInfoDTO { get; set; }
}

public class InsertFavoriteInCacheCommandHandler : IRequestHandler<InsertFavoriteInCacheCommand>
{
    private readonly ICacheService cacheService;
    public InsertFavoriteInCacheCommandHandler(ICacheService cacheService) => this.cacheService = cacheService;

    public async Task<Unit> Handle(InsertFavoriteInCacheCommand request, CancellationToken cancellationToken)
    {
        cacheService.Set("favorite", request.AdShortInfoDTO, true);
        return Unit.Value;
    }
}