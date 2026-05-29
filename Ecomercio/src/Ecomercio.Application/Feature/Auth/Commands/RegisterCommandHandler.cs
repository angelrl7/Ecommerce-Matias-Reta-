using MediatR;
using System.Security.Cryptography;
using System.Text;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Domain.Exceptions;
using Ecomercio.Application.Common.DTOs.User;
using UserEntity = Ecomercio.Domain.Entities.User;

namespace Ecomercio.Application.Feature.Auth.Commands;

public class RegisterCommandHandler(IUserRepository userRepository)
    : IRequestHandler<RegisterCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterCommand request, CancellationToken ct)
    {
        if (await userRepository.ExistsAsync(request.Email, ct))
            throw new BusinessRuleException($"El email '{request.Email}' ya está registrado.");

        var user = UserEntity.Create(request.Email, ComputeHash(request.Password), request.Name);
        await userRepository.AddAsync(user, ct);

        return new UserDto(user.Id, user.Email, user.Name, user.Role, user.CreatedAt);
    }

    private static string ComputeHash(string password)
        => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}
