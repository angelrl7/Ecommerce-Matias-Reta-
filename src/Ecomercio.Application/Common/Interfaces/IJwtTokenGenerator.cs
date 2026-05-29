using Ecomercio.Domain.Entities;

namespace Ecomercio.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
