using Application.Advertisments.Commands.DTOs;

namespace Application.Common.Interfaces;
public interface IAdService
{
    Task Upsert(AdDTO adDTO);
    Task CleanUp(int externalId);
}
