using Application.Common.CommonDTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using MongoDB.Driver;

namespace Application.Advertisments.Queries.GetAll;

public class GetAllAdvertismentsQuery : IRequest<List<AdShortInfoDTO>>
{
}

internal class GetAllAdvertismentsQueryHandler : IRequestHandler<GetAllAdvertismentsQuery, List<AdShortInfoDTO>>
{
    private readonly IMongoContext context;
    private readonly IMapper mapper;

    public GetAllAdvertismentsQueryHandler(IMongoContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public Task<List<AdShortInfoDTO>> Handle(GetAllAdvertismentsQuery request, CancellationToken cancellationToken)
    {
        var data = context.Advertisments.AsQueryable();
        var ads = new List<AdShortInfoDTO>();
        foreach (var item in data) {
            ads.Add(mapper.Map<AdShortInfoDTO>(item));
        }
        return Task.Run(() => ads);
    }
}