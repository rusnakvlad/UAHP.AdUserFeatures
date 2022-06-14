using Application.Common.Interfaces;
using Application.Users.DTOs;
using MassTransit;
using MediatR;
using UAHause.CommonWork.Models;

namespace WebAPI.Consumers;

public class UserConsumer : IConsumer<UAHause.CommonWork.Models.User>
{
    private readonly IUserService userService;

    public UserConsumer(IUserService userService) => this.userService = userService;

    public async Task Consume(ConsumeContext<User> context)
    {
        var commonUser = context.Message;
        var user = new UserDTO
        {
            ExternalId = commonUser.Id,
            Email = commonUser.Email,
            Name = commonUser.Name,
            Surname = commonUser.Surname,
            Phone = commonUser.Phone,
        };

        await userService.Upsert(user);
    }
}
