using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class ListUsersHandlerTestData
{
    public static ListUsersCommand GenerateValidCommand()
    {
        return new ListUsersCommand
        {
            Page = 1,
            Size = 10,
            Order = "UserName asc"
        };
    }

    public static ListUsersCommand GenerateInvalidCommand()
    {
        return new ListUsersCommand
        {
            Page = 0,
            Size = 0,
            Order = ""
        };
    }

    public static PaginatedResult<User> GeneratePaginatedResultUsers(int totalCount, int currentPage, int pageSize)
    {
        var users = new List<User>();

        var listCount = totalCount > pageSize ? pageSize : totalCount;

        for (var i = 0; i < listCount; i++)
        {
            users.Add(UserTestData.GenerateValidUser());
        }

        return new PaginatedResult<User>(users, currentPage, pageSize, totalCount);

    }
}