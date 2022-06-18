using Application.Advertisments.Commands.DTOs;
using Application.Common.Interfaces;
using Domain.Entities;
using MongoDB.Driver;

namespace Application.Advertisments;
public class AdService : IAdService
{
    private readonly IMongoContext context;

    public AdService(IMongoContext context) => this.context = context;

    public async Task CleanUp(int externalId)
    {
        if(context.Advertisments.AsQueryable().Count(x => x.adExternalId == externalId) > 1)
        {
            var ad = context.Advertisments.AsQueryable().First(x => x.adExternalId == externalId);
            var filter = Builders<Ad>.Filter.Eq("_id", ad.Id);
            var result = await context.Advertisments.DeleteOneAsync(filter);
        }
    }

    public async Task Upsert(AdDTO adDTO)
    {
        Ad advertisment;
        if (!string.IsNullOrEmpty(adDTO.Id) && !string.IsNullOrWhiteSpace(adDTO.Id))
        {
            var filter = Builders<Ad>.Filter.Eq("_id", adDTO.Id);
            advertisment = context.Advertisments.Find(filter).FirstOrDefault();

            if (advertisment == null)
            {
                throw new Exception("Not found advertisment with such id");
            }
            var localUser = context.Users.AsQueryable().Where(x => x.ExternalId == adDTO.OwnerId).FirstOrDefault();
            var update = Builders<Ad>.Update
                .Set(a => a.adExternalId, adDTO.adExternalId)
                .Set(a => a.OwnerId, localUser.Id)
                .Set(a => a.TitleImage, adDTO.TitleImage)
                .Set(a => a.Price, adDTO.Price)
                .Set(a => a.RoomNumber, adDTO.RoomNumber)
                .Set(a => a.FloorAmount, adDTO.FloorAmount)
                .Set(a => a.Pool, adDTO.Pool)
                .Set(a => a.Balkon, adDTO.Balkon)
                .Set(a => a.HouseYear, adDTO.HouseYear)
                .Set(a => a.RentOportunity, adDTO.RentOportunity)
                .Set(a => a.Region, adDTO.Region)
                .Set(a => a.City, adDTO.City)
                .Set(a => a.AreaOfHouse, adDTO.AreaOfHouse);

            await context.Advertisments.UpdateOneAsync(filter, update);
        }
        else
        {
            advertisment = new Ad();
            advertisment.adExternalId = adDTO.adExternalId;
            advertisment.OwnerId = adDTO.OwnerId;
            advertisment.TitleImage = adDTO.TitleImage;
            advertisment.Price = adDTO.Price;
            advertisment.RoomNumber = adDTO.RoomNumber;
            advertisment.FloorAmount = adDTO.FloorAmount;
            advertisment.Pool = adDTO.Pool;
            advertisment.Balkon = adDTO.Balkon;
            advertisment.HouseYear = adDTO.HouseYear;
            advertisment.RentOportunity = adDTO.RentOportunity;
            advertisment.Region = adDTO.Region;
            advertisment.City = adDTO.City;
            advertisment.AreaOfHouse = adDTO.AreaOfHouse;
            await context.Advertisments.InsertOneAsync(advertisment);
        }
    }
}
