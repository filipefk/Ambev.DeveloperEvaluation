using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class DeleteUserHandlerTestData
{
    public static DeleteUserCommand GenerateValidCommand()
    {
        return new DeleteUserCommand(Guid.NewGuid());
    }

    public static DeleteUserCommand GenerateInvalidCommand()
    {
        return new DeleteUserCommand(new Guid());
    }
}