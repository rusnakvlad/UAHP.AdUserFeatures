using Application.Advertisments.Commands.DTOs;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.Advertisments.Commands.Upsert;

public class UpsertAdCommand : IRequest<string>
{
    public string Id { get; set; }
    public int adExternalId { get; set; }
    public string OwnerId { get; set; }
    public byte[] TitleImage { get; set; }
    public int Price { get; set; }
    public int RoomNumber { get; set; }
    public int FloorAmount { get; set; }
    public bool Pool { get; set; }
    public bool Balkon { get; set; }
    public int HouseYear { get; set; }
    public bool RentOportunity { get; set; }
    public string Region { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public int AreaOfHouse { get; set; }
}

public class UpsertAdCommandHandler : IRequestHandler<UpsertAdCommand, string>
{
    private readonly IMongoContext context;

    public UpsertAdCommandHandler(IMongoContext context) => this.context = context;

    public async Task<string> Handle(UpsertAdCommand request, CancellationToken cancellationToken)
    {
        Ad advertisment;
        if (!string.IsNullOrEmpty(request.Id) && !string.IsNullOrWhiteSpace(request.Id))
        {
            var filter = Builders<Ad>.Filter.Eq("_id", request.Id);
            advertisment = context.Advertisments.Find(filter).FirstOrDefault();

            if (advertisment == null)
            {
                throw new Exception("Not found advertisment with such id");
            }

            var update = Builders<Ad>.Update
                .Set(a => a.adExternalId, request.adExternalId)
                .Set(a => a.OwnerId, request.OwnerId)
                .Set(a => a.TitleImage, request.TitleImage)
                .Set(a => a.Price, request.Price)
                .Set(a => a.RoomNumber, request.RoomNumber)
                .Set(a => a.FloorAmount, request.FloorAmount)
                .Set(a => a.Pool, request.Pool)
                .Set(a => a.Balkon, request.Balkon)
                .Set(a => a.HouseYear, request.HouseYear)
                .Set(a => a.RentOportunity, request.RentOportunity)
                .Set(a => a.Region, request.Region)
                .Set(a => a.District, request.District)
                .Set(a => a.City, request.City)
                .Set(a => a.AreaOfHouse, request.AreaOfHouse);

            await context.Advertisments.UpdateOneAsync(filter, update);
            return advertisment.Id;
        }
        else
        {
            advertisment = new Ad();
            advertisment.adExternalId = request.adExternalId;
            advertisment.OwnerId = request.OwnerId;
            advertisment.TitleImage = request.TitleImage.Any() ? request.TitleImage : null;
            advertisment.Price = request.Price;
            advertisment.RoomNumber = request.RoomNumber;
            advertisment.FloorAmount = request.FloorAmount;
            advertisment.Pool = request.Pool;
            advertisment.Balkon = request.Balkon;
            advertisment.HouseYear = request.HouseYear;
            advertisment.RentOportunity = request.RentOportunity;
            advertisment.Region = request.Region;
            advertisment.District = request.District;
            advertisment.City = request.City;
            advertisment.AreaOfHouse = request.AreaOfHouse;
            await context.Advertisments.InsertOneAsync(advertisment);
            return advertisment.Id;
        }
    }
}