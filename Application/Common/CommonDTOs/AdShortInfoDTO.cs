using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.CommonDTOs;

public class AdShortInfoDTO
{
    public string Id { get; set; }
    public int AdExternalId { get; set; }
    public string OwnerId { get; set; }
    public byte[] TitleImage { get; set; }
    public int Price { get; set; }
    public int RoomNumber { get; set; }
    public bool Balkon { get; set; }
    public int HouseYear { get; set; }
    public int AreaOfHouse { get; set; }
}
