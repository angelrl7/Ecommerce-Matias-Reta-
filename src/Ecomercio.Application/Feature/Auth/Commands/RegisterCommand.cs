using MediatR;
using Ecomercio.Application.Common.DTOs.User;

namespace Ecomercio.Application.Feature.Auth.Commands;

public record RegisterCommand(string Email, string Password, string Name) : IRequest<UserDto>;
