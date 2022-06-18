using Application.Advertisments.Commands.DTOs;
using Application.Common.Interfaces;
using MassTransit;
using UAHause.CommonWork.Models;

namespace WebAPI.Consumers;

public class AdConsumer : IConsumer<UAHause.CommonWork.Models.Advertisment>
{
    private readonly IAdService adService;

    public AdConsumer(IAdService adService) => this.adService = adService;


    public async Task Consume(ConsumeContext<Advertisment> context)
    {
        var commonAd = context.Message;
        var adToInsert = ConvertToLocalAd(commonAd);
        await adService.Upsert(adToInsert);
        await adService.CleanUp(adToInsert.adExternalId);
    }

    private AdDTO ConvertToLocalAd(UAHause.CommonWork.Models.Advertisment ad)
    {
        return new AdDTO
        {
            adExternalId = ad.adExternalId,
            OwnerId = ad.OwnerId,
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
            AreaOfHouse = ad.AreaOfHouse,
        };
    }
}
