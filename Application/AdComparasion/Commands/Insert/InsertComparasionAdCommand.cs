using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.AdComparasion.Commands.Insert;

public class InsertComparasionAdCommand : IRequest<string>
{
    public string AdvertismentId { get; set; }
    public string UserId { get; set; }
}

internal class InsertComparasionAdCommandHandler : IRequestHandler<InsertComparasionAdCommand, string>
{
    private readonly IMongoContext context;

    public InsertComparasionAdCommandHandler(IMongoContext context) => this.context = context;

    public async Task<string> Handle(InsertComparasionAdCommand request, CancellationToken cancellationToken)
    {
        ComparasionAd comparasionAd = new ComparasionAd() { AdShortInfoId = request.AdvertismentId, UserId = request.UserId };
        await context.ComparasionAds.InsertOneAsync(comparasionAd);
        return comparasionAd.Id;
    }
}