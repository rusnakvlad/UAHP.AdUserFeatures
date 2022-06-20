using Application.Common.CommonDTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using MongoDB.Driver;

namespace Application.Advertisments.Queries.GetUserAdvertismentsQuery;
public class GetUserAdvertismentsQuery : IRequest<List<AdShortInfoDTO>>
{
    public string OwnerId { get; set; }
}

public class GetUserAdvertismentsQueryHandler : IRequestHandler<GetUserAdvertismentsQuery, List<AdShortInfoDTO>>
{
    private readonly IMongoContext context;
    private readonly IMapper mapper;

    public GetUserAdvertismentsQueryHandler(IMongoContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public Task<List<AdShortInfoDTO>> Handle(GetUserAdvertismentsQuery request, CancellationToken cancellationToken)
    {
        var user = context.Users.AsQueryable().FirstOrDefault(x => x.ExternalId == request.OwnerId);
        var data = context.Advertisments.AsQueryable().Where(x => x.OwnerId == user.Id);

        var ads = new List<AdShortInfoDTO>();
        foreach (var item in data)
        {
            ads.Add(mapper.Map<AdShortInfoDTO>(item));
        }
        return Task.Run(() => ads);
    }
}


