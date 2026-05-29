using MediatR;
using Ecomercio.Application.Common.DTOs.User;

namespace Ecomercio.Application.Feature.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponseDto>;
