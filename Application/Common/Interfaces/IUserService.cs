using Application.Users.DTOs;

namespace Application.Common.Interfaces;
public interface IUserService
{
    Task<UserDTO> Upsert(UserDTO userDTO);
    Task Delete(string id);
}
