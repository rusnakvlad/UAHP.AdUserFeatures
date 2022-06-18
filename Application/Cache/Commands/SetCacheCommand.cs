using Application.Common.Interfaces;
using MediatR;

namespace Application.Cache.Commands;
public class SetCacheCommand : IRequest
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public class SetCacheCommandHandler : IRequestHandler<SetCacheCommand>
{
    private readonly ICacheService cacheService;

    public SetCacheCommandHandler(ICacheService cacheService) => this.cacheService = cacheService;

    public async Task<Unit> Handle(SetCacheCommand request, CancellationToken cancellationToken)
    {
        cacheService.Set(request.Key, request.Value, false);
        return Unit.Value;
    }
}

