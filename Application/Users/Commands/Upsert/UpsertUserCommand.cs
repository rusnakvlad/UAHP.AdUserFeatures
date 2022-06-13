using Application.Common.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.Users.Commands.Upsert;

public class UpsertUserCommand : IRequest<UserDTO>
{
    public string Id { get; set; }
    public string ExternalId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class UpsertUserCommandHandler : IRequestHandler<UpsertUserCommand, UserDTO>
{
    private readonly IMongoContext context;
    private readonly IMapper mapper;

    public UpsertUserCommandHandler(IMongoContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<UserDTO> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
    {
        User user;

        if(!string.IsNullOrEmpty(request.Id) && !string.IsNullOrWhiteSpace(request.Id))
        {
            var filter = Builders<User>.Filter.Eq("_id", request.Id);
            user = context.Users.Find(filter).FirstOrDefault();

            if(user == null)
            {
                throw new Exception("Not found user with such id");
            }

            var update = Builders<User>.Update
                .Set(u => u.ExternalId, request.ExternalId)
                .Set(u => u.Name, request.Name)
                .Set(u => u.Surname, request.Surname)
                .Set(u => u.Email, request.Email)
                .Set(u => u.Phone, request.Phone);

            await context.Users.UpdateOneAsync(filter, update);
        }
        else
        {
            user = mapper.Map<User>(request);
            await context.Users.InsertOneAsync(user);
        }

        return mapper.Map<UserDTO>(user);
    }
}