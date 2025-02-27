using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class GetUserHandlerTestData
{
    public static GetUserCommand GenerateValidCommand()
    {
        return new GetUserCommand(Guid.NewGuid());
    }

    public static GetUserCommand GenerateInvalidCommand()
    {
        return new GetUserCommand(new Guid());
    }
}