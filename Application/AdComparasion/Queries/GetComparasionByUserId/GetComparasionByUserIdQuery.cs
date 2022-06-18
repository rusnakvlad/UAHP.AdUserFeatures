using Application.AdComparasion.DTOs;
using Application.Common.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace Application.AdComparasion.Queries.GetComparasionByUserId;

public class GetComparasionByUserIdQuery : IRequest<List<ComparasionAdDTO>>
{
    public string UserId { get; set; }
}

public class GetComparasionByUserIdQueryHandler : IRequestHandler<GetComparasionByUserIdQuery, List<ComparasionAdDTO>>
{
    private readonly IMongoContext context;
    public GetComparasionByUserIdQueryHandler(IMongoContext context) => this.context = context;

    public async Task<List<ComparasionAdDTO>> Handle(GetComparasionByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = from user in context.Users.AsQueryable().Where(u => u.Id == request.UserId)
                    join comp in context.ComparasionAds.AsQueryable() on user.Id equals comp.UserId
                    join ad in context.Advertisments.AsQueryable() on comp.AdShortInfoId equals ad.Id
                    select new ComparasionAdDTO
                    {
                        Id = comp.Id,
                        adExternalId = ad.adExternalId,
                        OwnerId = user.Id,
                        TitleImage = ad.TitleImage,
                        Price = ad.Price,
                        RoomNumber = ad.RoomNumber,
                        FloorAmount = ad.FloorAmount,
                        Pool = ad.Pool,
                        Balkon = ad.Balkon,
                        HouseYear = ad.HouseYear,
                        RentOportunity = ad.RentOportunity,
                        Region = ad.Region,
                        City = ad.City,
                        AreaOfHouse = ad.AreaOfHouse
                    };
        var ads = query.ToList();
        return ads;
    }
}