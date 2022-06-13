using Application.Users.Commands.Upsert;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UpsertUserCommand, User>();
    }
}
