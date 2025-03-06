using Ambev.DeveloperEvaluation.Common.Security;

namespace TestUtil.Token;

public class JwtTokenGeneratorBuilder
{
    public static IJwtTokenGenerator Build() => new JwtTokenGenerator("AmbevDeveloperEvaluationSuperSecretKey");
}
