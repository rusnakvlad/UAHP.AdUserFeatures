using Application.Common.Interfaces;
using MediatR;

namespace Application.Cache.Queries;
public class GetCacheOjdQuery : IRequest<string>
{
    public string Key { get; set; }
}

public class GetCacheOjdQueryHandler : IRequestHandler<GetCacheOjdQuery, string>
{
    private readonly ICacheService cacheService;

    public GetCacheOjdQueryHandler(ICacheService cacheService) => this.cacheService = cacheService;

    public async Task<string> Handle(GetCacheOjdQuery request, CancellationToken cancellationToken)
    {
        var result = cacheService.Get<string>(request.Key);
        return result;
    }
}


