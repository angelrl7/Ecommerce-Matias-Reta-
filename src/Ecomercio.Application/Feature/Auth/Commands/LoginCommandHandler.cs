using MediatR;
using System.Security.Cryptography;
using System.Text;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Domain.Exceptions;
using Ecomercio.Application.Common.DTOs.User;
using Ecomercio.Application.Common.Interfaces;

namespace Ecomercio.Application.Feature.Auth.Commands;

public class LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, ct);
        if (user is null)
            throw new NotFoundException("Usuario", request.Email);

        if (user.PasswordHash != ComputeHash(request.Password))
            throw new BusinessRuleException("Credenciales inválidas.");

        var token = jwtTokenGenerator.GenerateToken(user);
        return new LoginResponseDto(
            token,
            new UserDto(user.Id, user.Email, user.FullName, user.Role, user.CreatedAt)
        );
    }

    private static string ComputeHash(string password)
        => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}