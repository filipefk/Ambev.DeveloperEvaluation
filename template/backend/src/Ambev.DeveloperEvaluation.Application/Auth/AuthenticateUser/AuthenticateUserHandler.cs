using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public AuthenticateUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<AuthenticateUserResult> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user == null || !_passwordHasher.VerifyPassword(command.Password, user.Password))
        {
            throw new UnauthorizedException("Invalid credentials");
        }

        var activeUserSpec = new ActiveUserSpecification();
        if (!activeUserSpec.IsSatisfiedBy(user))
        {
            throw new UnauthorizedException("User is not active");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        var result = _mapper.Map<AuthenticateUserResult>(user);
        result.Token = token;

        return result;

    }
}

