using Microsoft.AspNetCore.Identity;

namespace SocialPulse.Areas.Identity.Services;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = $"Nazwa użytkownika '{userName}' jest już zajęta."
        };
    }

    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = $"Adres e-mail '{email}' jest już zajęty."
        };
    }

    public override IdentityError InvalidEmail(string? email)
    {
        return new IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = $"Adres e-mail '{email}' jest nieprawidłowy."
        };
    }
}
