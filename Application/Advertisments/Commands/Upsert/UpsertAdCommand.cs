using Application.Advertisments.Commands.DTOs;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.Advertisments.Commands.Upsert;

public class UpsertAdCommand : IRequest<string>
{
    public AdDTO adDTO { get; set; }
}

public class UpsertAdCommandHandler : IRequestHandler<UpsertAdCommand, string>
{
    private readonly IMongoContext context;

    public UpsertAdCommandHandler(IMongoContext context) => this.context = context;

    public async Task<string> Handle(UpsertAdCommand request, CancellationToken cancellationToken)
    {
        Ad advertisment;
        if (!string.IsNullOrEmpty(request.adDTO.Id) && !string.IsNullOrWhiteSpace(request.adDTO.Id))
        {
            var filter = Builders<Ad>.Filter.Eq("_id", request.adDTO.Id);
            advertisment = context.Advertisments.Find(filter).FirstOrDefault();

            if (advertisment == null)
            {
                throw new Exception("Not found advertisment with such id");
            }

            var update = Builders<Ad>.Update
                .Set(a => a.adExternalId, request.adDTO.adExternalId)
                .Set(a => a.OwnerId, request.adDTO.OwnerId)
                .Set(a => a.TitleImage, request.adDTO.TitleImage)
                .Set(a => a.Price, request.adDTO.Price)
                .Set(a => a.RoomNumber, request.adDTO.RoomNumber)
                .Set(a => a.FloorAmount, request.adDTO.FloorAmount)
                .Set(a => a.Pool, request.adDTO.Pool)
                .Set(a => a.Balkon, request.adDTO.Balkon)
                .Set(a => a.HouseYear, request.adDTO.HouseYear)
                .Set(a => a.RentOportunity, request.adDTO.RentOportunity)
                .Set(a => a.Region, request.adDTO.Region)
                .Set(a => a.City, request.adDTO.City)
                .Set(a => a.AreaOfHouse, request.adDTO.AreaOfHouse);

            await context.Advertisments.UpdateOneAsync(filter, update);
            return advertisment.Id;
        }
        else
        {
            advertisment = new Ad();
            advertisment.adExternalId = request.adDTO.adExternalId;
            advertisment.OwnerId = request.adDTO.OwnerId;
            advertisment.TitleImage = request.adDTO.TitleImage.Any() ? request.adDTO.TitleImage : null;
            advertisment.Price = request.adDTO.Price;
            advertisment.RoomNumber = request.adDTO.RoomNumber;
            advertisment.FloorAmount = request.adDTO.FloorAmount;
            advertisment.Pool = request.adDTO.Pool;
            advertisment.Balkon = request.adDTO.Balkon;
            advertisment.HouseYear = request.adDTO.HouseYear;
            advertisment.RentOportunity = request.adDTO.RentOportunity;
            advertisment.Region = request.adDTO.Region;
            advertisment.City = request.adDTO.City;
            advertisment.AreaOfHouse = request.adDTO.AreaOfHouse;
            await context.Advertisments.InsertOneAsync(advertisment);
            return advertisment.Id;
        }
    }
}