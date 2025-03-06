using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Bogus;

namespace TestUtil.Request;

public class CreateUserRequestBuilder
{
    public static CreateUserRequest Build()
    {
        return new Faker<CreateUserRequest>()
            .RuleFor(u => u.UserName, _ => GenerateValidUsername())
            .RuleFor(u => u.Password, _ => GenerateValidPassword())
            .RuleFor(u => u.Email, _ => GenerateValidEmail())
            .RuleFor(u => u.Phone, _ => GenerateValidPhone())
            .RuleFor(u => u.Status, f => UserStatus.Active.ToString())
            .RuleFor(u => u.Role, f => UserRole.Admin.ToString());
    }

    public static string GenerateValidEmail()
    {
        return new Faker().Internet.Email();
    }

    public static string GenerateValidPassword()
    {
        return $"Test@{new Faker().Random.Number(100, 999)}";
    }

    public static string GenerateValidPhone()
    {
        var faker = new Faker();
        return $"+55{faker.Random.Number(11, 99)}{faker.Random.Number(100000000, 999999999)}";
    }

    public static string GenerateValidUsername()
    {
        return new Faker().Internet.UserName();
    }

    public static string GenerateInvalidEmail()
    {
        var faker = new Faker();
        return faker.Lorem.Word();
    }

    public static string GenerateInvalidPassword()
    {
        return new Faker().Lorem.Word();
    }

    public static string GenerateInvalidPhone()
    {
        return new Faker().Random.AlphaNumeric(5);
    }
}
