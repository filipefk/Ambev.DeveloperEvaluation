using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace TestUtil.Command;

public class AuthenticateUserCommandBuilder
{
    public static AuthenticateUserCommand Build()
    {
        return new Faker<AuthenticateUserCommand>()
            .RuleFor(user => user.Email, fake => fake.Internet.Email())
            .RuleFor(user => user.Password, fake => fake.Internet.Password());
    }

    public static AuthenticateUserCommand Build(User user)
    {
        return new AuthenticateUserCommand()
        {
            Email = user.Email,
            Password = user.Password
        };
    }
}
