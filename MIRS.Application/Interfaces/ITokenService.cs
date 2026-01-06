using MIRS.Domain.Models;

namespace MIRS.Application.Interfaces;

public interface ITokenService
{
    (string Token, DateTime ExpiresAt) CreateToken(AppUser user, IList<string> roles);
}
