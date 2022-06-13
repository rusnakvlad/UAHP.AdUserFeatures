﻿namespace Application.Advertisments.Commands.DTOs;

public class AdInfoDTO
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
    public bool PurchaseOportunity { get; set; }
    public string Region { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public int AreaOfHouse { get; set; }
}
