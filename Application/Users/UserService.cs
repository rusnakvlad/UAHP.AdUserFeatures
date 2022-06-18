using Application.Common.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using MongoDB.Driver;

namespace Application.Users;
public class UserService : IUserService
{
    private IMongoContext context;
    private readonly IMapper mapper;

    public UserService(IMongoContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task Delete(string id)
    {
        var filter = Builders<User>.Filter.Eq("_id", id);
        await context.Users.DeleteOneAsync(filter);
    }

    public async Task<UserDTO> Upsert(UserDTO userDTO)
    {
        User user;

        if (!string.IsNullOrEmpty(userDTO.Id) && !string.IsNullOrWhiteSpace(userDTO.Id))
        {
            var filter = Builders<User>.Filter.Eq("_id", userDTO.Id);
            user = context.Users.Find(filter).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("Not found user with such id");
            }

            var update = Builders<User>.Update
                .Set(u => u.ExternalId, userDTO.ExternalId)
                .Set(u => u.Name, userDTO.Name)
                .Set(u => u.Surname, userDTO.Surname)
                .Set(u => u.Email, userDTO.Email)
                .Set(u => u.Phone, userDTO.Phone);

            await context.Users.UpdateOneAsync(filter, update);
        }
        else
        {
            user = new User
            {
                ExternalId = userDTO.ExternalId,
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Email = userDTO.Email,
                Phone = userDTO.Phone
            };

            if(context.Users.AsQueryable().Count(u => u.ExternalId == user.ExternalId) == 0)
            {
                await context.Users.InsertOneAsync(user);
            }
        }

        return mapper.Map<UserDTO>(user);
    }

    public async Task CleanUp(string externalId)
    {
        if(context.Users.AsQueryable().Count(x => x.ExternalId == externalId) > 1)
        {
            var user = context.Users.AsQueryable().First(x => x.ExternalId == externalId);
            var filter = Builders<User>.Filter.Eq("_id", user.Id);
            await context.Users.DeleteOneAsync(filter);
        }
    }
}
