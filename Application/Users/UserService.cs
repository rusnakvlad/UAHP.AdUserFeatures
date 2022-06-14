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

    public Task Delete(string id)
    {
        throw new NotImplementedException();
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
            await context.Users.InsertOneAsync(user);
        }

        return mapper.Map<UserDTO>(user);
    }
}
